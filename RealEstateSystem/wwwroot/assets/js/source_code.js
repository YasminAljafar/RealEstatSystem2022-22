var myMap;
var lyrOSM;
var mrkCurrentLocation;
var popMinarEPakistan;
var ctlPan;
var ctlZoomSlider;
var marker;


$(document).ready(function () {

    // create map object

    myMap = L.map('map_div', { center: [35.998933, 36.784922], zoom:13,zoomControl: false});
    // add basemap layer
    lyrOSM = L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png');
    myMap.addLayer(lyrOSM);
    ctlPan = L.control.pan().addTo(myMap);
    ctlZoomSlider = L.control.zoomslider({position: 'topright'}).addTo(myMap);
  



    // popup for minar e pakistan
    popMinarEPakistan = L.popup();
    popMinarEPakistan.setLatLng([35.998933, 36.784922]);
    popMinarEPakistan.setContent("<h2> Minar e Pakistan </h2>" +
        "<img src='img/minar-e-pakistan.jpg' width='300px' />");

    // popMinarEPakistan.openOn(myMap);

    // event handler on left click
    myMap.on('click' , function (e) {
        //alert(e.latlng.toString());
        //alert(myMap.getZoom());
        $('#lat').val(lat_to_string(e.latlng));
        $('#lng').val(lng_to_string(e.latlng));
        if (mrkCurrentLocation) {

            mrkCurrentLocation.remove();
        }
        mrkCurrentLocation = L.marker(e.latlng).addTo(myMap);
        myMap.setView(e.latlng, 14);
        //popMinarEPakistan.setLatLng([e.latlng.lat, e.latlng.lng]);
        //debugger;
        //l.marker.clear();
       // L.marker(e.latlng).addTo(myMap).bindPopup(e.latlng.toString());

    });


    // right click
    myMap.on('contextmenu', function (e) {
        L.marker(e.latlng).addTo(myMap).bindPopup(e.latlng.toString());
    })


    // call locate method

    myMap.on('keypress', function (e) {
        if (e.originalEvent.key = 'l'){
            myMap.locate();
        }
    });

    myMap.on('locationfound', function (e) {

        if(mrkCurrentLocation){

            mrkCurrentLocation.remove();
        }
        mrkCurrentLocation = L.marker(e.latlng).addTo(myMap);
        myMap.setView(e.latlng, 14);

    });

    myMap.on('locationerror', function (e) {

        alert("location was not found");

    })

    // get user location on button click
    $('#get_user_location_id').click(function () {
        myMap.locate();
    })

    // get specific location
    $('#go_to_id').click(function () {
        myMap.setView([35.998933, 36.784922], 18);
        myMap.openPopup(popMinarEPakistan);
    })


    // get zoom level
    myMap.on('zoomend', function () {

        $("#zoom_level_id").html(myMap.getZoom());
    })

    // get map center
    myMap.on('moveend', function () {
        $('#map_center_id').html(lat_lng_to_string(myMap.getCenter()));
    })

    // get mouse location
    myMap.on('mousemove', function (e) {
        $('#mouse_location_id').html(lat_lng_to_string(e.latlng));
    })

    myMap.on('mousemove', function (e) {
        //$('#lat').val(lat_to_string(e.latlng));
    })

    myMap.on('mousemove', function (e) {
       // $('#lng').val(lng_to_string(e.latlng));
    })

    // custom functions
    function lat_lng_to_string(ll) {
        return "["+ll.lat.toFixed(5)+","+ll.lng.toFixed(5)+"]";
    }

    function lat_to_string(ll) {
        debugger;

        return  ll.lat.toFixed(5) ;
    }

    function lng_to_string(ll) {
        return   ll.lng.toFixed(5) ;
    }


})

