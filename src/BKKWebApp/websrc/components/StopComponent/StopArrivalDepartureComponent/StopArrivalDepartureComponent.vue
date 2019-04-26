<script>
import bkkApi from "../../../js/bkkApi.js";
export default {
  props: ["currentStop"],
  data() {
    return {
      stopName: "",
      stopTimeData: []
    };
  },
  created: function() {
    let _this = this;
    let req = bkkApi.getArrivalsAndDeparturesForStop(this.currentStop);
    req
      .then(res => res.json())
      .then(function(response) {
        console.log(response);

        let entry = response.payload.data.entry;
        let references = response.payload.data.references;
        let currentTime = response.payload.currentTime / 1000;

        console.log("current time: " + currentTime);

        let stopId = entry.stopId;
        let stopObject = references.stops[stopId];

        _this.stopName = stopObject.name;

        _this.stopTimeData = [];

        entry.stopTimes.forEach(function(element) {
          console.log(element);
          let route =
            references.routes[references.trips[element.tripId].routeId];

          let arrivalTime = element.arrivalTime;
          let departureTime = element.departureTime;
          console.log("arrivalTime time: " + arrivalTime);

          _this.stopTimeData.push({
            name: route.shortName,
            description: route.description,
            stopHeadSign: element.stopHeadsign, //name of final stop
            arriveAfter:
              arrivalTime === undefined
                ? undefined
                : Math.floor((arrivalTime - currentTime) / 60),
            arriveAt:
              arrivalTime === undefined
                ? undefined
                : new Date(Math.floor(arrivalTime * 1000))
                    .toISOString()
                    .substr(11, 5),
            departureAfter:
              departureTime === undefined
                ? undefined
                : Math.floor((departureTime - currentTime) / 60),
            departureAt:
              departureTime === undefined
                ? undefined
                : new Date(Math.floor(departureTime * 1000))
                    .toISOString()
                    .substr(11, 5),
            timeAfter: function() {
              return this.arriveAfter === undefined ? this.departureAfter : this.arriveAfter;
            },
            timeAt: function() {
              return this.arriveAt === undefined ? this.departureAt : this.arriveAt;
            }
          });
        });
      })
      .catch(error => console.error("Error:", error));
  }
};
</script>

<template src="./StopArrivalDepartureComponent.html"></template>
