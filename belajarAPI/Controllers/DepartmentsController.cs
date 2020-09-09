using belajarAPI.Models;
using belajarAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace belajarAPI.Controllers
{
    public class DepartmentsController : ApiController
    {
        DepartmentRepository depRepo = new DepartmentRepository();
        // GET: api/Department
        public async Task<IEnumerable<Department>> Get()
        {
            return await depRepo.Get();
           // return new string[] { "value1", "value2" };
        }

        //GET: api/Department/5
        public Department Get(int id)
        {

            //depRepo.Get(department);
           return depRepo.Get(id); 
        }

        // POST: api/Department
        public IHttpActionResult Post(Department department)
        {
            depRepo.Create(department);
            return Ok("Department Successfully Added");

        }

        // PUT: api/Department/5
        public IHttpActionResult Put(int id, Department department)
        {
            depRepo.Update(id, department);
            return Ok("Department has been updated");
        }

        // DELETE: api/Department/5
        public IHttpActionResult Delete(int id)
        {
            depRepo.Delete(id);
            return Ok("OK");
        }
    }
}
