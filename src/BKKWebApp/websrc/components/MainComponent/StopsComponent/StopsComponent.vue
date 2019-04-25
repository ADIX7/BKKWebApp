<script>
import bkkApi from "../../../js/bkkApi.js";
export default {
  props: ["lat", "lng"],
  data() {
    return {
      stopData: []
    };
  },
  methods: {
    rotateIcon(deg) {
      return "transform: rotate(" + (deg - 45) + "deg);";
    }
  },
  created: function() {
    let _this = this;
    let req = bkkApi.getStopsForLocation(this.lat, this.lng);
    req
      .then(res => res.json())
      .then(function(response) {
        console.log(response);
        console.log(response.payload.data.list);
        _this.stopData = [];
        response.payload.data.list.forEach(function(element) {
          if (element.locationType === 0) {
            console.log(element);

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
      })
      .catch(error => console.error("Error:", error));
  }
};
</script>

<style lang="scss">
.stopRoute {
  padding-right: 10px;
}
</style>


<template src="./StopsComponent.html"></template>