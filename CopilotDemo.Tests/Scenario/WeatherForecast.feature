Feature: WeatherForecast
  In order to know what the weather will be
  As a user of the application
  I want to call this API and retrieve my forecast
  
Scenario: I make a valid http request
  Given I create a GET request to the endpoint with 12 as the hours
  And I set the header X-REGION to be Perth
  When The request is sent
  Then the status code returned should be 200