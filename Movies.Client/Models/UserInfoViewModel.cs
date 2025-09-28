namespace Movies.Client.Models;

public class UserInfoViewModel
{
    public Dictionary<string, string> UserInfoDictionary { get; set; } = new();
    public UserInfoViewModel(Dictionary<string, string> userInfoDictionary)
    {
        UserInfoDictionary = userInfoDictionary;
    }
}
