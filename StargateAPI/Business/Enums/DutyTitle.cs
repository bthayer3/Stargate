using System.ComponentModel;

namespace StargateAPI.Business.Enums
{
    public enum DutyTitle
    {
        //Assigned integer values here on purpose to be future proof in case someone wants to reorder these, then we won't cause out of sync database issues
        [Description("Mission Specialist")] MissionSpecialist = 0,
        [Description("Payload Specialist")] PayloadSpecialist = 1,
        [Description("Flight Engineer")] FlightEngineer = 2,
        [Description("Commander")] Commander = 3,
        [Description("Pilot")] Pilot = 4,
        [Description("Lunar Module Pilot")] LunarModulePilot = 5,
        [Description("Science Officer")] ScienceOfficer = 6,
        [Description("Retired")] Retired = 7     //Instruction said use "RETIRED" but trying to enforce PascalCase naming convention for enums
    }
}