using Movies.Client.Models;

namespace Movies.Client.Services;

public class MovieService : IMovieService
{
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
        var movieList = new List<Movie>
        {
            new Movie { Id = 1, Title = "Inception", Director = "Christopher Nolan", ReleaseDate = new DateTime(1964, 9, 22) },
            new Movie { Id = 2, Title = "The Matrix", Director = "The Wachowskis", ReleaseDate = new DateTime(1974, 9, 22) },
            new Movie { Id = 3, Title = "Interstellar", Director = "Christopher Nolan", ReleaseDate = new DateTime(1994, 9, 22) }
        };
        return await Task.FromResult(movieList);
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
