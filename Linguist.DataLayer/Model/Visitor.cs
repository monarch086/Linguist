using System;

namespace Linguist.DataLayer.Model
{
    public class Visitor
    {
        public int VisitorId { get; set; }

        public string Login { get; set; }

        public string Ip { get; set; }

        public string Url { get; set; }

        public DateTime Date { get; set; }
    }
}
