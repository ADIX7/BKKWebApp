//TODO: remove file

let baseUrl = "http://futar.bkk.hu/bkk-utvonaltervezo-api/ws/otp/api/where/";
let key = "";
let version = "3";
let appVersion = "";

function getBaseQuery(functionName, includeReferences) {
    return baseUrl + functionName + `?key=${key}&version=${version}&appVersion=${appVersion}&includeReferences=${includeReferences}&`;
}

exports.getArrivalsAndDeparturesForLocation = function (lat, lng) {
    query = `/api/bkkapi/arrivals-and-departures-for-location?lng=${lng}&lat=${lat}`;
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