using Xunit.Gherkin.Quick;

namespace CopilotDemo.Tests.Scenario;

[FeatureFile("./Scenario/WeatherForecast.feature")]
public class WeatherForecast : HttpFeature
{
    [Given(@"I create a (\w+) request to the endpoint with (\d+) as the hours")]
    public void I_create_http_request_to_endpoint_with_hours(string statusCode, int hours)
    {
        SetHttpRequestMessage(ParseStatusCode(statusCode), CreateRequestUri(hours));
    }

    private string CreateRequestUri(int hours)
    {
        return $"/weatherforecast?hours={hours}";
    }
}