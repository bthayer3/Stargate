using System.ComponentModel;

namespace StargateAPI.Business.Enums
{
    public enum Rank
    {
        //Assigned integer values here on purpose to be future proof in case someone wants to reorder these, then we won't cause out of sync database issues
        //Use military ranks or astronaut ranks?
        [Description("Private")] PVT = 0,
        [Description("Private First Class")] PFC = 1,
        [Description("Specialist")] SPC = 2,
        [Description("Corporal")] CPL = 3,
        [Description("Sergeant")] SGT = 4,
        [Description("Staff Sergeant")] SSGT = 5,
        [Description("Sergeant First Class")] SFC = 6,
        [Description("Master Sergeant")] MSGT = 7,
        [Description("First Sergeant")] FSGT = 8,
        [Description("Warrant Officer")] WO = 9,
        [Description("Chief Warrant Officer 2")] CW2 = 10,
        [Description("Chief Warrant Officer 3")] CW3 = 11,
        [Description("Chief Warrant Officer 4")] CW4 = 12,
        [Description("Chief Warrant Officer 5")] CW5 = 13,
        [Description("Second Lieutenant")] LT2 = 14,
        [Description("First Lieutenant")] LT1 = 15,  //Seed data originally used 1LT but enums cannot start with digit
        [Description("Captain")] CPT = 16,
        [Description("Major")] MAJ = 17,
        [Description("Lieutenant Colonel")] LTC = 18,
        [Description("Colonel")] COL = 19,
        [Description("Brigadier General=")] BG = 20,
        [Description("Major General")] MG = 21,
        [Description("Lieutenant General")] LTG = 22,
        [Description("General")] GEN = 23,
        [Description("General of the Army")] GOA = 24
    }
}