using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using AirlinesLibrary.Models;
//using AirlinesLibrary.Repos;
using Microsoft.AspNetCore.Mvc.Rendering;
using EFAirlinesLibrary.Models;
using EFAirlinesLibrary.Repos;

namespace AirlinesMvcApp.Controllers {
    public class FlightScheduleController : Controller {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5071/api/FlightSchedule/") };
        public async Task<ActionResult> Index() {
            List<FlightSchedule> schedules = await client.GetFromJsonAsync<List<FlightSchedule>>("");
            return View(schedules);
        }
        public async Task<ActionResult> Details(string fno, DateTime trdate) {
            FlightSchedule schedule = await client.GetFromJsonAsync<FlightSchedule>("" + fno + "/" + trdate.ToLongDateString());
            return View(schedule);
        }
        public ActionResult Create() {
            FlightSchedule schedule = new FlightSchedule();
            return View(schedule);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FlightSchedule schedule) {
            await client.PostAsJsonAsync<FlightSchedule>("", schedule);
            return RedirectToAction(nameof(Index));
        }
        [Route("FlightSchedule/Edit/{fno}/{trdate}")]
        public async Task<ActionResult> Edit(string fno, DateTime trdate) {
            FlightSchedule schedule = await client.GetFromJsonAsync<FlightSchedule>("" + fno + "/" + trdate.ToLongDateString());
            return View(schedule);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("FlightSchedule/Edit/{fno}/{trdate}")]
        public async Task<ActionResult> Edit(string fno, DateTime trdate, FlightSchedule schedule) {
            await client.PutAsJsonAsync<FlightSchedule>("" + fno + "/" + trdate, schedule);
            return RedirectToAction(nameof(Index));
        }
        [Route("FlightSchedule/Delete/{fno}/{trdate}")]
        public async Task<ActionResult> Delete(string fno, DateTime trdate) {
            FlightSchedule schedule = await client.GetFromJsonAsync<FlightSchedule>("" + fno + "/" + trdate.ToLongDateString());
            return View(schedule);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("FlightSchedule/Delete/{fno}/{trdate}")]
        public async Task<ActionResult> Delete(string fno, DateTime trdate, IFormCollection collection) {
            await client.DeleteAsync("" + fno + "/" + trdate);
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> SchedulesByFlight(string fno) {
            List<FlightSchedule> schedules = await client.GetFromJsonAsync<List<FlightSchedule>>("" + "ByFlight/" + fno);
            return View(schedules);
        }
        public async Task<ActionResult> SchedulesByDate(DateTime trdate) {
            List<FlightSchedule> schedules = await client.GetFromJsonAsync<List<FlightSchedule>>("" + "ByDate/" + trdate);
            return View(schedules);
        }
    }
}
