(function () {
    function poll() {
        var xhr = new XMLHttpRequest();
        xhr.open("POST", "/poll");
        xhr.setRequestHeader("Accept", "application/json");
        xhr.send();

        window.setTimeout(poll, 2000);
    }
    window.setTimeout(poll, 2000);

    document.addEventListener('DOMContentLoaded', function () {
        console.log('PAGE LOADED');
    });
}());