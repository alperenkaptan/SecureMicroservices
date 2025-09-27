namespace Movies.Client.Services;

public interface IMovieService
{
    Task<IEnumerable<Models.Movie>> GetAllMoviesAsync();
    Task<Models.Movie?> GetMovieByIdAsync(int id);
    Task<Models.Movie> AddMovieAsync(Models.Movie movie);
    Task<Models.Movie?> UpdateMovieAsync(int id, Models.Movie movie);
    Task<bool> DeleteMovieAsync(int id);

    Task<bool> SaveChangesAsync();
}
