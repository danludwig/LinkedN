using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkedN.MvcDemo.Models
{
    public class PersonResponse
    {
        public string Code { get; set; }
        public IHandleLinkedInRequest<Person> Handler { get; set; }
    }
}