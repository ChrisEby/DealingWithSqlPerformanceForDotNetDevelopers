using System;
using System.Collections.Generic;

namespace AdventureWorks2014TestBed.Models
{
    public partial class DatabaseLog
    {
        public int DatabaseLogID { get; set; }
        public System.DateTime PostTime { get; set; }
        public string DatabaseUser { get; set; }
        public string Event { get; set; }
        public string Schema { get; set; }
        public string Object { get; set; }
        public string TSQL { get; set; }
        public string XmlEvent { get; set; }
    }
}
