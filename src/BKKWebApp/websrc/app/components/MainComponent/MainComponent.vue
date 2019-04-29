<script>
import ArrivalDepartureComponent from "./ArrivalDepartureComponent/ArrivalDepartureComponent.vue";
import MapComponent from "./MapComponent/MapComponent.vue";
import StopsComponent from "./StopsComponent/StopsComponent.vue";

export default {
  components: {
    MapComponent,
    ArrivalDepartureComponent,
    StopsComponent
  },
  props: ["currentStop"],
  inject: ["externalData", "screenModeChanged"],
  data() {
    return {
      activeTab: "map",
      lat: 47.477, //47.497,
      lng: 19.292 //19.065
    };
  },
  beforeMount: function() {
    if (this.externalData.desktopMode) this.activeTab = "departure";
  },
  created: function() {
    this.screenModeChanged.subscribe({
      next: data => this.handleScreenSizeChange(data),
      error: err => console.error("something wrong occurred: " + err),
      complete: () => console.log("done")
    });
  },
  methods: {
    handleScreenSizeChange(data) {
      if (data.desktopMode && this.activeTab == "map")
        this.activeTab = "departure";
    }
  }
};
</script>

<template src="./MainComponent.html"></template>
<style lang="scss" src="./MainComponent.scss"></style>
