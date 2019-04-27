<script>
import HeaderComponent from "../HeaderComponent.vue";
import MainComponent from "../MainComponent/MainComponent.vue";
import SidebarComponent from "../Sidebar/Sidebar.vue";
import StopComponent from "../StopComponent/StopComponent.vue";

export default {
  components: {
    HeaderComponent,
    MainComponent,
    SidebarComponent,
    StopComponent
  },
  data() {
    return {
      currentView: "main",
      currentStop: "",
      title: "Main page"
    };
  },
  methods: {
    navigateToStop(event) {
      this.currentStop = event;
      this.currentView = "stop";
    }
  }
};
</script>

<template src="./AppComponent.html"></template>

<style lang="scss">
</style>


