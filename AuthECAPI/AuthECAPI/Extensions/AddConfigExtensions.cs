namespace AuthECAPI.Extensions
{
    public static class AddConfigExtensions
    {
        public static WebApplication ConfigureCORS(this WebApplication app,IConfiguration configuration)
        {

            return app;
        }
    }
}
