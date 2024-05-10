using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorApp.Components
{
    public partial class Carousel
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public RenderFragment[] Components { get; set; }

        private ElementReference scrollerReference;

        private int currentPosition = 0;        
        
        private async Task ScrollLeft()
        {
            int nextPosition = (currentPosition == 0) ? Components.Length - 1 : currentPosition - 1;
            await ScrollTo(nextPosition);            
        }

        private async Task ScrollRight()
        {
            int nextPosition = (currentPosition == Components.Length - 1) ? 0 : currentPosition + 1;
            await ScrollTo(nextPosition);            
        }

        private async Task ScrollTo(int position)
        {
            int delta = position - currentPosition;
            currentPosition = position;
            await JSRuntime.InvokeVoidAsync("scrollElement", scrollerReference, delta); // Adjust scroll amount as needed
        }
    }
}
