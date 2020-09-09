using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace belajarAPI.Models
{
    [Table("Tb_M_Departement")]
    public class Department
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }

        public Department()
        {
        }
    }
}