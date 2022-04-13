using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Apple_Clone_Website.Models.Metadatas
{
    [MetadataTypeAttribute(typeof(CustomerMetadata))]
    public partial class Customer
    {
        internal sealed class CustomerMetadata
        {
            public string CustomerID { get; set; }
            public Nullable<bool> Gender { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Country { get; set; }
            public Nullable<System.DateTime> CreatedAt { get; set; }
            public Nullable<System.DateTime> UpdatedAt { get; set; }
            public Nullable<bool> IsDeleted { get; set; }
        }
    }
}