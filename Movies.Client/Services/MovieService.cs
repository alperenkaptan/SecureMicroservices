using Movies.Client.Models;
using System.Text.Json;


namespace Movies.Client.Services;

public class MovieService : IMovieService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public MovieService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public Task<Movie> AddMovieAsync(Movie movie)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteMovieAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
    {
        var client = _httpClientFactory.CreateClient("MoviesAPIClient");
        var response = await client.GetAsync("/api/movies");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var movieList = JsonSerializer.Deserialize<List<Movie>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return movieList ?? new List<Movie>();
    }

    public Task<Movie?> GetMovieByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Movie?> UpdateMovieAsync(int id, Movie movie)
    {
        throw new NotImplementedException();
    }
}
