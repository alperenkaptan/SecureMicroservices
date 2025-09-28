namespace Movies.Client.Services;

public interface IMovieService
{
    Task<IEnumerable<Models.MovieViewModel>> GetAllMoviesAsync();
    Task<Models.MovieViewModel?> GetMovieByIdAsync(int id);
    Task<Models.MovieViewModel> AddMovieAsync(Models.MovieViewModel movie);
    Task<Models.MovieViewModel?> UpdateMovieAsync(int id, Models.MovieViewModel movie);
    Task<bool> DeleteMovieAsync(int id);
    Task<bool> SaveChangesAsync();
    Task<Models.UserInfoViewModel> GetUserInfo();
}
