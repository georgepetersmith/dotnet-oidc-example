using System.Text.Json;
using IdentityModel.Client;

using var authClient = new HttpClient();
var disco = await authClient.GetDiscoveryDocumentAsync("https://localhost:5001");
if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}

Console.WriteLine("Querying discovery");
var tokenResponse = await authClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
    {
        Address = disco.TokenEndpoint,
        ClientId = "console_app",
        ClientSecret = "my_secret",
        Scope = "weatherapi"
    });

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}

using var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

Console.WriteLine("Querying identity");
var identityResponse = await apiClient.GetAsync("https://localhost:6001/identity");
if (!identityResponse.IsSuccessStatusCode)
{
    Console.WriteLine(identityResponse.StatusCode);
    return;
}

var identityDoc = JsonDocument.Parse(await identityResponse.Content.ReadAsStringAsync()).RootElement;
Console.WriteLine(JsonSerializer.Serialize(identityDoc, new JsonSerializerOptions {WriteIndented = true}));

Console.WriteLine("Querying weather");
var weatherResponse = await apiClient.GetAsync("https://localhost:6001/weatherforecast");
if (!weatherResponse.IsSuccessStatusCode)
{
    Console.WriteLine(weatherResponse.StatusCode);
    return;
}

var weatherDoc = JsonDocument.Parse(await weatherResponse.Content.ReadAsStringAsync()).RootElement;
Console.WriteLine(JsonSerializer.Serialize(weatherDoc, new JsonSerializerOptions {WriteIndented = true}));
