using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuHobbyWeb.Models.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}