using belajarAPI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class DivisionsController : Controller
    {
        readonly HttpClient http = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:11598/api/")
        };
        // GET: Divisions
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadDiv()
        {
            IEnumerable<DivisionVm> divisionVms = null;
            var resTask = http.GetAsync("divisions");
            resTask.Wait();
            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<DivisionVm>>();
                readTask.Wait();
                divisionVms = readTask.Result;
            }
            else
            {
                divisionVms = Enumerable.Empty<DivisionVm>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }

            return Json(divisionVms, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetById(int id)
        {
            DivisionVm divisionVms = null;
            var resTask = http.GetAsync("divisions/" + id);
            resTask.Wait();
            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var getJson = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                divisionVms = JsonConvert.DeserializeObject<DivisionVm>(getJson);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }

            return Json(divisionVms, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertOrUpdate(DivisionVm divisionVms, int id)
        {
            try
            {
                var json = JsonConvert.SerializeObject(divisionVms);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                if (divisionVms.id == 0)
                {
                    var result = http.PostAsync("divisions", byteContent).Result;
                    return Json(result);
                }
                else if (divisionVms.id != 0)
                {
                    var result = http.PutAsync("divisions/" + id, byteContent).Result;
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
            var result = http.DeleteAsync("divisions/" + id).Result;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}