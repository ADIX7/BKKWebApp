let baseUrl = "http://futar.bkk.hu/bkk-utvonaltervezo-api/ws/otp/api/where/";
let key = "";
let version = "3";
let appVersion = "";

function getBaseQuery(functionName, includeReferences) {
    return baseUrl + functionName + `?key=${key}&version=${version}&appVersion=${appVersion}&includeReferences=${includeReferences}&`;
}

exports.getArrivalsAndDeparturesForLocation = function (lat, lng) {
    let functionName = "arrivals-and-departures-for-location.json";
    let includeReferences = "true";

    let query = getBaseQuery(functionName, includeReferences) + `lon=${lng}&lat=${lat}&radius=100&onlyDepartures=false&limit=30&minutesBefore=0&minutesAfter=30&groupLimit=3&clientLon=${lng}&clientLat=${lat}`;
    console.log(query);

    query = "http://futar.bkk.hu/bkk-utvonaltervezo-api/ws/otp/api/where/arrivals-and-departures-for-location.json?key=apaiary-test&version=3&appVersion=apiary-1.0&includeReferences=true&lon=47.477900&lat=19.045807&lonSpan=&latSpan=&radius=100&onlyDepartures=false&limit=60&minutesBefore=2&minutesAfter=30&groupLimit=4&clientLon=47.477900&clientLat=19.045807";

    query = `/api/bkkapi/lng=${lng}&lat=${lat}`;
    return fetch(query);
}

exports.getStopsForLocation = function(lat, lng){
    query = `/api/bkkapi/stops-for-location?lng=${lng}&lat=${lat}`;
    return fetch(query);
}

exports.getArrivalsAndDeparturesForStop = function(stopId){
    query = `/api/bkkapi/arrivals-and-departures-for-stop?stopId=${stopId}`;
    return fetch(query);
}