<script>
//import bkkApi from "../../../js/bkkApi.js";
export default {
  props: ["lat", "lng"],
  inject: ["signalRApi"],
  data() {
    return {
      stopData: []
    };
  },
  methods: {
    rotateIcon(deg) {
      return "transform: rotate(" + (deg - 45) + "deg);";
    },
    processData(response) {
      let _this = this;
      this.stopData = [];
      response.payload.data.list.forEach(function(element) {
        if (element.locationType === 0) {
          let routes = [];

          element.routeIds.forEach(function(routeId) {
            let route = response.payload.data.references.routes[routeId];
            routes.push({
              id: routeId,
              name: route.shortName,
              type:
                route.type == "BUS" && route.color == "1E1E1E"
                  ? "BUS_ESTI"
                  : route.type
            });
          });

          _this.stopData.push({
            name: element.name,
            id: element.id,
            routes: routes,
            direction: element.direction
          });
        }
      });
    },
    getUpdate(){
      this.signalRApi.methods.getStopsForLocation(this.lat, this.lng);
    }
  },
  created: function() {
    this.signalRApi.observables.StopsForLocation.subscribe({
      next: data => this.processData(JSON.parse(data)),
      error: err => console.error("something wrong occurred: " + err),
      complete: () => console.log("done")
    });

    this.getUpdate();
  }
};
</script>

<style lang="scss">
.stopRoute {
  padding-right: 10px;
}
</style>


<template src="./StopsComponent.html"></template>