using CSVFileReader.Models;
using Newtonsoft.Json;
using OrganizationsAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CSVFileReader.LoginService
{
    public class LoginService
    {
        public async static Task<string> Login(LoginModel model)
        {
            if(model is null)
            {
                return "The login model cannot be null";
            }

            string jsonData = JsonConvert.SerializeObject(model);

            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7126/api/users/login", content);

            if (!response.IsSuccessStatusCode)
            {
                return "Invalid login attempt";
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
