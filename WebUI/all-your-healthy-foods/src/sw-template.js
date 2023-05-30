importScripts('https://storage.googleapis.com/workbox-cdn/releases/5.1.2/workbox-sw.js');

workbox.setConfig({
    debug: true,
});

workbox.precaching.precacheAndRoute(self.__WB_MANIFEST);

workbox.routing.registerNavigationRoute(workbox.precaching.getCacheKeyForURL('/index.html'));
