namespace Movies.Client.Models;

public class MovieViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Director { get; set; }
    public double Rating { get; set; }
}
