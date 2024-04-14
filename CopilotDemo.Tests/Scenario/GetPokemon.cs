using Xunit.Gherkin.Quick;

namespace CopilotDemo.Tests.Scenario;

[FeatureFile("./Scenario/GetPokemon.feature")]
public class GetPokemon : HttpFeature
{
    [Given(@"I create a (\w+) request to the endpoint with (\d+) as the generation")]
    public void I_create_http_request_to_endpoint_with_generation(string statusCode, int generation)
    {
        SetHttpRequestMessage(ParseStatusCode(statusCode), CreateRequestUri(generation));
    }

    private string CreateRequestUri(int generation)
    {
        return $"/getpokemon?generation={generation}";
    }
}