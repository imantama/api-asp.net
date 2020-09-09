using belajarAPI.Models;
using belajarAPI.Repositories;
using belajarAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace belajarAPI.Controllers
{
    public class DivisionsController : ApiController
    {
        DivisionRepositories divRepo = new DivisionRepositories();
        // GET: api/Divisions
        public IHttpActionResult Create(DivisionVm divisionVm)
        {
            var add = divRepo.Create(divisionVm);
            if (add > 0)
            {
                return Ok("Successfully Created");
            }
            return BadRequest("Not Success");
        }
        public async Task<IEnumerable<DivisionVm>> Get()
        {
            return await divRepo.GetAll();
        }

        [ResponseType(typeof(DivisionVm))]
        
        public DivisionVm Get(int id)
        {

            return divRepo.GetID(id);
        }
        // GET: api/Divisions/5
        
        public IHttpActionResult Delete(int id)
        {
            var delete = divRepo.Delete(id);
            if (delete > 0)
            {
                return Ok("Successfully Delete");
            }
            return BadRequest("Not Success");
        }
        [HttpPut]
        [ActionName("Divisions/{id}")]
        public IHttpActionResult Update(int id, DivisionVm divisionVm)
        {
            var update = divRepo.Update(id, divisionVm);
            if (update > 0)
            {
                return Ok("Successfully Update");
            }
            return BadRequest("Not Success");
        }
    }
}
