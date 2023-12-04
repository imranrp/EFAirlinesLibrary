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
    public class FlightScheduleController : ControllerBase {
        IFlightScheduleRepo scheduleRepo;
        public FlightScheduleController(IFlightScheduleRepo repo) {
            scheduleRepo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll() {
            List<FlightSchedule> schedules = await scheduleRepo.GetAllSchedules();
            return Ok(schedules);
        }
        [HttpGet("{fno}/{trdate}")]
        public async Task<ActionResult> Get(string fno, DateTime trdate) {
            try {
                FlightSchedule schedule = await scheduleRepo.GetSchedule(fno, trdate);
                return Ok(schedule);
            }
            catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByFlight/{fno}")]
        public async Task<ActionResult> GetByFlight(string fno) {
            try {
                List<FlightSchedule> schedules = await scheduleRepo.GetSchedulesByFlight(fno);
                return Ok(schedules);
            }
            catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByDate/{trdate}")]
        public async Task<ActionResult> GetByDate(DateTime trdate) {
            try {
                List<FlightSchedule> schedules = await scheduleRepo.GetSchedulesByDate(trdate);
                return Ok(schedules);
            }
            catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(FlightSchedule schedule) {
            await scheduleRepo.InsertSchedule(schedule);
            return Created($"api/flightschedule/{schedule.FlightNo}/{schedule.TravelDate}", schedule);
        }
        [HttpPut("{fno}/{trdate}")]
        public async Task<ActionResult> Update(string fno, DateTime trdate, FlightSchedule schedule)
        {
            await scheduleRepo.UpdateSchedule(fno, trdate, schedule);
            return Ok(schedule);
        }
        [HttpDelete("{fno}/{trdate}")]
        public async Task<ActionResult> Delete(string fno, DateTime trdate) {
            await scheduleRepo.DeleteSchedule(fno, trdate);
            return Ok();
        }
    }
}
