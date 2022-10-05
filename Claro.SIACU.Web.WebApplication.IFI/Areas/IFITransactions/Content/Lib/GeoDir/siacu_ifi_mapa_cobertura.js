/*INICIO INICIATIVA - 932 | MOVILIDAD IFI | JEAN NAVARRO*/

var token;
var latitud;
var longitud;
var NombreVia;
$(document).ready(function () {

    $('#search').keyup(function (e) {

        var keyCode = e.which;
        var searchField = $('#search').val();
        if (keyCode == 13) {
            that = this;
            var buscar = searchField.split(",");
            latitud = $.trim(buscar[0]);
            longitud = $.trim(buscar[1]);
            searchLatLong(latitud, longitud);
            //event.preventDefault();
            //return false;
        }

        $('#result').html('');
      
        var expression = new RegExp(searchField, "i");

        queryString = searchField;
        token = 'f1cc5ddc-0098-4772-aecd-be8a4647950a';

        if (queryString !== "" && queryString !== undefined && queryString !== null) {
            $.ajax({
                url: 'https://apis.geodir.co/places/autocomplete/v1/json?search=' +
                    queryString + '&key=' + token,
                success: function (respuesta) {
                    predictions = respuesta.predictions;
                    $('#result').html('');
                    $.each(predictions, function (key, value) {

                        $('#result').append('<li class="list-group-item link-class"><span class="text-muted">' + value.description + '</span><span class="text-muted" style="display: none;">' + '|' + value.main_text + '</span><span class="text-muted" style="display: none;">' + '|' + value.place_id + '</span></li>');
                    });
                },
                error: function () {
                    alert('No se ha podido obtener la información');
                    console.log("No se ha podido obtener la información");
                }
            });
        }
        {
            $('#result').html('');
            cleanmarker();
            cleanSegments();
        }

    });

    $('#result').on('click', 'li', function () {
        var click_text = $(this).text().split('|');
        $('#search').val($.trim(click_text[0]));
        handleSelect($.trim(click_text[2]));
        $("#result").html('');
    });
});

var map = L.map('map', {
    center: [-12.12, -77.02],
    zoom: 13,
    minZoom: 1,
    maxZoom: 20,
    scaleControl: false,
    zoomControl: false,
    attributionControl: true
});

function cleanmarker() {
    if (this.marker) {
        this.map.removeLayer(this.marker);
    }
}

function searchLatLong(lat, long) {
    this.cleanmarker();
    var that = this;
    cleanSegments();
    $.ajax({
        url: "https://apis.geodir.co/geocoding/v1/json?latlon=" + lat + ',' + long
            + '&key=' + this.token,
        success: function (respuesta) {
            if (respuesta.status == 'OK') {
                console.log(respuesta.status);
                console.log(JSON.stringify(respuesta));
                //var place_id = respuesta.results[0].place_id;
                latitud = respuesta.results[0].geometry.coordinates.lat;
                longitud = respuesta.results[0].geometry.coordinates.lon;
                var address = respuesta.results[0].standard_address;
                that.address = address;
                that.getInformation(respuesta.results[0], lat, long, that);
                that.printMarker(latitud, longitud, address, that);
                that.map.flyTo([latitud, longitud], 16);
                //jean la respuesta
                console.log(respuesta);
                var lat_lon = latitud + "|" + longitud;
                $('#hidLatitudLongitud').val(lat_lon);
            }
            else {
                that.printMarkerLatLong(lat, long, that);
            }
            
            console.log(JSON.stringify(respuesta));
        },
        error: function () {
            console.error("No se ha podido obtener la información");
        }
    });
}

