using Gherkin.Ast;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Gherkin.Quick;
using Feature = Xunit.Gherkin.Quick.Feature;

namespace CopilotDemo.Tests;

public class HttpFeature : Feature, IDisposable
{
    private HttpClient _client;
    private HttpRequestMessage _request;
    internal HttpResponseMessage _result;

    public HttpFeature()
    {
        var application = new WebApplicationFactory<Program>();
        _client = application.CreateClient();
    }
    
    [And(@"I set the header ([\w-]+) to be (\w+)")]
    public void I_set_the_header_headerName_to_be_headerValue(string headerName, string headerValue)
    {
        _request.Headers.Add(headerName, headerValue);
    }
    
    [When(@"The request is sent")]
    public async Task The_request_is_sent()
    {
        _result = await _client.SendAsync(_request);
    }
    
    [Then(@"the status code returned should be (\d+)")]
    public void The_status_code_returned_should_be(int statusCode)
    {
        Assert.Equal((int)_result.StatusCode, statusCode);
    }
    

    internal void SetHttpRequestMessage(HttpMethod method, string requestUrl)
    {
        _request = new HttpRequestMessage(method, requestUrl);
    }
    
    internal HttpMethod ParseStatusCode(string statusCode)
    {
        return statusCode switch
        {
            "GET" => HttpMethod.Get,
            "POST" => HttpMethod.Post,
            "Put" => HttpMethod.Put,
            "Delete" => HttpMethod.Delete,
            _ => throw new NotSupportedException($"{statusCode} not supported")
        };
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}