importScripts('https://storage.googleapis.com/workbox-cdn/releases/5.1.2/workbox-sw.js');

workbox.core.setCacheNameDetails({
    prefix: 'your-app-name',
    suffix: 'v1',
    precache: 'precache',
    runtime: 'runtime'
});

workbox.precaching.precacheAndRoute([]);

workbox.routing.registerRoute(
    new RegExp('https://your-api-url/'),
    new workbox.strategies.NetworkFirst()
);
