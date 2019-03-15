using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PasteWebApiApp.DataAccess
{
    public class Paste
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime AccessDate { get; set; }
    }
}