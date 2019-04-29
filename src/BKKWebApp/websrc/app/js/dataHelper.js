exports.createRouteObject = function (routeId, route, headsign) {
    return {
        id: routeId,
        name: route.shortName,
        headsign: headsign,
        type:
            route.type == "BUS" && route.color == "1E1E1E"
                ? "BUS_ESTI"
                : route.type
    };
}

let getTimeBySeconds = function (time) {
    return getTimeByMilliseconds(time * 1000);
}

let getTimeByMilliseconds = function (time) {
    return new Date(Math.floor(time + 7200000)) //+ 2 hour
        .toISOString()
        .substr(11, 5);
}

exports.getTimeBySeconds = getTimeBySeconds

exports.getTimeByMilliseconds = getTimeByMilliseconds

exports.getArrivalDepartureTime = function (element, currentTime) {

    if (element.predictedArrivalTime === undefined && element.arrivalTime === undefined && element.predictedDepartureTime === undefined && element.departureTime === undefined) {
        console.log(element);
        throw new Error("Object must contains predictedDepartureTime or departureTime or predictedArrivalTime or arrivalTime");
    }

    let arrivalTime = element.predictedArrivalTime || element.arrivalTime;
    let departureTime =
        element.predictedDepartureTime || element.departureTime;
    let isPredicted = element.predictedArrivalTime !== undefined || element.predictedDepartureTime !== undefined;

    return {
        arriveAfter:
            arrivalTime === undefined
                ? undefined
                : Math.floor((arrivalTime - currentTime) / 60),
        arriveAt:
            arrivalTime === undefined
                ? undefined
                : getTimeBySeconds(arrivalTime),
        departureAfter:
            departureTime === undefined
                ? undefined
                : Math.floor((departureTime - currentTime) / 60),
        departureAt:
            departureTime === undefined
                ? undefined
                : getTimeBySeconds(departureTime),
        isPredicted: isPredicted,
        timeAfter: function () {
            return this.arriveAfter === undefined
                ? this.departureAfter
                : this.arriveAfter;
        },
        timeAt: function () {
            return this.arriveAt === undefined
                ? this.departureAt
                : this.arriveAt;
        }
    };
}