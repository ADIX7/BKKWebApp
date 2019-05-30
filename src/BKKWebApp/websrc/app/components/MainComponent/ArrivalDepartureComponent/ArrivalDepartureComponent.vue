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
  subscription: null,
  updateTimer: null,
  signalRInitTimer: null,
  methods: {
    processData(response) {
      let _this = this;
      this.data = [];
      let currentTime = response.payload.currentTime / 1000;

      response.payload.data.list.forEach(element => {
        let routes = [];

        let routeId = element.routeId;
        let route = response.payload.data.references.routes[routeId];

        element.stopTimes.forEach(stopTime => {
          let tripId = stopTime.tripId;
          let wheelchairAccessible =
            response.payload.data.references.trips[tripId].wheelchairAccessible;

          routes.push({
            arrivalDepartureTime: dataHelper.getArrivalDepartureTime(
              stopTime,
              currentTime
            ),
            tripId: tripId,
            wheelchairAccessible: wheelchairAccessible
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

      this.$options.updateTimer = setTimeout(this.getUpdate, 5000);
    },
    getUpdate() {
      this.signalRApi.methods.getArrivalsAndDeparturesForLocation(
        this.lat,
        this.lng
      );
    },
    waitForSignalRConnection() {
      if (this.signalRApi.signalRConnection.connectionState == 1) {
        this.getUpdate();
      } else {
        this.$options.signalRInitTimer = setTimeout(
          this.waitForSignalRConnection,
          250
        );
      }
    }
  },
  created: function() {
    this.$options.subscription = this.signalRApi.observables.ArrivalsAndDeparturesForLocation.subscribe(
      {
        next: data => this.processData(JSON.parse(data)),
        error: err => console.error("something wrong occurred: " + err),
        complete: () => console.log("done")
      }
    );

    this.waitForSignalRConnection();
  },
  beforeDestroy: function() {
    this.$options.subscription.unsubscribe();
    clearTimeout(this.$options.updateTimer);
    clearTimeout(this.$options.signalRInitTimer);
  }
};
</script>
<style lang="scss">
@import "../../../css/common";

#arrivalDepartureContainer {
  overflow-y: auto;
}

.timeContainer {
  display: inline-block;
  width: 80px;
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
    text-align: right;
    padding-right: 5px;
  }

  .stopTimeContainer {
    grid-area: stopTime;
    padding-bottom: 20px;
    padding-left: 10px;
  }
}

.bkkTripArrowTip {
  margin-bottom: 2px;
}

/*@media not screen and (min-width: $desktopViewMinWidth) {
  .dataContainer {
    grid-template-columns: auto auto;
    .stopNameContainer {
      text-align: right;
    }
  }
}*/
</style>

<template src="./ArrivalDepartureComponent.html"></template>