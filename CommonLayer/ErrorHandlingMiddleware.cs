//using Microsoft.AspNetCore.Http;
//using Serilog;
//using System;
//using System.Net;
//using System.Threading.Tasks;

//namespace CommonLayer
//{
//    public class ErrorHandlingMiddleware
//    {
//        private readonly RequestDelegate next;

//        public static object JsonConvert { get; private set; }

//        public ErrorHandlingMiddleware(RequestDelegate next)
//        {
//            this.next = next;
//        }

//        public async Task Invoke(HttpContext context /* other dependencies */)
//        {
//            try
//            {
//                await next(context);
//            }
//            catch (Exception ex)
//            {
//                HandleExceptionAsync(context, ex);
//            }
//        }

//        private static void HandleExceptionAsync(HttpContext context, Exception ex)
//        {
//            var code = HttpStatusCode.InternalServerError;
//            context.Response.ContentType = "application/json";
//            context.Response.StatusCode = (int)code;
//            Log.Error(ex.Message);
//        }
//    }
//}
