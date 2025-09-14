namespace Catalog.QueriesService._01.Dto
{
    public class AuthResultDto
    {
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
        public string Message { get; set; }
    }
}
