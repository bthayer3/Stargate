using System.ComponentModel.DataAnnotations.Schema;

namespace StargateAPI.Business.Dtos
{
    [Table(nameof(RequestLog))]
    public class RequestLog
    {
        public int Id { get; set; }

        public string Endpoint { get; set; } = "Unknown Endpoint"; // Default to "Unknown Endpoint" if not set

        public string HttpMethod { get; set; } = "UNKNOWN"; // Default to "UNKNOWN" if not set

        public int StatusCode { get; set; } = 0;

        public DateTime RequestTime { get; set; } = DateTime.UtcNow;

        public DateTime? ResponseTime { get; set; }

        public string? ExceptionMessage { get; set; }

        public bool IsSuccess { get; set; } = false;
    }
}