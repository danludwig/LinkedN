using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinkedN.MvcDemo.Models
{
    public class PersonRequest
    {
        public PersonIdentifier Identifier { get; set; }
        public ProfileVersion ProfileVersion { get; set; }
        public string MemberId { get; set; }
        public string MemberUrl { get; set; }
        public ProfileFieldCheckBox[] Fields { get; set; }
    }
}