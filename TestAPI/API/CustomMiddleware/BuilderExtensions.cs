namespace API.CustomMiddleware
{
    public static class BuilderExtensions
    {
        public static void RegisterMiddeleare(this IApplicationBuilder app)
        {
            app.UseMiddleware<LoggerMiddeleware>();
            //app.UseMiddleware<KerberosMiddleware>();


            app.ConfigureExceptionHandler();
        }

        //public static void RegisterConfigService(this IApplicationBuilder app)
        //{
        //}
    }
}
