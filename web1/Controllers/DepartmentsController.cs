using belajarAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace web1.Controllers
{
    public class DepartmentsController : Controller
    {
        readonly HttpClient http = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:11598/api/")
        };
        
        // GET: Departments
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadDepart()
        {
            IEnumerable<Department> departments = null;
            var resTask = http.GetAsync("departments");
            resTask.Wait();
            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var read = result.Content.ReadAsAsync<IList<Department>>();
                read.Wait();
                departments = read.Result;
            }
            else {
               departments = Enumerable.Empty<Department>();
               ModelState.AddModelError(string.Empty, "Server error");
            }
            return Json(departments, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetById(int id)
        {
            Department departments = null;
            var resTask = http.GetAsync("departments/" + id);
            resTask.Wait();
            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var getJson = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                departments = JsonConvert.DeserializeObject<Department>(getJson);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }

            return Json(departments, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertOrUpdate(Department departments, int id)
        {
            try
            {
                var json = JsonConvert.SerializeObject(departments);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                if (departments.id == 0)
                {
                    var result = http.PostAsync("departments", byteContent).Result;
                    return Json(result);
                }
                else if (departments.id != 0)
                {
                    var result = http.PutAsync("departments/" + id, byteContent).Result;
                    return Json(result);
                }

                return Json(404);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult Delete(int id)
        {
            var result = http.DeleteAsync("departments/" + id).Result;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }

}