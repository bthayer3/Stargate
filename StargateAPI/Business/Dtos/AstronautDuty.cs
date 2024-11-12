using StargateAPI.Business.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace StargateAPI.Business.Dtos
{
    [Table(nameof(AstronautDuty))]
    public class AstronautDuty
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public Rank Rank { get; set; }

        public DutyTitle DutyTitle { get; set; }

        public DateTime DutyStartDate { get; set; }

        public DateTime? DutyEndDate { get; set; }

        public virtual Person Person { get; set; }
    }
}