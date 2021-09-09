using System;
using System.Collections.Generic;

#nullable disable

namespace TacuDataAccess.Models
{
    public partial class Culoc
    {
        public int Id { get; set; }
        public DateTime? PostedDate { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public double? Amount { get; set; }
        public string CrDr { get; set; }
    }
}
