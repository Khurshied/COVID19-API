using Polly;
using Polly.Retry;

namespace CovidStatsRequestService.Policies
{
    public class ClientPolicy
    {
        public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; set; }
        public AsyncRetryPolicy<HttpResponseMessage> SuccessiveHttpRetry { get; set; }

        public AsyncRetryPolicy<HttpResponseMessage> CustomHttpRetry { get; set; }

        public ClientPolicy()
        {
            ImmediateHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode).
                RetryAsync(5);


            SuccessiveHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5,retryattempt => TimeSpan.FromSeconds(3));


            CustomHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryattempt => TimeSpan.FromSeconds(Math.Pow(2, retryattempt)));



        }
    }
}
