using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LinkedN.MvcDemo.Models;

namespace LinkedN.MvcDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConsumeLinkedInApi _linkedN;

        public HomeController()
        {
            _linkedN = new DefaultLinkedInClient();
        }

        public ActionResult LogonStart(string returnUrl)
        {
            if (Request.Url == null)
                throw new NotSupportedException("Request.Url was unexpectedly null.");

            // Url to redirect to
            TempData["ReturnUrl"] = returnUrl;
            var receiveTokenAt = Url.Action("LogonEnd");

            _linkedN.RequestUserAuthorization(
                new LinkedInUserAuthorizationRequest(Request.Url, receiveTokenAt));
            return null;
        }

        public ActionResult LogonEnd()
        {
            var response = _linkedN.ReceiveUserAuthorization(User);
            var returnUrl = TempData["ReturnUrl"] as string;
            TempData.Remove("ReturnUrl");
            return Redirect(returnUrl ?? "/");
        }

        public ActionResult LogOff(string returnUrl)
        {
            HttpContext.Response.Cookies.Add(
                new HttpCookie(HttpCookieLinkedInTokenStorage.CookieNameConstant)
                {
                    Expires = DateTime.UtcNow.AddDays(-1),
                });
            return Redirect(returnUrl ?? "/");
        }

        [LinkedN]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Fields()
        {
            var fieldEnums = Enum.GetValues(typeof(PersonField))
                .Cast<PersonField>().ToArray();
            var checkBoxes = fieldEnums.Select(f =>
                new ProfileFieldCheckBox
                {
                    Text = f.ActualName(),
                    Value = f.ToString()
                });
            return Json(checkBoxes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Analyze(PersonRequest model)
        {
            var response = Parse(model);
            var url = response.Handler.BuildRequestUrl();
            const string jsonAppendage = "?format=json";
            if (url.EndsWith(jsonAppendage))
                url = url.Substring(0, url.IndexOf(jsonAppendage, System.StringComparison.Ordinal));
            return Json(new
                {
                    response.Code,
                    Url = url,
                });
        }

        [HttpPost]
        public JsonResult Invoke(PersonRequest model)
        {
            var response = Parse(model);
            try
            {
                var result = response.Handler.SendRequestFrom(User);
                return Json(result);
            }
            catch (WebException webEx)
            {
                dynamic result = new { error = webEx.Message };
                HttpStatusCode? errorCode = null;
                var httpWebResponse = webEx.Response as HttpWebResponse;
                if (httpWebResponse != null)
                    errorCode = httpWebResponse.StatusCode;
                else if (webEx.Status == WebExceptionStatus.SendFailure)
                    errorCode = HttpStatusCode.Unauthorized;
                return Json(new
                {
                    error = webEx.Message,
                    errorCode = errorCode,
                });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        private PersonResponse Parse(PersonRequest model, bool invoke = false)
        {
            var code = new StringBuilder();
            var request = _linkedN.ForResource<Person>();
            code.Append("var person = _linkedN");
            code.AppendLine();
            code.Append("\t.ForResource<Person>()");

            code.Append("\r\n\t.");
            switch (model.Identifier)
            {
                case PersonIdentifier.Myself:
                    request = request.Myself();
                    code.Append("Myself()");
                    break;
                case PersonIdentifier.MemberId:
                    request = request.MemberId(model.MemberId);
                    code.Append(string.Format("MemberId(\"{0}\")", model.MemberId));
                    break;
                case PersonIdentifier.MemberUrl:
                    request = request.MemberUrl((model.MemberUrl));
                    code.Append(string.Format("MemberUrl(\"{0}\")", model.MemberUrl));
                    break;
            }

            if (model.ProfileVersion == ProfileVersion.Public)
            {
                code.AppendLine();
                code.Append("\t.Public()");
                request = request.Public();
            }

            if (model.Fields.Any(f => f.IsChecked))
            {
                var selectedFields = model.Fields.Where(f => f.IsChecked).ToArray();
                request = request.Select(selectedFields.Select(f => f.EnumValue).ToArray());
                var fieldsBuilder = new StringBuilder();
                foreach (var selectedField in selectedFields)
                {
                    if (fieldsBuilder.Length > 0)
                        fieldsBuilder.Append(",");
                    fieldsBuilder.Append(string.Format("\r\n\t\t{0}", selectedField.Value));
                }
                code.Append("\r\n\t.Select(");
                code.Append(fieldsBuilder);
                code.Append("\r\n\t)");
            }

            code.Append(";");
            code.Replace("\t", "    ");

            return new PersonResponse
            {
                Code = code.ToString(),
                Handler = request,
            };
        }
    }

    public class LinkedNAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // look for cookie token
            var cookie = filterContext.HttpContext.Request.Cookies.Get(HttpCookieLinkedInTokenStorage.CookieNameConstant);
            if (cookie == null || string.IsNullOrWhiteSpace(cookie.Value))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "LogonStart",
                }));
            }
        }
    }
}
