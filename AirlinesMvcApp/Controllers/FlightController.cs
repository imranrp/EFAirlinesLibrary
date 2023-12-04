using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using AirlinesLibrary.Models;
//using AirlinesLibrary.Repos;
using EFAirlinesLibrary.Models;
using EFAirlinesLibrary.Repos;
namespace AirlinesMvcApp.Controllers {
    public class FlightController : Controller {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5071/api/Flight/") };
        public async Task<ActionResult> Index() {
            List<Flight> flights = await client.GetFromJsonAsync<List<Flight>>("");
            return View(flights);
        }
        public async Task<ActionResult> Details(string fno) {
            Flight flight = await client.GetFromJsonAsync<Flight>("" + fno);
            return View(flight);
        }
        public ActionResult Create() {
            Flight flight = new Flight();
            return View(flight);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Flight flight) {
            await client.PostAsJsonAsync<Flight>("", flight);
            return RedirectToAction(nameof(Index));
        }
        [Route("Flight/Edit/{fno}")]
        public async Task<ActionResult> Edit(string fno) {
            Flight flight = await client.GetFromJsonAsync<Flight>("" + fno);
            return View(flight);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Flight/Edit/{fno}")]
        public async Task<ActionResult> Edit(string fno, Flight flight) {
            await client.PutAsJsonAsync<Flight>("" + fno, flight);
            return RedirectToAction(nameof(Index));
        }
        [Route("Flight/Delete/{fno}")]
        public async Task<ActionResult> Delete(string fno) {
            Flight flight = await client.GetFromJsonAsync<Flight>("" + fno);
            return View(flight);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Flight/Delete/{fno}")]
        public async Task<ActionResult> Delete(string fno, IFormCollection collection) {
            await client.DeleteAsync("" + fno);
            return RedirectToAction(nameof(Index));
        }
    }
}
