namespace Movies.API.Data;

public class MoviesContextSeed
{
    public static async Task SeedAsync(MoviesAPIContext moviesContext)
    {
        if (!moviesContext.Movie.Any())
        {
            moviesContext.Movie.AddRange(GetPreconfiguredMovies());
            await moviesContext.SaveChangesAsync();
        }
    }

    private static IEnumerable<Model.Movie> GetPreconfiguredMovies()
    {
        return new List<Model.Movie>()
        {
            new Model.Movie()
            {
                Title = "The Shawshank Redemption",
                Genre = "Drama",
                ReleaseDate = new DateTime(1994, 9, 22),
                Director = "Frank Darabont",
                Rating = 9.3
            },
            new Model.Movie()
            {
                Title = "The Godfather",
                Genre = "Crime",
                ReleaseDate = new DateTime(1972, 3, 24),
                Director = "Francis Ford Coppola",
                Rating = 9.2
            },
            new Model.Movie()
            {
                Title = "The Dark Knight",
                Genre = "Action",
                ReleaseDate = new DateTime(2008, 7, 18),
                Director = "Christopher Nolan",
                Rating = 9.0
            }
        };
    }
}
