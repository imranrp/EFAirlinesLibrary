using EFAirlinesLibrary.Models;
using EFAirlinesLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlinesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FlightController : ControllerBase {
        IFlightRepo flightRepo;
        public FlightController(IFlightRepo repo) {
            flightRepo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll() {
            List<Flight> flights = await flightRepo.GetAllFlights();
            return Ok(flights);
        }
        [HttpGet("{fno}")]
        public async Task<ActionResult> Get(string fno) {
            try {
                Flight flight = await flightRepo.GetFlight(fno);
                return Ok(flight);
            }
            catch(Exception ex) {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(Flight flight) {
            await flightRepo.InsertFlight(flight);
            return Created($"api/flight/{flight.FlightNo}", flight);
        }
        [HttpPut("{fno}")]
        public async Task<ActionResult> Update(string fno, Flight flight) {
            await flightRepo.UpdateFlight(fno, flight);
            return Ok(flight);
        }
        [HttpDelete("{fno}")]
        public async Task<ActionResult> Delete(string fno) {
            await flightRepo.DeleteFlight(fno);
            return Ok();
        }
    }
}
