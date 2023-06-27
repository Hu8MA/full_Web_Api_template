namespace jwt.Security
{
    public class AuthResult
    {
        public string token { get; set; }
        public bool result { get; set; }
        public List<string> listError { get; set; }
    }
}
