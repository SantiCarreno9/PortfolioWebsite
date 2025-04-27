using System.Net.Http.Json;

using BlazorApp.Models;

namespace BlazorApp.Services
{
    public class ProjectService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private const string _projectsPath = "portfolio/projects.json";

        public ProjectService(HttpClient httpClient)
        {
            this._httpClient = httpClient;            
        }

        private async Task<List<Project>?> GetProjectsAsync()
        {
            try
            {
                var response = await this._httpClient.GetAsync(_projectsPath);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return null;

                    var text = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(text))
                        return null;

                    return await response.Content.ReadFromJsonAsync<List<Project>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Project>?> GetProjects()
        {
            var projects = await GetProjectsAsync();
            if (projects == null)
                return null;
            return projects;
        }
        public async Task<List<Project>?> GetProjectsByCategory(ProjectCategory category)
        {
            var projects = await GetProjectsAsync();
            if (projects == null)
                return null;
            if (category == ProjectCategory.All)
                return projects;
            return projects.Where(p => p.Category == category).ToList();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
