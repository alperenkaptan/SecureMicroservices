using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Client.Models;
using System.Text.Json;


namespace Movies.Client.Services;

public class MovieService : IMovieService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public MovieService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
    }
    public Task<MovieViewModel> AddMovieAsync(MovieViewModel movie)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteMovieAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MovieViewModel>> GetAllMoviesAsync()
    {
        var client = _httpClientFactory.CreateClient("MoviesAPIClient");
        // Ocelot | Upstream: /movies | Downstream: /api/movies 
        var response = await client.GetAsync("/movies"); 
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var movieList = JsonSerializer.Deserialize<List<MovieViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return movieList ?? new List<MovieViewModel>();
    }

    public Task<MovieViewModel?> GetMovieByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<MovieViewModel?> UpdateMovieAsync(int id, MovieViewModel movie)
    {
        throw new NotImplementedException();
    }

    public async Task<UserInfoViewModel> GetUserInfo()
    {
        var identityServerClient = _httpClientFactory.CreateClient("IdentityServerClient");

        var metadataResponse = await identityServerClient.GetDiscoveryDocumentAsync();
        if (metadataResponse.IsError)
        {
            throw new Exception("Unable to retrieve the discovery document.");
        }

        var accessToken = await _httpContextAccessor.HttpContext!
            .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        var userInfoResponse = await identityServerClient
            .GetUserInfoAsync(new UserInfoRequest
        {
            Address = metadataResponse.UserInfoEndpoint,
            Token = accessToken
        });

        if (userInfoResponse.IsError)
        {
            throw new Exception("Unable to retrieve the user info response");
        }

        var userInfoDictnary = new Dictionary<string, string?>();
        foreach (var claim in userInfoResponse.Claims)
        {
            userInfoDictnary.Add(claim.Type, claim.Value);
        }

        return new UserInfoViewModel(userInfoDictnary!);
    }
}
