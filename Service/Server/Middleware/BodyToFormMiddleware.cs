using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Server.Middleware
{
    public class BodyToFormMiddleware
    {
        private readonly RequestDelegate _next;

        public BodyToFormMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if ((context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put) && context.Request.ContentType?.ToLower().StartsWith("application/json") == true)
            {
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();

                var formData = new Dictionary<string, string>();
                formData = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);

                var formBuilder = new StringBuilder();
                foreach (var kvp in formData)
                {
                    formBuilder.Append($"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}&");
                }
                var formDataString = formBuilder.ToString().TrimEnd('&');

                var formDataBytes = Encoding.UTF8.GetBytes(formDataString);
                context.Request.Body = new MemoryStream(formDataBytes);
                context.Request.ContentType = "application/x-www-form-urlencoded";
            }

            await _next(context);
        }
    }
}
