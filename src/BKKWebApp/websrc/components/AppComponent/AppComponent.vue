<script>
import HeaderComponent from "../HeaderComponent.vue";
import MainComponent from "../MainComponent/MainComponent.vue";
import SidebarComponent from "../Sidebar/Sidebar.vue";
import "bootstrap";
import "leaflet";

export default {
  components: {
    HeaderComponent,
    MainComponent,
    SidebarComponent
  },
  data() {
    return {
      currentView: "browse",
      title: 'Main page'
    };
  },
  methods: {
  }  
};
</script>

<template src="./AppComponent.html">  
</template>

<style lang="scss">

</style>


