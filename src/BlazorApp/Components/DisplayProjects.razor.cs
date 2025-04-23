using BlazorApp.Models;

using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components
{
    public partial class DisplayProjects
    {        
        private class ProjectCategoryInfo
        {
            public string Name { get; set; }            
            public string Logo { get; set; }
            public ProjectCategory Category { get; set; }
        }
        [Parameter]
        public IEnumerable<Project> Projects { get; set; }
        [Parameter]
        public Logos Logos { get; set; }
        [Parameter]
        public Action<string> OnProjectSelected { get; set; }
        private ProjectCategory _currentProjectCategory;
        private ProjectCategoryInfo[] _projectCategories;


        private void TriggerSelection(string Id) => OnProjectSelected?.Invoke(Id);

        protected override Task OnInitializedAsync()
        {
            var categoriesInfo = Enum.GetValues(typeof(ProjectCategory));
            _projectCategories = new ProjectCategoryInfo[categoriesInfo.Length];
            for (int i = 0; i < _projectCategories.Length; i++)
            {
                _projectCategories[i] = new ProjectCategoryInfo
                {
                    Name = ((ProjectCategory)i).ToString(),
                    Logo = Logos?.GetLogoByCategory((ProjectCategory)i)??"",
                    Category = (ProjectCategory)i
                };
            }
            return base.OnInitializedAsync();
        }        

        private void SetProjectCategory(ProjectCategory category)
        {
            _currentProjectCategory = category;
            //StateHasChanged();
        }
    }
}
