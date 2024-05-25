using BlazorApp.Models;

using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components
{
    public partial class DisplayProjects
    {        
        [Parameter]
        public IEnumerable<Project> Projects { get; set; }
        [Parameter]
        public Logos Logos { get; set; }
        [Parameter]
        public Action<int> OnProjectSelected { get; set; }

        private void TriggerSelection(int Id) => OnProjectSelected?.Invoke(Id);
    }
}
