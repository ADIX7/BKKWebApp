<script>
import RouteComponent from "../../../componentsReusable/RouteComponent.vue";
import dataHelper from "../../../js/dataHelper.js";

export default {
  components: {
    RouteComponent
  },
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
            routes.push(dataHelper.createRouteObject(routeId, route));
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
    getUpdate() {
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
#stopContainer {
  overflow-y: auto;
}
</style>


<template src="./StopsComponent.html"></template>