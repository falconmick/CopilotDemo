Feature: GetPokemon
  In order to know what the Pokemon will be
  As a user of the application
  I want to call this API and retrieve my Pokemon

  Scenario: I make a valid http request
    Given I create a GET request to the endpoint with 1 as the generation
    When The request is sent
    Then the status code returned should be 200