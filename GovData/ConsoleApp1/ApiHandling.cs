using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ConsoleApp1
{
    public class ApiHandling
    {
        string jwt = "eyJraWQiOiJjZWlkZyIsImFsZyI6IkhTNTEyIn0.eyJnaXZlbl9uYW1lIjoiS2FtaWwiLCJwZXNlbCI6IjkyMDYxNTE0MjkzIiwiaWF0IjoxNjUwMzYxMTYyLCJmYW1pbHlfbmFtZSI6IkthcmFzaWV3aWN6IiwiY2xpZW50X2lkIjoiVVNFUi05MjA2MTUxNDI5My1LQU1JTC1LQVJBU0lFV0lDWiJ9.0NQkVav7j7uKFnlEPcVcALFLvGvuZHpb8Z7K7XhalVk0bcsHGoHseZK9rNfiXSsj5xv9vcq6jpG5HhPqU5dnmg";
        private HttpClient HttpClientApi { get; set; }
        public ApiHandling(string url)
        {
            HttpClientApi = new HttpClient();
            HttpClientApi.DefaultRequestHeaders.Accept.Clear();
            HttpClientApi.BaseAddress = new Uri(url);
            HttpClientApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            HttpClientApi.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
        }

        public async Task<DataFromGov> GetCompanys(string endpoint)
        {
            var result = await HttpClientApi.GetAsync(endpoint);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            var myContent = await result.Content.ReadAsStringAsync();
            var dataCompanys = JsonConvert.DeserializeObject<DataFromGov>(myContent);
      
            return dataCompanys;
        }
        public async Task<DataFromGov> GetPhoneNumber(string endpoint)
        {
            var result = await HttpClientApi.GetAsync(endpoint);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            var myContent = await result.Content.ReadAsStringAsync();
            var dataCompany = JsonConvert.DeserializeObject<DataFromGov>(myContent);

            return dataCompany;
        }
    }
}
