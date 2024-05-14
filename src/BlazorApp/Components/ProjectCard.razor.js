function muteVideo(element) {
    element.muted = true;
}

function pauseVideo(element) {
    element.pause();
}

function playVideo(element) {
    element.play();
}

var stopWebVideo = function (iframe) {
    //Reassigns source video to "stop it"
    let iframeSrc = iframe.src;
    iframe.src = iframeSrc;    
}