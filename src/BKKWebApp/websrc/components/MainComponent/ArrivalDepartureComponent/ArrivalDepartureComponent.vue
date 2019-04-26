<template></template>
<script>
import bkkApi from "../../../js/bkkApi.js";
export default {
  props: ["lat", "lng"],
  created: function() {
    let req = bkkApi.getArrivalsAndDeparturesForLocation(this.lat, this.lng);
    req
      .then(res => res.json())
      .then(function(response) {
        console.log(response);
      })
      .catch(error => console.error("Error:", error));
  }
};
</script>
<style lang="scss">
</style>
