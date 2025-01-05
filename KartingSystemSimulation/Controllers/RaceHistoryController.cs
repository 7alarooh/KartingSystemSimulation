using AutoMapper; // AutoMapper is used to map between entities and DTOs.
using KartingSystemSimulation.DTOs; // Includes Data Transfer Objects for RaceHistory.
using KartingSystemSimulation.Enums; // Provides the ErrorCode enum for error handling.
using KartingSystemSimulation.Services; // Includes the RaceHistoryService.
using Microsoft.AspNetCore.Mvc; // Provides base classes and attributes for creating controllers.


namespace KartingSystemSimulation.Controllers
{
        [ApiController] // Specifies that this class is an API controller.
        [Route("api/[controller]")] // Sets the base route for this controller to "api/RaceHistory".
        public class RaceHistoryController : ControllerBase
        {
            private readonly IRaceHistoryService _raceHistoryService; // Service for race history logic.
            private readonly IMapper _mapper; // AutoMapper instance for mapping entities to DTOs.
                                              // Constructor to initialize the controller with injected dependencies.
            public RaceHistoryController(IRaceHistoryService raceHistoryService, IMapper mapper)
            {
                _raceHistoryService = raceHistoryService; // Inject the race history service.
                _mapper = mapper; // Inject the AutoMapper instance.
            }
            // Endpoint to add racer history for a specific live race.
            [HttpPost("addFromLiveRace/{liveRaceId}")] // Route: POST /api/RaceHistory/addFromLiveRace/{liveRaceId}
            public IActionResult AddRacerHistoryFromLiveRace(int liveRaceId)
            {
                try
                {
                    // Call the service to add racer history for the specified live race.
                    _raceHistoryService.AddRacerHistoryFromLiveRace(liveRaceId);
                    return Ok(new { Message = $"Race history added successfully for LiveRaceId {liveRaceId}" }); // Return success message.
                }
                catch (ArgumentException ex) // Handle specific errors like no racers found.
                {
                    return NotFound(new
                    {
                        ErrorCode = ErrorCode.NotFound, // Return NotFound error code.
                        Error = ex.Message // Include the error message from the exception.
                    });
                }
                catch (Exception ex) // Handle unexpected errors.
                {
                    return StatusCode(500, new
                    {
                        ErrorCode = ErrorCode.UnknownError, // Return UnknownError code.
                        Error = "An error occurred while adding race history.", // Generic error message.
                        Details = ex.Message // Include exception details.
                    });
                }
            }
            // Endpoint to get all race histories.
            [HttpGet("all")] // Route: GET /api/RaceHistory/all
            public IActionResult GetAllRaceHistories()
            {
                try
                {
                    // Fetch all race histories from the service.
                    var raceHistories = _raceHistoryService.GetAllRaceHistories();
                    // Map the list of RaceHistory entities to RaceHistoryDTOs.
                    var raceHistoryDTOs = _mapper.Map<IEnumerable<RaceHistoryDTO>>(raceHistories);
                    // Return the mapped DTOs in a 200 OK response.
                    return Ok(raceHistoryDTOs);
                }
                catch (Exception ex) // Handle unexpected errors.
                {
                    return StatusCode(500, new
                    {
                        ErrorCode = ErrorCode.UnknownError, // Return UnknownError code.
                        Error = "An error occurred while fetching race histories.", // Generic error message.
                        Details = ex.Message // Include exception details.
                    });
                }
            }
            // Endpoint to get a specific race history by ID.
            [HttpGet("{historyId}")] // Route: GET /api/RaceHistory/{historyId}
            public IActionResult GetRaceHistoryById(int historyId)
            {
                try
                {
                    // Fetch the race history for the given ID from the service.
                    var raceHistory = _raceHistoryService.GetRaceHistoryById(historyId);
                    // If the race history is not found, return a 404 NotFound response.
                    if (raceHistory == null)
                    {
                        return NotFound(new
                        {
                            ErrorCode = ErrorCode.NotFound, // Return NotFound error code.
                            Error = "Race history not found." // Error message.
                        });
                    }
                    // Map the RaceHistory entity to a RaceHistoryDTO.
                    var raceHistoryDTO = _mapper.Map<RaceHistoryDTO>(raceHistory);
                    // Return the mapped DTO in a 200 OK response.
                    return Ok(raceHistoryDTO);
                }
                catch (Exception ex) // Handle unexpected errors.
                {
                    return StatusCode(500, new
                    {
                        ErrorCode = ErrorCode.UnknownError, // Return UnknownError code.
                        Error = "An error occurred while fetching race history.", // Generic error message.
                        Details = ex.Message // Include exception details.
                    });
                }
            }
        }
}
