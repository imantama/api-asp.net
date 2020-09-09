using belajarAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace belajarAPI.MyContext
{
    
    public class Context :DbContext
    {
        public Context() : base("myConnection") {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Division> Divisions { get; set; }
    }
}