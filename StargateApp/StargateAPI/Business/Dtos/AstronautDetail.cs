using StargateAPI.Business.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace StargateAPI.Business.Dtos
{
    [Table(nameof(AstronautDetail))]
    public class AstronautDetail
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public Rank CurrentRank { get; set; }

        public DutyTitle CurrentDutyTitle { get; set; }

        public DateTime CareerStartDate { get; set; }

        public DateTime? CareerEndDate { get; set; }

        public virtual Person Person { get; set; }
    }
}