using System;
using System.Collections.Generic;

namespace AdventureWorks2014TestBed.Models
{
    public partial class AddressType
    {
        public AddressType()
        {
            this.BusinessEntityAddresses = new List<BusinessEntityAddress>();
        }

        public int AddressTypeID { get; set; }
        public string Name { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual ICollection<BusinessEntityAddress> BusinessEntityAddresses { get; set; }
    }
}
