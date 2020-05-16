
var map, infoWindow;
function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 12.971599, lng: 77.594563 },
        zoom: 12,
        //scaleControl: true,
        //zoomControl: true,
        //mapTypeControl: true,
        //streetViewControl: true,
        //rotateControl: true,
        //fullscreenControl: true
    });

    infoWindow = new google.maps.InfoWindow;

    var marker = new google.maps.Marker({
        position: {
            lat: 12.971599,
            lng: 77.594563,
        },
        map: map,
        draggable: true,
    });

    // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            marker.setPosition(pos);
            map.setCenter(pos);

        }, function () {
            handleLocationError(true, infoWindow, map.getCenter());

        });

    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }

    var searchBox = new google.maps.places.SearchBox(document.getElementById('searchmap'));
    google.maps.event.addListener(searchBox, 'places_changed', function () {
        var places = searchBox.getPlaces();
        var bounds = new google.maps.LatLngBounds();
        var i, place;
        for (i = 0; place = places[i]; i++) {
            bounds.extend(place.geometry.location);
            marker.setPosition(place.geometry.location);
        }
        map.fitBounds(bounds);
        map.setZoom(15);
    });

    google.maps.event.addListener(marker, 'dragend', function () {
        latlng = marker.getPosition();
        url = 'https://maps.googleapis.com/maps/api/geocode/json?latlng=' + latlng.lat() + ',' + latlng.lng() + '&sensor=false';
        $.get(url, function (data) {
            if (data.status == 'OK') {
                map.setCenter(data.results[0].geometry.location);
                if (confirm('Do you also want to change location text to ' + data.results[0].formatted_address) === true) {
                    $('#searchmap').val(data.results[0].formatted_address);
                    $('#lat').val(data.results[0].geometry.location.lat);
                    $('#lng').val(data.results[0].geometry.location.lng);
                }
            }
        });
    });
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
        'Error: The Geolocation service failed.' :
        'Error: Your browser doesn\'t support geolocation.');
    infoWindow.open(map);
}
