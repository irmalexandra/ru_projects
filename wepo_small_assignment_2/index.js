(function(globalObj) {
    document.getElementById('play-button').addEventListener('click', function(evt) {
        evt.preventDefault();

        const television = document.getElementById('television');
        const cartridge = document.getElementById('nes-cartridge');

        const introSong = new Audio('./resources/intro.mp3');
        const levelOneSong = new Audio('./resources/stage1.mp3');

        // Let's start the show!
        startTheShow(television, cartridge, introSong, levelOneSong);
    });

    function startTheShow(television, cartridge, introSong, levelOneSong) {
        // Insert cartridge
        cartridge.classList.add('move-cartridge');

        window.setTimeout(function() {
            // Start Contra!
            television.classList.remove('static');
            television.classList.add('intro');
            introSong.play();

            window.setTimeout(function() {
                levelOneSong.play();
                television.classList.remove('intro');
                television.classList.add('level-1');
            }, 6000);
        }, 2000);
    };
})(window);
