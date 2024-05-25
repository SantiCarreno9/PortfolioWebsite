using BlazorApp.Models;

using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class ProjectService:IDisposable
    {
        private readonly HttpClient httpClient;
        private readonly Task<IEnumerable<Project>?> getProjectsTask;

        public ProjectService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            getProjectsTask = GetProjectsAsync();
        }

        private async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            try
            {
                var response = await this.httpClient.GetAsync(GlobalValues.ProjectsFolderPath);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return Enumerable.Empty<Project>();

                    return await response.Content.ReadFromJsonAsync<IEnumerable<Project>>();
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

        public async Task<IEnumerable<Project>?> GetProjects()
        {
            var projects = await getProjectsTask;
            return projects;
        }
        public async Task<IEnumerable<Project>?> GetProjectsByCategory(Category category)
        {
            var projects = await getProjectsTask;
            return projects?.Where(x => x.CategoryId == (int)category);
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
