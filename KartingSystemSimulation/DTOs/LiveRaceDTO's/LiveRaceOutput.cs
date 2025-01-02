namespace KartingSystemSimulation.DTOs.LiveRaceDTO_s
{
    public class LiveRaceOutputDTO
    {
        public int LiveRaceId { get; set; }
        public int GameId { get; set; }
        public DateTime RaceDate { get; set; }
        public string UpdateDetails { get; set; }
        public List<LiveRaceRacerOutputDTO> Racers { get; set; }
    }

    public class LiveRaceRacerOutputDTO
    {
        public int RacerId { get; set; }
        public int CurrentLap { get; set; }
        public TimeSpan LapTime { get; set; }
        public TimeSpan TotalTime { get; set; }
    }

}