function handleSelect(place_id) {
    this.cleanmarker();
    var that = this;
    var place_id = place_id;
    this.cleanSegments();
    $.ajax({
        url: "https://apis.geodir.co/places/fields/v1/json?place_id=" + place_id
            + '&key=' + this.token,
        success: function (respuesta) {
            if (respuesta.status == 'OK') {
                latitud = respuesta.geometry.coordinates.lat;
                longitud = respuesta.geometry.coordinates.lon;
                var address = respuesta.standard_address;
                that.address = address;
                that.getInformation(respuesta,latitud,longitud, that);
                that.printMarker(latitud, longitud, address, that);
                that.map.flyTo([latitud, longitud], 16);
                //jean la respuesta
                console.log(respuesta);
                var lat_lon = latitud + "|" + longitud;
                $('#hidLatitudLongitud').val(lat_lon);
            }
        },
        error: function () {
            console.error("No se ha podido obtener la información");
        }
    });
}
function printMarkerLatLong(latitud, longitud, that) {
    that.cleanSegments();
    that.map.flyTo([latitud, longitud], 16);
    var objMsj = {};
    var customIcon = new L.Icon({
        iconUrl: '../../../../../Images/marker-home.png',
        iconSize: [40, 40],
        iconAnchor: [25, 50],
        popupAnchor: [0, -38]
    });
    that.marker = L.marker([latitud, longitud], {
        icon: customIcon,
        draggable: 'true'
    }).addTo(that.map);
    that.marker.on('dragend', function (event) {
        var marker = event.target;
        var position = marker.getLatLng();
        that.printLatLong(position.lat, position.lng, that);
        marker.setLatLng(new L.LatLng(position.lat, position.lng), { draggable: 'true' });
        map.panTo(new L.LatLng(position.lat, position.lng));
    });
    $('#Latitud').val(latitud);
    $('#Longitud').val(longitud);
    Session.DatosDireccionMapaLat = latitud;
    Session.DatosDireccionMapaLon = longitud;
    $('#txtTipoVia').show();
    $('#txtVia').show();
    $('#txtNumero').show();
    $('#txtUrbanizacion').show();
    $('#txtLatitud').show();
    $('#txtLongitud').show();
    $('#btnValidar').show();
    
    console.log("VALORES EN SESIONES - para asignar a modelos");
    Session.DatosDireccionMapaTipoVia = '';
    Session.DatosDireccionMapaNombreVia = '';
    Session.DatosDireccionMapaNroVia = '';
    Session.DatosDireccionMapaTipoSublocalidad = '';
    Session.DatosDireccionMapaSublocalidad = '';
    Session.DatosDireccionMapaDistrito = '';
    Session.DatosDireccionMapaProvincia = '';
    Session.DatosDireccionMapaDepartamento = '';
    //Session.DatosDireccionMapaLat = '';
    //Session.DatosDireccionMapaLon = '';
    Session.DatosDireccionMapaDirStd = '';
    Session.DatosCompletoDirMapa = '';
    Session.DatosDireccionMapaCodPostal = '';

    objMsj.strIdSession = Session.IDSESSION;
    objMsj.strLlave = 'Key_MsgCoberturaSIACIFI';

    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(objMsj),
        url: window.location.protocol + '//' + window.location.host + '/IFITransactions/ChangeServiceAddress/obtenerMensajeGeoDir',
        success: function (response) {
            if (response.data !== '')
            {
                alert(response.data);
            }
        }
    });
}

function printLatLong(latitud, longitud, that) {
    var vardata = latitud + ', ' + longitud;
    $('#Latitud').val(latitud);
    $('#Longitud').val(longitud);
    $('#search').val(vardata);
    Session.DatosDireccionMapaLat = latitud;
    Session.DatosDireccionMapaLon = longitud;
}


function printMarker(latitud, longitud, address, that) {
    var customIcon = new L.Icon({
        iconSize: [40, 40],
        iconAnchor: [25, 50],
        iconUrl: '../../../../../Images/marker-home.png',
        popupAnchor: [0, -38]
    });
    that.marker = L.marker([latitud, longitud], {
        icon: customIcon,
        draggable: 'true'
    }).addTo(that.map);
    that.marker.on('dragend', function (event) {
        var marker = event.target;
        var position = marker.getLatLng();
        marker.setLatLng(new L.LatLng(position.lat, position.lng), { draggable: 'true' });
        map.panTo(new L.LatLng(position.lat, position.lng));
        marker.openPopup();
        cleanSegments();
        $.ajax({
            url: 'https://apis.geodir.co/geocoding/v1/json?latlon=' +
                position.lat + ',' + position.lng + '&key=' + that.token,
            success: function (respuesta) {
                if (respuesta.status == 'OK') {
                    var firstResult = respuesta.results[0];
                    console.log(JSON.stringify(firstResult));
                    var lat = firstResult.geometry.coordinates.lat;
                    var long = firstResult.geometry.coordinates.lon;
                    that.getInformation(firstResult, lat, long, that);
                    var address_1 = firstResult.standard_address;
                    $('#search').val(address_1);

                    var lat_lon = firstResult.geometry.coordinates.lat + "|" + firstResult.geometry.coordinates.lon;
                    $('#hidLatitudLongitud').val(lat_lon);
                }
                else {
                    console.error('fallo reverse');
                }
            },
            error: function () {
                console.error("No se ha podido obtener la información");
            }
        });
    });
}

