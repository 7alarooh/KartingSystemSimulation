using KartingSystemSimulation.DTOs.LiveRaceDTO_s;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Mvc;

namespace KartingSystemSimulation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LiveRaceController : ControllerBase
    {
        private readonly ILiveRaceService _liveRaceService;

        public LiveRaceController(ILiveRaceService liveRaceService)
        {
            _liveRaceService = liveRaceService;
        }

        [HttpGet]
        public IActionResult GetAllLiveRaces()
        {
            return Ok(_liveRaceService.GetAllLiveRaces());
        }

        [HttpGet("{id}")]
        public IActionResult GetLiveRaceById(int id)
        {
            return Ok(_liveRaceService.GetLiveRaceById(id));
        }

        [HttpPost]
        public IActionResult AddLiveRace(LiveRaceInputDTO liveRaceInput)
        {
            _liveRaceService.AddLiveRace(liveRaceInput);
            return Ok("Live race added successfully.");
        }

        [HttpPut("update-lap")]
        public IActionResult UpdateLapTime(UpdateLapInputDTO updateLapInput)
        {
            _liveRaceService.UpdateLapTime(updateLapInput);
            return Ok("Lap time updated successfully.");
        }

        [HttpGet("{id}/racers")]
        public IActionResult GetRaceRacers(int id)
        {
            return Ok(_liveRaceService.GetRaceRacers(id));
        }
    }

}
