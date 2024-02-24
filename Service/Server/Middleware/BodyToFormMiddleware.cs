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
            // Kiểm tra xem request có phải là phương thức POST và có chứa dữ liệu JSON không
            if (context.Request.Method == HttpMethods.Post && context.Request.ContentType?.ToLower().StartsWith("application/json") == true)
            {
                // Đọc nội dung của request
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();

                // Giả sử LoginForm là một lớp đơn giản với các thuộc tính email và password.
                // Bạn có thể cần điều chỉnh phần này dựa trên cấu trúc của lớp LoginForm của bạn.
                var formData = new Dictionary<string, string>();
                // Ở đây bạn cần phân tích dữ liệu JSON của bạn thành một từ điển hoặc bất kỳ cấu trúc phù hợp nào khác.
                // Ví dụ, tôi giả định một cấu trúc JSON đơn giản với các cặp key-value.
                formData = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);

                // Xây dựng lại request với dữ liệu form
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
