using BlazorApp.Models;
using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class LogoService : IDisposable
    {
        private readonly HttpClient httpClient;
        private readonly Task<Logos?> getLogosTask;

        public LogoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            getLogosTask = GetLogosAsync();
        }

        private async Task<Logos> GetLogosAsync()
        {
            try
            {
                var response = await this.httpClient.GetAsync(Path.Combine(GlobalValues.JsonPath,"categoryicons.json"));
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return null;

                    return await response.Content.ReadFromJsonAsync<Logos>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }        

        public async Task<Logos?> GetLogos()
        {
            var logos = await getLogosTask;
            return logos;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
