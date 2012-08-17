using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for providing a fluent client interface for accessing person resources via the Linkedin REST API.
    /// </summary>
    public static class PersonRequestExtensions
    {
        public static IHandleLinkedInRequest<Person> Myself(this IHandleLinkedInRequest<Person> endpoint)
        {
            endpoint.SetRequestOption(PersonRequestOption.Identification, "~");
            return endpoint;
        }

        public static IHandleLinkedInRequest<Person> MemberId(this IHandleLinkedInRequest<Person> endpoint, string memberId)
        {
            endpoint.SetRequestOption(PersonRequestOption.Identification, string.Format("id={0}", memberId));
            return endpoint;
        }

        public static IHandleLinkedInRequest<Person> MemberUrl(this IHandleLinkedInRequest<Person> endpoint, string memberUrl, bool isAlreadyEncoded = false)
        {
            if (!isAlreadyEncoded)
                memberUrl = HttpUtility.UrlEncode(memberUrl);
            endpoint.SetRequestOption(PersonRequestOption.Identification, string.Format("url={0}", memberUrl));
            return endpoint;
        }

        public static IHandleLinkedInRequest<Person> Public(this IHandleLinkedInRequest<Person> endpoint)
        {
            endpoint.SetRequestOption(PersonRequestOption.ProfileVersion, ":public");
            return endpoint;
        }

        public static IHandleLinkedInRequest<Person> Standard(this IHandleLinkedInRequest<Person> endpoint)
        {
            endpoint.SetRequestOption(PersonRequestOption.ProfileVersion, null);
            return endpoint;
        }

        public static IHandleLinkedInRequest<Person> Select(this IHandleLinkedInRequest<Person> endpoint, params PersonField[] fields)
        {
            // allow this method to be invoked more than once by merging with existing values (needs to be unit tested)
            fields = fields.MergeFieldSelectors(endpoint.ExtractFieldSelectors());

            // convert enums to a comma separated string
            var fieldList = new List<string>();

            foreach (var field in fields)
            {
                var dataMember = field.GetAttribute<DataMemberAttribute>();
                if (dataMember == null)
                    throw new NotSupportedException(string.Format(
                        "The person profile field selector '{0}' is not supported because it is not decorated with a DataMemberAttribute.",
                            field));
                fieldList.Add(!string.IsNullOrWhiteSpace(dataMember.Name) ? dataMember.Name : field.ToString());
            }

            var formattedSelection = string.Format(":({0})", fieldList.Implode());
            endpoint.SetRequestOption(PersonRequestOption.Fields, formattedSelection);

            return endpoint;
        }

        public static IHandleLinkedInRequest<Person> Select(this IHandleLinkedInRequest<Person> endpoint, params PersonFieldGroup[] selectors)
        {
            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (var selector in selectors)
                // ReSharper restore LoopCanBeConvertedToQuery
                endpoint = endpoint.Select(selector.Fields);

            return endpoint;
        }

        public static IHandleLinkedInRequest<Person> InLanguages(this IHandleLinkedInRequest<Person> endpoint, params string[] languages)
        {
            var builder = new StringBuilder();
            foreach (var language in languages)
            {
                if (builder.Length > 0) builder.Append(',');
                builder.Append(language);
            }

            endpoint.SetRequestOption(PersonRequestOption.Languages, builder.ToString());
            return endpoint;
        }

        public static IHandleLinkedInRequest<Person> AtUrl(this IHandleLinkedInRequest<Person> endpoint, string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Cannot be null or whitespace", "url");

            // parse out the parts
            var parts = url.StartsWith(PersonRequestHandler.BaseUrl)
                ? url.Substring(PersonRequestHandler.BaseUrl.Length) : url;

            // get the identification
            if (parts.StartsWith("~"))
            {
                endpoint.Myself();
                parts = parts.Substring(1);
            }
            else if (parts.StartsWith("id=", StringComparison.OrdinalIgnoreCase))
            {
                var memberId = parts.Substring(3);
                var colon = memberId.IndexOf(':');
                if (colon > 4)
                    memberId = memberId.Substring(0, colon);
                endpoint.MemberId(memberId);
                parts = parts.Substring(memberId.Length + 3);
            }
            else if (parts.StartsWith("url=", StringComparison.OrdinalIgnoreCase))
            {
                var memberUrl = parts.Substring(4);
                var colon = memberUrl.IndexOf(':');
                if (colon > 5)
                    memberUrl = memberUrl.Substring(0, colon);
                endpoint.MemberUrl(memberUrl);
                parts = parts.Substring(memberUrl.Length + 4);
            }
            else
            {
                throw new InvalidOperationException(
                    string.Format("Could not extract identification request part from the URL '{0}'.", url));
            }

            // get the public section
            if (parts.StartsWith(":public", StringComparison.OrdinalIgnoreCase))
            {
                endpoint.Public();
                parts = parts.Substring(7);
            }
            else
            {
                endpoint.SetRequestOption(PersonRequestOption.ProfileVersion, null);
            }

            // the rest should be the fields
            if (!string.IsNullOrWhiteSpace(parts))
                endpoint.SetRequestOption(PersonRequestOption.Fields, parts);

            return endpoint;
        }

        private static IEnumerable<PersonField> ExtractFieldSelectors(this IHandleLinkedInRequest<Person> endpoint)
        {
            var fieldEnums = new List<PersonField>();
            var fieldsString = endpoint.GetRequestOption(PersonRequestOption.Fields);
            if (!string.IsNullOrWhiteSpace(fieldsString))
            {
                // chop of wrapper
                fieldsString = fieldsString.Substring(2, fieldsString.Length - 3);

                // extract into list
                var fieldStrings = fieldsString.Explode();

                var enumValues = Enum.GetValues(typeof(PersonField)).Cast<PersonField>().ToArray();
                // ReSharper disable LoopCanBeConvertedToQuery
                foreach (var fieldString in fieldStrings)
                // ReSharper restore LoopCanBeConvertedToQuery
                {
                    // find matching enum
                    var enumValue = enumValues.Single(
                        e =>
                            fieldString.Equals(e.GetAttribute<DataMemberAttribute>().Name, StringComparison.OrdinalIgnoreCase) ||
                            fieldsString.Equals(e.ToString(), StringComparison.OrdinalIgnoreCase));
                    fieldEnums.Add(enumValue);
                }
            }
            return fieldEnums.ToArray();
        }

        private static PersonField[] MergeFieldSelectors(this IEnumerable<PersonField> first, IEnumerable<PersonField> second)
        {
            var merged = first.ToList();
            merged.AddRange(second);
            return merged.Distinct().ToArray();
        }
    }
}
