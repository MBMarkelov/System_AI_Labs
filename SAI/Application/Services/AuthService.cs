using SAI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SAI.Application.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<UserDto?> LoginAsync(string phoneNumber)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { PhoneNumber = phoneNumber });
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        public async Task<bool> ResgisterAsync(UserDto user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", user);
            return response.IsSuccessStatusCode;
        }
    }
}
