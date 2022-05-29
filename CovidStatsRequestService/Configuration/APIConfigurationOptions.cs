namespace CovidStatsRequestService.Configuration
{
    public class APIConfigurationOptions
    {
        public const string APISettings = "APISettings";
        public string? BaseURL { get; set; } 
        public string? Summary { get; set; }

        public string? UAECovidHisotry { get; set; }

    }
}
