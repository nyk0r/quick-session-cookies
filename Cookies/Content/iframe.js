(function () {
    function getRawCookieValue(name) {
        var cookies = document.cookie.split(';');
        for (var i = 0; i < cookies.length; i++) {
            var pair = cookies[i].split('=');
            if (pair.length === 2 && pair[0].trim() === name) {
                return pair[1];
            }
        }
        return undefined;
    }

    function slideSessionCookie() {
        var cookieName = 'slidingSession',
            path = '/',
            // repeat methos every second
            keepAliveIntervalMsec = 1000,
            // make cookie expire in two seconds
            expires = new Date(+new Date() + keepAliveIntervalMsec * 2),
            cookieValue = getRawCookieValue(cookieName);

        console.log(decodeURIComponent(document.cookie));
        if (cookieValue) {
            // update the cookie by setting new expiration date
            document.cookie = cookieName + '=' + cookieValue + '; expires=' + expires.toGMTString() + '; path=' + path;
        } else {
            // if cookie is absent force reloading. this can happen when user hits the back button trying to return to our site after visiting onother in the same tab.
            window.location.replace(path);
        }
        console.log(decodeURIComponent(document.cookie));

        // schedule another round
        window.setTimeout(slideSessionCookie, keepAliveIntervalMsec);
    }

    document.addEventListener('DOMContentLoaded', function () {
        slideSessionCookie();
    });
}());
