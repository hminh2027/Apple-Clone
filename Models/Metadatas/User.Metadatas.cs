using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Apple_Clone_Website.Models.Metadatas
{
    [MetadataTypeAttribute(typeof(UserMetadata))]
    public partial class User
    {
        internal sealed class UserMetadata
        {
            public string UserID { get; set; }


            public string Username { get; set; }


            public string Password { get; set; }


            public string Email { get; set; }


            public Nullable<int> Phone { get; set; }
            public Nullable<System.DateTime> Birthday { get; set; }
            [UIHint("gender")]
            public Nullable<bool> Gender { get; set; }
            //public Gender Gender { get; set; }
            public string Country { get; set; }
            public Nullable<System.DateTime> CreatedAt { get; set; }
            public Nullable<System.DateTime> UpdatedAt { get; set; }
            public Nullable<bool> IsDeleted { get; set; }
        }
    }
}