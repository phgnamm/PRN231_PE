using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace PRN231PE_SP24_123890_DangPhuongNam_FE
{
    public class Common
    {
        public static string NoPermissionMessage = "You do not have any permission";
        public static string BaseURL = "https://localhost:7213";
        private static readonly HttpClient httpClient = new() { BaseAddress = new Uri(BaseURL) };

        public static async Task<HttpResponseMessage> SendRequestAsync(
            string url,
            HttpMethod method,
            object? body = null,
            string? jwt = null)
        {
            using var request = new HttpRequestMessage(method, url);

            // Set the JWT token if provided
            if (!string.IsNullOrEmpty(jwt))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }

            // Add JSON content if it's a POST or PUT request and body is provided
            if ((method == HttpMethod.Post || method == HttpMethod.Put) && body != null)
            {
                var json = JsonSerializer.Serialize(body);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return await httpClient.SendAsync(request);
        }

        public async static Task<T?> ReadT<T>(HttpResponseMessage reponse, bool isOdata = false)
        {
            var content = await reponse.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(content);
            if (!isOdata)
            {
                return JsonSerializer.Deserialize<T>(jsonDocument, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            var valueElement = jsonDocument.RootElement.GetProperty("value");
            return JsonSerializer.Deserialize<T>(valueElement, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async static Task<string> ReadToken(HttpResponseMessage reponse)
        {
            var responseBody = await reponse.Content.ReadAsStringAsync();
            //var token = JsonDocument.Parse(responseBody).RootElement.GetProperty("token").GetString();
            return responseBody;
        }

        public async static Task<string> ReadError(HttpResponseMessage reponse)
        {
            var responseBody = await reponse.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(responseBody);

            // Kiểm tra xem thuộc tính "message" có tồn tại trong JSON không
            if (jsonDocument.RootElement.TryGetProperty("message", out JsonElement messageElement))
            {
                return messageElement.GetString();
            }

            // Nếu không có "message", trả về thông báo lỗi mặc định
            return "An unknown error occurred";
        }


        public static void DecodeJwtToken(string token, out string id, out string role)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            id = "";
            role = "";
            if (jsonToken == null)
            {
                throw new Exception("Invalid token");
            }

            foreach (var claim in jsonToken.Claims)
            {
                switch (claim.Type)
                {
                    case ClaimTypes.Role: role = claim.Value; break;
                    case ClaimTypes.NameIdentifier: id = claim.Value; break;
                    default:
                        break;
                }
            }
        }

        public static bool CheckPermission(string? token, int[] acceptedRole)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            DecodeJwtToken(token, out string id, out string role);
            if (int.TryParse(role, out int ro) && acceptedRole.Contains(ro))
            {
                return true;
            }
            return false;
        }
    }
}



