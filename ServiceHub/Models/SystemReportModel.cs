using Newtonsoft.Json;

namespace ServiceHub.Models
{
    public sealed class SystemReportModel
    {
        [JsonProperty("systemId")]
        public string SystemID { get; set; } = nameof(SystemID);

        [JsonProperty("systemName")]
        public string SystemName { get; set; } = nameof(SystemName);

        [JsonProperty("systemDescription")]
        public string SystemDescription { get; set; } = nameof(SystemDescription);

        [JsonProperty("status")]
        public string Status { get; set; } = nameof(Status);

        [JsonProperty("isRunning")]
        public bool IsRunning { get; set; } = false;
    }
}
