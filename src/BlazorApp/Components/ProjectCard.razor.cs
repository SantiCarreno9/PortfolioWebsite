using BlazorApp.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using RazorSCLibrary;
using RazorSCLibrary.Interfaces;

namespace BlazorApp.Components
{
    public partial class ProjectCard : ComponentBase, ICarouselComponent
    {
        [Inject]
        IJSRuntime JsRuntime { get; set; }

        [Parameter]
        public Project Project { get; set; }

        private List<(MediaContentType, string)> mediaContent = new List<(MediaContentType, string)>();

        private (MediaContentType, string) currentMediaContent;
        private int currentMediaIndex = 0;

        private ElementReference videoPlayer;
        private ElementReference shortVideoPlayer;
        private ElementReference iFramePlayer;

        protected override void OnInitialized()
        {
            if (Project.MediaContent == null)
                return;

            for (int i = 0; i < 4; i++)
            {
                if (!Project.MediaContent.ContainsKey(i))
                    continue;
                foreach (var item in Project.MediaContent[i])
                {
                    mediaContent.Add(((MediaContentType)i, item));
                }
            }
            SelectContent(0);
        }

        private void SelectContent(int index)
        {
            currentMediaContent.Item1 = mediaContent[index].Item1;
            currentMediaContent.Item2 = mediaContent[index].Item2;
            currentMediaIndex = index;
        }    

        public async void StartRendering()
        {
            switch (currentMediaContent.Item1)
            {
                case MediaContentType.ShortVideo:
                    await JsRuntime.InvokeVoidAsync("playVideo", shortVideoPlayer);
                    break;
                default:
                    StateHasChanged();
                    break;
            }
        }

        public async void StopRendering()
        {
            SelectContent(0);
            switch (currentMediaContent.Item1)
            {
                case MediaContentType.ShortVideo:
                    await JsRuntime.InvokeVoidAsync("pauseVideo", shortVideoPlayer);
                    break;
                case MediaContentType.Video:
                    await JsRuntime.InvokeVoidAsync("pauseVideo", videoPlayer);
                    break;
                case MediaContentType.WebVideo:
                    await JsRuntime.InvokeVoidAsync("stopWebVideo", iFramePlayer);
                    break;
            }
        }
    }

}
