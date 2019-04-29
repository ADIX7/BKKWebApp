<script>
import HeaderComponent from "../HeaderComponent.vue";
import MainComponent from "../MainComponent/MainComponent.vue";
import SidebarComponent from "../Sidebar/Sidebar.vue";
import StopComponent from "../StopComponent/StopComponent.vue";
import TripComponent from "../TripComponent/TripComponent.vue";

export default {
  components: {
    HeaderComponent,
    MainComponent,
    SidebarComponent,
    StopComponent,
    TripComponent
  },
  data() {
    return {
      currentView: "main",
      currentStop: "",
      currentTrip: "",
      title: "Main page"
    };
  },
  methods: {
    navigateToStop(event) {
      this.currentStop = event;
      this.currentView = "stop";
    },
    navigateToTrip(event) {
      this.currentTrip = event;
      this.currentView = "trip";
    }
  }
};
</script>

<template src="./AppComponent.html"></template>

<style lang="scss">
</style>


