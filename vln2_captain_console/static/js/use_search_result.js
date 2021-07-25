$(document).ready(function () {
    $('.search-result').on('click', function (e) {
        let search_txt = e.currentTarget.innerHTML
        window.location = '/search/?search_field=' + search_txt
    })
});