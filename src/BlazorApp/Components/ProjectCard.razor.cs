using BlazorApp.Models;

using Microsoft.AspNetCore.Components;

using RazorSCLibrary;
using RazorSCLibrary.Interfaces;

namespace BlazorApp.Components
{
    public partial class ProjectCard : ComponentBase, ICarouselComponent
    {
        [Inject]
        JsMethods JsMethods { get; set; }

        [Parameter]
        public Project Project { get; set; }
        [Parameter]
        public Logos Logos { get; set; }

        private List<(MediaContentType, string)> _mediaContent = new List<(MediaContentType, string)>();

        private (MediaContentType?, string?) _currentMediaContent;
        private int currentMediaIndex = 0;

        private ElementReference? _videoPlayer;
        private ElementReference? _shortVideoPlayer;
        private ElementReference? _iFramePlayer;

        protected override async Task OnParametersSetAsync()
        {            
            var contentTypes = Enum.GetValues(typeof(MediaContentType)).Cast<MediaContentType>().ToList();
            _mediaContent = new List<(MediaContentType, string)>();
            foreach (var contentType in contentTypes)
            {
                if (!Project.MediaContentDirectories.ContainsKey(contentType))
                    continue;
                foreach (var item in Project.MediaContentDirectories[contentType])
                {
                    _mediaContent.Add((contentType, item));
                }
            }
            SelectContent(0);
            await base.OnParametersSetAsync();
        }

        private void SelectContent(int index)
        {
            if (_mediaContent.Count == 0)
                return;
            _currentMediaContent.Item1 = _mediaContent[index].Item1;
            _currentMediaContent.Item2 = _mediaContent[index].Item2;
            currentMediaIndex = index;
        }

        public async void StartRendering()
        {
            switch (_currentMediaContent.Item1)
            {
                case MediaContentType.ShortVideo:
                    if (_shortVideoPlayer.HasValue)
                        await JsMethods.PlayVideo(_shortVideoPlayer.Value);
                    break;
                default:
                    StateHasChanged();
                    break;
            }
        }

        public void Rerender()
        {
            StateHasChanged();
        }

        public void Reset()
        {
            _currentMediaContent = (null, null);
            currentMediaIndex = 0;
            _mediaContent.Clear();
            StateHasChanged();
        }

        public async void StopRendering()
        {
            SelectContent(0);
            switch (_currentMediaContent.Item1)
            {
                case MediaContentType.ShortVideo:
                    if (_shortVideoPlayer.HasValue)
                        await JsMethods.PauseVideo(_shortVideoPlayer.Value);
                    break;
                case MediaContentType.Video:
                    if (_videoPlayer.HasValue)
                        await JsMethods.PauseVideo(_videoPlayer.Value);
                    break;
                case MediaContentType.WebVideo:
                    if (_iFramePlayer.HasValue)
                        await JsMethods.PauseVideo(_iFramePlayer.Value, isIFrame: true);
                    break;
            }
        }
    }

}
