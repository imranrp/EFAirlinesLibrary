using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using AirlinesLibrary.Models;
//using AirlinesLibrary.Repos;
using EFAirlinesLibrary.Models;
using EFAirlinesLibrary.Repos;
using NuGet.ProjectModel;

namespace AirlinesMvcApp.Controllers {
    public class ReservationController : Controller {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5071/api/Reservation/") };
        public async Task<ActionResult> Index() {
            List<ReservationMaster> masters = await client.GetFromJsonAsync<List<ReservationMaster>>("");
            return View(masters);
        }
        public async Task<ActionResult> Details(string pnr) {
            ReservationMaster master = await client.GetFromJsonAsync<ReservationMaster>("" + pnr);
            return View(master);
        }
        public ActionResult Create() {
            ReservationMaster master = new ReservationMaster();
            return View(master);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ReservationMaster master) {
            master.NoOfPassengers = 0;
            await client.PostAsJsonAsync<ReservationMaster>("", master);
            return RedirectToAction(nameof(Index));
        }
        [Route("Reservation/Edit/{pnr}")]
        public async Task<ActionResult> Edit(string pnr) {
            ReservationMaster master = await client.GetFromJsonAsync<ReservationMaster>("" + pnr);
            return View(master);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Reservation/Edit/{pnr}")]
        public async Task<ActionResult> Edit(string pnr, ReservationMaster master) {
            await client.PutAsJsonAsync<ReservationMaster>("" + pnr, master);
            return RedirectToAction(nameof(Index));
        }
        [Route("Reservation/Delete/{pnr}")]
        public async Task<ActionResult> Delete(string pnr) {
            ReservationMaster master = await client.GetFromJsonAsync<ReservationMaster>("" + pnr);
            return View(master);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Reservation/Delete/{pnr}")]
        public async Task<ActionResult> Delete(string pnr, IFormCollection collection) {
            await client.DeleteAsync("" + pnr);
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> ReservationsByFlight(string fno) {
            List<ReservationMaster> masters = await client.GetFromJsonAsync<List<ReservationMaster>>("" + "ByFlight/" + fno);
            return View(masters);
        }
        public async Task<ActionResult> ReservationsByDate(DateTime trdate) {
            List<ReservationMaster> masters = await client.GetFromJsonAsync<List<ReservationMaster>>("" + "ByDate/" + trdate);
            return View(masters);
        }
        public async Task<ActionResult> ReservationsByFlightAndDate(string fno, DateTime trdate) {
            List<ReservationMaster> masters = await client.GetFromJsonAsync<List<ReservationMaster>>("" + fno + "/" + trdate);
            return View(masters);
        }
        static int passCount;
        // Template: List; Model class: ReservationDetail
        public async Task<ActionResult> Passengers(string pnr) {
            List<ReservationDetail> passengers = new List<ReservationDetail>();
                passengers = await client.GetFromJsonAsync<List<ReservationDetail>>("" + "PassengersByPNR/" + pnr);
            passCount = passengers.Count;
            ViewBag.PNR = pnr;
            return View(passengers);
        }
        // Template: Create; Model class: ReservationDetail
        public ActionResult CreatePassenger(string pnr) {
            ReservationDetail passenger = new ReservationDetail();
            passenger.PNRNo = pnr;
            passenger.PassengerNo = passCount + 1;
            return View(passenger);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePassenger(ReservationDetail passenger) {
            await client.PostAsJsonAsync<ReservationDetail>("" + "Passenger", passenger);
            return RedirectToAction(nameof(Passengers), new { pnr = passenger.PNRNo });
        }
        // Template: Details; Model class: ReservationDetail
        public async Task<ActionResult> PassengerDetails(string pnr, int pno) {
            ReservationDetail passenger = await client.GetFromJsonAsync<ReservationDetail>("" + pnr + "/" + pno);
            return View(passenger);
        }
        // Template: Edit; Model class: ReservationDetail
        [Route("Reservation/EditPassenger/{pnr}/{pno}")]
        public async Task<ActionResult> EditPassenger(string pnr, int pno) {
            ReservationDetail passenger = await client.GetFromJsonAsync<ReservationDetail>("" + pnr + "/" + pno); return View(passenger);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Reservation/EditPassenger/{pnr}/{pno}")]
        public async Task<ActionResult> EditPassenger(string pnr, int pno, ReservationDetail detail){
            await client.PutAsJsonAsync<ReservationDetail>("" + pnr + "/" + pno, detail);
            return RedirectToAction(nameof(Passengers), new { pnr = pnr });
        }
        // Template: Delete; Model class: ReservationDetail
        [Route("Reservation/DeletePassenger/{pnr}/{pno}")]
        public async Task<ActionResult> DeletePassenger(string pnr, int pno) {
            ReservationDetail passenger = await client.GetFromJsonAsync<ReservationDetail>("" + pnr + "/" + pno); return View(passenger);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Reservation/DeletePassenger/{pnr}/{pno}")]
        public async Task<ActionResult> DeletePassenger(string pnr, int pno, IFormCollection collection) {
            await client.DeleteAsync("" + pnr + "/" + pno);
            return RedirectToAction(nameof(Passengers), new { pnr = pnr });
        }
    }
}
