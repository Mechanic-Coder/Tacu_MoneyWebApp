using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacuDataAccess.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string KeyWord { get; set; }
        public bool Hidden { get; set; }
    }
}