function cleanSegments() {

    $("#tipoVia").val('');
    $("#nombreVia").val('');
    $("#nroVia").val('');
    $("#Urbanizacion").val('');
    $("#Latitud").val('');
    $("#Longitud").val('');

    $('#txtVia').hide();
    $('#txtNumero').hide();
    $('#txtUrbanizacion').hide();
    $('#btnValidar').hide();
    $('#divInfo').show();
    $('#divExito').hide();
    $('#divError').hide();
    $('#btnProcesar').hide();
    $('#txtLatitud').hide();
    $('#txtLongitud').hide();

}
function getInformation(firstResult, latitud, longitud) {
    var segments = {
        ROUTE: "route",
        ROUTE_TYPE: "route_type",
        ROUTE_NUMBER: "route_number",
        ADMIN_LEVEL_3: "admin_level_3",
        ADMIN_LEVEL_2: "admin_level_2",
        ADMIN_LEVEL_1: "admin_level_1",
        COUNTRY:"country",
        SUBLOCALITY: "sublocality",
        SUBLOCALITY_TYPE:"sublocality_type",
        STANDARD_ADDRESS: "standard_address",
        POSTAL_CODE: "postal_code"
    };
    var namevia = '';
    var typeVia = '';
    for (var _i = 0, _a = firstResult.address_segments; _i < _a.length; _i++) {
        var segment = _a[_i];
        var type = segment.types;
        if (includes(type, segments.ROUTE)) {
            $("#nombreVia").val(JSON.stringify(segment.name));
            namevia = segment.name;
            Session.DatosDireccionMapaNombreVia = segment.name.toUpperCase();
        }
        else if (includes(type, segments.ROUTE_TYPE)) {
            $("#tipoVia").val(segment.name);
            typeVia = segment.name;
            Session.DatosDireccionMapaTipoVia = segment.name.toUpperCase();
        }
        else if (includes(type, segments.ROUTE_NUMBER)) {
            $("#nroVia").val(segment.name);
            Session.DatosDireccionMapaNroVia = segment.name;
        }
        else if (includes(type, segments.SUBLOCALITY_TYPE)) {
            Session.DatosDireccionMapaTipoSublocalidad = segment.name.toUpperCase();
        }
        else if (includes(type, segments.SUBLOCALITY)) {
            $("#Urbanizacion").val(segment.name);
            Session.DatosDireccionMapaSublocalidad = segment.name.toUpperCase();
        }
        else if (includes(type, segments.ADMIN_LEVEL_3)) {
            Session.DatosDireccionMapaDistrito = reemplazarAcentos(segment.name.toUpperCase());
        }
        else if (includes(type, segments.ADMIN_LEVEL_2)) {
            Session.DatosDireccionMapaProvincia = reemplazarAcentos(segment.name.toUpperCase());
        }
        else if (includes(type, segments.ADMIN_LEVEL_1)) {
            Session.DatosDireccionMapaDepartamento = reemplazarAcentos(segment.name.toUpperCase());
        }
        else if (includes(type, segments.COUNTRY)) {
            Session.DatosDireccionMapaPais = segment.name.toUpperCase();
        }
        else if (includes(type, segments.POSTAL_CODE)) {
            Session.DatosDireccionMapaCodPostal = segment.name;
        }
        
        
        Session.DatosDireccionMapaDirStd = firstResult.standard_address;
        Session.DatosCompletoDirMapa = firstResult;

        if (namevia) {
            $("#nombreVia").val(namevia);
        }
    }
    $('#Latitud').val(latitud);
    $('#Longitud').val(longitud);
    $('#txtLatitud').show();
    $('#txtLogitud').show();

    $('#tipoVia').show();
    $('#nombreVia').show();
    $('#nroVia').show();

    $('#btnValidar').show();
    $('#btnProcesar').hide();

    Session.DatosDireccionMapaLat = latitud;//firstResult.geometry.coordinates.lat;
    Session.DatosDireccionMapaLon = longitud;//firstResult.geometry.coordinates.lon;
    
    console.log("VALORES EN SESIONES - para asignar a modelos");
    console.log('valor Tipo Via: ' + Session.DatosDireccionMapaTipoVia);
    console.log('valor Nombre Via: ' + Session.DatosDireccionMapaNombreVia);
    console.log('valor Nro Via: ' + Session.DatosDireccionMapaNroVia);
    console.log('valor Tipo Sublocalidad: ' + Session.DatosDireccionMapaTipoSublocalidad);
    console.log('valor Sublocalidad: ' + Session.DatosDireccionMapaSublocalidad);
    console.log('valor Distrito: ' + Session.DatosDireccionMapaDistrito);
    console.log('valor Provincia: ' + Session.DatosDireccionMapaProvincia);
    console.log('valor Departamento: ' + Session.DatosDireccionMapaDepartamento);
    console.log('valor latitud: ' + Session.DatosDireccionMapaLat);
    console.log('valor longitud: ' + Session.DatosDireccionMapaLon);
    console.log('valor Direccion Estándar: ' + Session.DatosDireccionMapaDirStd);
    console.log('Valor Datos completos Direccion Mapa: ' + Session.DatosCompletoDirMapa);
    console.log('Valor Código Postal: ' + Session.DatosDireccionMapaCodPostal);
    
}

