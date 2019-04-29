<script>
import RouteComponent from "../../../componentsReusable/RouteComponent.vue";
import TimeDisplayComponent from "../../../componentsReusable/TimeDisplayComponent.vue";
import dataHelper from "../../../js/dataHelper.js";

export default {
  components: {
    RouteComponent,
    TimeDisplayComponent
  },
  props: ["lat", "lng"],
  inject: ["signalRApi"],
  data() {
    return {
      data: []
    };
  },
  methods: {
    processData(response) {
      console.log(response);
      let _this = this;
      this.data = [];
      let currentTime = response.payload.currentTime / 1000;

      response.payload.data.list.forEach(element => {
        let routes = [];

        let routeId = element.routeId;
        let route = response.payload.data.references.routes[routeId];

        element.stopTimes.forEach(stopTime => {
          routes.push({
            arrivalDepartureTime: dataHelper.getArrivalDepartureTime(
              stopTime,
              currentTime
            ),
            tripId: stopTime.tripId
          });
        });

        let stop =
          element.stopTimes.length > 0
            ? response.payload.data.references.stops[
                element.stopTimes[0].stopId
              ]
            : { name: "" };

        _this.data.push({
          route: dataHelper.createRouteObject(routeId, route, element.headsign),
          routes: routes,
          stopName: stop.name
        });
      });
    },
    getUpdate() {
      this.signalRApi.methods.getArrivalsAndDeparturesForLocation(
        this.lat,
        this.lng
      );
    }
  },
  created: function() {
    this.signalRApi.observables.ArrivalsAndDeparturesForLocation.subscribe({
      next: data => this.processData(JSON.parse(data)),
      error: err => console.error("something wrong occurred: " + err),
      complete: () => console.log("done")
    });

    this.getUpdate();
  }
};
</script>
<style lang="scss">
.timeContainer {
  padding-right: 10px;
}

.dataContainer {
  display: grid;
  grid-template-rows: 25px auto;
  grid-template-columns: 300px auto;
  grid-template-areas:
    "tripId tripCurrent"
    "stopTime stopTime";

  .routeContainer {
    grid-area: tripId;
  }

  .stopNameContainer {
    grid-area: tripCurrent;
  }

  .stopTimeContainer {
    grid-area: stopTime;
    padding-bottom: 10px;
  }
}

@media not screen and (min-width: 600px) {
  .dataContainer {
    grid-template-columns: auto auto;
    .stopNameContainer{
      text-align: right;
    }
  }
}
</style>

<template src="./ArrivalDepartureComponent.html"></template>