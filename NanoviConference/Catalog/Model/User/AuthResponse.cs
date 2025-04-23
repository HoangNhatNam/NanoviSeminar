namespace NanoviConference.Catalog.Model.User
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public List<string> Roles { get; set; }
        public string UserName { get; set; }
    }
}
