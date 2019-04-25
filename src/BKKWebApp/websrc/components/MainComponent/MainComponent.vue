<script>
import ArrivalDepartureComponent from "./ArrivalDepartureComponent/ArrivalDepartureComponent.vue";
import MapComponent from "./MapComponent/MapComponent.vue";
import StopsComponent from "./StopsComponent/StopsComponent.vue"

export default {
  components: {
    MapComponent,
    ArrivalDepartureComponent,
    StopsComponent
  },
  props: ['currentStop'],
  data() {
    return {
      activeTab: "map",
      lat: 47.477, //47.497,
      lng: 19.292 //19.065
    };
  }
};
</script>

<template src="./MainComponent.html"></template>
<style lang="scss"></style>
