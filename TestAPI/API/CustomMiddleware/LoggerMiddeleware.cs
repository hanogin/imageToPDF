using Serilog.Context;
using System.Net;

namespace API.CustomMiddleware
{
    public class LoggerMiddeleware
    {
        readonly RequestDelegate _next;
        //private IStaticDataRepository _staticDataRepository;

        public LoggerMiddeleware(RequestDelegate next)
        {
            _next = next;
        }

        //public async Task Invoke(HttpContext context, IStaticDataRepository staticDataRepository)
        public async Task Invoke(HttpContext context)

        {
            //_staticDataRepository = staticDataRepository;
            //var nextId = _staticDataRepository.GetNextIdForLog();

            var ipAddrerss = GetIpAddress(context);

            using (LogContext.PushProperty("ClientIP", ipAddrerss))
            //windows auth only!!!
            //using (LogContext.PushProperty("LogId", nextId))
            //using (LogContext.PushProperty("UserName", context.User.Identity?.Name, destructureObjects: true))
            {
                //if (context.Request.Method != "OPTIONS" && !context.User.Identity.IsAuthenticated)
                //{
                //    context.Response.StatusCode = 401;
                //    return; //Stop pipeline and return immediately.
                //}
                await _next.Invoke(context);
            }
        }

        private string GetIpAddress(HttpContext context)
        {
            IPAddress remoteIpAddress = context.Connection.RemoteIpAddress;
            string result = "";
            if (remoteIpAddress != null)
            {
                if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList
                    .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                }
                result = remoteIpAddress.ToString();
            }

            return result;
        }
    }
}
