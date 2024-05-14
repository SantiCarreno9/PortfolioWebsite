window.scrollElement = function (container, amount) {
    if (container) {
        const scrollAmount = amount * window.innerWidth;
        const currentPosition = container.scrollLeft;
        const newPosition = currentPosition + scrollAmount;

        container.scrollTo({
            left: newPosition,
            behavior: 'smooth'
        });

        // Return a promise that resolves when the scrolling animation finishes
        return new Promise(resolve => {
            container.addEventListener("scrollend", (event) => {
                resolve(true);
            })
        });
    }
};