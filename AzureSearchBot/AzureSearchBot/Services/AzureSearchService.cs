using System.Net.Http;
using System.Web.Configuration;
using System.Threading.Tasks;
using AzureSearchBot.Model;
using Newtonsoft.Json;
using System;

namespace AzureSearchBot.Services
{
    [Serializable]
    public class AzureSearchService
    {
        private static readonly string QueryString = $"https://{WebConfigurationManager.AppSettings["SearchName"]}.search.windows.net/indexes/{WebConfigurationManager.AppSettings["IndexName"]}/docs?api-key={WebConfigurationManager.AppSettings["SearchKey"]}&api-version=2015-02-28&";

        public async Task<SearchResult> SearchByName(string name)
        {
            using (var httpClient = new HttpClient())
            {
                string nameQuey = $"{QueryString}search={name}";
                string response = await httpClient.GetStringAsync(nameQuey);
                return JsonConvert.DeserializeObject<SearchResult>(response);
            }
        }

        public async Task<SearchResult> SearchProductName()
        {
            using (var httpClient = new HttpClient())
            {
                string facetQuey = $"{QueryString}facet=Product_Name";
                string response = await httpClient.GetStringAsync(facetQuey);
                return JsonConvert.DeserializeObject<SearchResult>(response);
            }
        }

        public async Task<SearchResult> SearchProductVersion(string product_Name)
        {
            using (var httpClient = new HttpClient())
            {
                string nameQuey = $"{QueryString}facet=Product_Version&$filter=Product_Name eq '{product_Name}'";

                // string nameQuey = $"{QueryString}$filter=Era eq '{era}'";
                string response = await httpClient.GetStringAsync(nameQuey);
                return JsonConvert.DeserializeObject<SearchResult>(response);
            }
        }

        public async Task<SearchResult> SearchProductOS(string product_Name, string product_Version)
        {
            using (var httpClient = new HttpClient())
            {
                string nameQuey = $"{QueryString}facet=Product_OS&$filter=Product_Name eq '{product_Name}' and Product_Version eq '{product_Version}'";

                // string nameQuey = $"{QueryString}$filter=Era eq '{era}'";
                string response = await httpClient.GetStringAsync(nameQuey);
                return JsonConvert.DeserializeObject<SearchResult>(response);
            }
        }
        public async Task<SearchResult> SearchFinal(string product_Name, string product_Version, string product_OS, string description)
        {
            using (var httpClient = new HttpClient())
            {
               // string nameQuey = $"{QueryString}search='{description}'&filter=Product_Name eq '{product_Name}' and Product_Version eq '{product_Version}' and Product_OS eq '{product_OS}' ";
                string nameQuey = $"{QueryString}search='{description}'&filter=Product_Name eq '{product_Name}' and Product_Version eq '{product_Version}'  ";

                // string nameQuey = $"{QueryString}$filter=Era eq '{era}'";
                string response = await httpClient.GetStringAsync(nameQuey);
                return JsonConvert.DeserializeObject<SearchResult>(response);
            }
        }
    }
}