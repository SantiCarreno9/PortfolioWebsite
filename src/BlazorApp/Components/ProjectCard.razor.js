function muteVideo(element) {
    element.muted = true;
}

function pauseVideo(element) {
    if (!element.paused)
        element.pause();
}

function playVideo(element) {
    element.play();
}

function stopWebVideo(iframe) {
    //Reassigns source video to "stop it"
    let iframeSrc = iframe.src;
    iframe.src = iframeSrc;
}