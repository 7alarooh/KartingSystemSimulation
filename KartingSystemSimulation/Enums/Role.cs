namespace KartingSystemSimulation.Enums
{
    public enum Role
    {
        Admin,
        Racer,
        Supervisor
    }

    public static class RolePermissions
    {
        public static Dictionary<Role, string> Permissions = new Dictionary<Role, string>
    {
        { Role.Admin, "ManageUsers,EditConfig" },
        { Role.Racer, "ParticipateRace,ViewLeaderboard" },
        { Role.Supervisor, "ManageRacers,ApproveRaces" }
    };
    }

}
