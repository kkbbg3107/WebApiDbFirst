namespace WebApiDBFirst.AuthorizationFilter
{
    public class ResponseType
    {
        public object Data { get; set; }
        public int HttpCode { get; set; }
        public object ErrorMessage { get; set; }
    }
}