var reemplazarAcentos = function (cadena) {
    var chars = {
        "á": "a", "é": "e", "í": "i", "ó": "o", "ú": "u",
        "à": "a", "è": "e", "ì": "i", "ò": "o", "ù": "u",
        "Á": "A", "É": "E", "Í": "I", "Ó": "O", "Ú": "U",
        "À": "A", "È": "E", "Ì": "I", "Ò": "O", "Ù": "U"
    }
    var expr = /[áàéèíìóòúù]/ig;
    var res = cadena.replace(expr, function (e) { return chars[e] });
    return res;
}

function includes(type, str) {
    var returnValue = false;

    if (type.indexOf(str) !== -1) {
        returnValue = true;
    }

    return returnValue;
}


//function validar() {

//    var direccion = $('#search').val()
//    var latitud_longitud = $('#hidLatitudLongitud').val();

//    PageMethods.ValidarCobertura(direccion, latitud_longitud, ValidarCobertura_Callback);

    
//}

//function ValidarCobertura_Callback(objResponse) {

//    if (objResponse.Boleano) {

//        $('#btnValidar').hide();
//        $('#btnProcesar').show();
//        $('#divExito').show();
//        $('#divInfo').hide();
//        $('#divError').hide();
//    }
//    else {
//        $('#divExito').hide();
//        $('#divInfo').hide();
//        $('#divError').show();
//    }
//}

//function confirmar() {
//    window.returnValue = $('#search').val() + '|' + $('#txtTipoVia').val() + '|' + $('#txtNombreVia').val() + '|' + $('#txtNroVia').val() + $('#txtUrbanizacion').val();
//    window.close();
//    console.log("esta ejecutando en la propia funcion");
//    //Session.DatosDireccionMapaTipoVia = "";
//    //Session.DatosDireccionMapaNombreVia = "";
//    //Session.DatosDireccionMapaNroVia = "";
//    //Session.DatosDireccionMapaTipoSublocalidad = "";
//    //Session.DatosDireccionMapaSublocalidad = "";
//    //Session.DatosDireccionMapaDistrito = "";
//    //Session.DatosDireccionMapaProvincia = "";
//    //Session.DatosDireccionMapaDepartamento = "";
//    //Session.DatosDireccionMapaPais = "";
//    //Session.DatosDireccionMapaLat = "";
//    //Session.DatosDireccionMapaLon = "";
//    //Session.DatosDireccionMapaDirStd = "";
//    //Session.DatosDireccionMapaCodPostal = "";


//    console.log("se pulso en confirmar");
//}
var geodirStreets = L.tileLayer('https://tiles.geodir.co/osm_tiles/{z}/{x}/{y}.png', {
    minZoom: 1,
    maxZoom: 20,
    attribution: '&copy; <a href="https://maps.geodir.co/" target="_blank">Geodir Maps</a> Contribuciones'
}).addTo(this.map);

var googleSatellite = L.tileLayer('http://{s}.google.com/vt/lyrs=s&x={x}&y={y}&z={z}', {
    minZoom: 1,
    maxZoom: 20,
    subdomains: ['mt0', 'mt1', 'mt2', 'mt3']
});

// CONTROL ESCALA
var scale = L.control.scale({
    imperial: false
}).addTo(this.map);
//CONTROL ZOOM
var zoom = L.control.zoom({ position: 'bottomright' }).addTo(this.map);




/*FIN INICIATIVA - 932 | MOVILIDAD IFI | JEAN NAVARRO*/