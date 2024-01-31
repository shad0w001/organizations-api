using CSVFileReader.Models;
using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSVFileReader.DataManager
{
    public static class DataManager
    {
        public static List<OrganizationModel> ReadOrganizationsFromCsv(string filePath)
        {
            List<OrganizationModel> organizations = new();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(
                reader, 
                new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    BadDataFound = null
                }))
            
            {
                csv.Read();

                while (csv.Read())
                {
                    string rowData = csv.GetField<string>(0);

                    string[] values = SplitCsvRow(rowData);

                    OrganizationModel org = new OrganizationModel
                    {
                        Name = values[2].Trim('"'),
                        Website = values[3].Trim(),
                        Country = values[4].Trim(),
                        Description = values[5].Trim(),
                        Founded = int.Parse(values[6].Trim()),
                        Industry = values[7].Trim(),
                        NumberOfEmployees = int.Parse(values[8].Trim())
                    };

                    organizations.Add(org);
                }
            }

            return organizations;
        }

        public static async Task SendDataToAPI(List<OrganizationModel> organizations, string token)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            foreach (var organization in organizations)
            {
                string jsonData = JsonConvert.SerializeObject(organization);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7126/api/organizations/create", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Organization {organization.Name} sent successfully!");
                }
                else
                {
                    Console.WriteLine($"Error sending organization {organization.Name}. Status code: {response.StatusCode}");
                }
            }
        }

        private static string[] SplitCsvRow(string rowData)
        {
            List<string> values = new List<string>();
            StringBuilder currentValue = new StringBuilder();
            bool inQuotes = false;

            foreach (char c in rowData)
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    values.Add(currentValue.ToString());
                    currentValue.Clear();
                }
                else
                {
                    currentValue.Append(c);
                }
            }

            // Add the last value
            values.Add(currentValue.ToString());

            return values.ToArray();
        }
    }
}
