<script>
import TimeDisplayComponent from "../../../componentsReusable/TimeDisplayComponent.vue";
import dataHelper from "../../../js/dataHelper.js";

export default {
  props: ["currentStop"],
  inject: ["signalRApi"],
  components: {
    TimeDisplayComponent
  },
  data() {
    return {
      stopName: "",
      stopTimeData: []
    };
  },
  methods: {
    processData(response) {
      let _this = this;

      let entry = response.payload.data.entry;
      let references = response.payload.data.references;
      let currentTime = response.payload.currentTime / 1000;

      let stopId = entry.stopId;
      let stopObject = references.stops[stopId];

      _this.stopName = stopObject.name;

      _this.stopTimeData = [];

      entry.stopTimes.forEach(function(element) {
        let route = references.routes[references.trips[element.tripId].routeId];

        let arrivalTime = element.predictedArrivalTime || element.arrivalTime;
        let departureTime =
          element.predictedDepartureTime || element.departureTime;

        _this.stopTimeData.push({
          name: route.shortName,
          description: route.description,
          stopHeadSign: element.stopHeadsign, //name of final stop
          arrivalDepartureTime: dataHelper.getArrivalDepartureTime(element, currentTime)
        });
      });
    },
    getUpdate() {
      this.signalRApi.methods.getArrivalsAndDeparturesForStop(this.currentStop);
    }
  },
  created: function() {
    this.signalRApi.observables.ArrivalsAndDeparturesForStop.subscribe({
      next: data => this.processData(JSON.parse(data)),
      error: err => console.error("something wrong occurred: " + err),
      complete: () => console.log("done")
    });

    this.getUpdate();
  }
};
</script>

<template src="./StopArrivalDepartureComponent.html"></template>
