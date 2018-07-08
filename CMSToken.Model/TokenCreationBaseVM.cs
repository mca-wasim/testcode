namespace Evolent.Model
{
    public class TokenCreationBaseVM
    {
        public string ErrorVal { get; set; } = string.Empty;

        public bool IsSuccessful { get; set; } = true;

        public bool IsExists { get; set; }
    }
}
