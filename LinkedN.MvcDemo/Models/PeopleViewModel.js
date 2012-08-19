function ProfileFieldCheckBox(js) {
    var self = this;
    self.text = js.Text;
    self.value = js.Value;
    self.isChecked = ko.observable(js.IsChecked);
}

function PeopleViewModel() {
    var self = this;

    self.identifiers = ko.computed(function () {
        return [
            { value: 'myself', text: 'Mine' },
            { value: 'memberId', text: "I know the member's ID" },
            { value: 'memberUrl', text: "I know the member's URL" }
        ];
    });
    self.identifier = ko.observable(self.identifiers()[0].value);
    self.memberId = ko.observable();
    self.memberUrl = ko.observable();
    self.memberIdChecked = ko.computed(function () {
        return self.identifier() == self.identifiers()[1].value;
    });
    self.memberUrlChecked = ko.computed(function () {
        return self.identifier() == self.identifiers()[2].value;
    });
    self.focusMemberId = function () {
        setTimeout(function () {
            $(':input[data-bind*="value: memberId"]').focus();
        }, 0);
    };
    self.focusMemberUrl = function () {
        setTimeout(function () {
            $(':input[data-bind*="value: memberUrl"]').focus();
        }, 0);
    };
    self.identifier.subscribe(function (value) {
        if (value == 'memberId') self.focusMemberId();
        if (value == 'memberUrl') self.focusMemberUrl();
    });

    self.profileVersions = ko.computed(function () {
        return [
            { value: 'standard', text: "Member's Standard profile" },
            { value: 'public', text: "Member's Public profile" }
        ];
    });
    self.profileVersion = ko.observable(self.profileVersions()[0].value);

    self.fields = ko.observableArray();
    ko.computed(function () {
        $.ajax({
            url: '/Home/Fields',
            async: false
        })
        .success(function (response) {
            var mapping = {
                create: function (options) {
                    return new ProfileFieldCheckBox(options.data);
                }
            };
            var mapped = ko.mapping.fromJS(response, mapping);
            self.fields(mapped());
        });
    });
    self.fieldsSelectedCount = ko.computed(function () {
        var count = 0, fields = self.fields();
        for (var i = 0; i < fields.length; i++) {
            var field = fields[i];
            if (field.isChecked()) ++count;
        }
        return count;
    });

    self.cSharpCode = ko.observable();
    self.restRequestUrl = ko.observable();
    self.restResource = ko.observable();
    var analyzeOrInvoke = function (url, callback) {
        var input = {
            identifier: self.identifier(),
            memberId: self.memberId(),
            memberUrl: self.memberUrl(),
            profileVersion: self.profileVersion(),
            fields: self.fields()
        };
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON(input)
        })
        .success(callback);
    };
    ko.computed(function () {
        analyzeOrInvoke('/Home/Analyze', function (response) {
            self.cSharpCode(response.Code);
            self.restRequestUrl(response.Url);
        });
    }).extend({ throttle: 1 });

    self.invoke = function () {
        analyzeOrInvoke('/Home/Invoke', function (response) {
            if (!response.error)
                self.restResource(response.Resource);
        });
    };

    self.initialized = ko.computed(function () { return true; });
}
