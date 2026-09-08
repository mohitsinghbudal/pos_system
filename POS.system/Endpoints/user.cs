namespace POS.system.Endpoints
{
    public class user
    {
        public static void Endpoints(WebApplication app)
        {
            app.MapGet("/api/user", () => "Hello from user endpoint!");
        }
    }
}
