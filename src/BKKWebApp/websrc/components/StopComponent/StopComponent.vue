<template>
  <StopArrivalDepartureComponent v-bind:currentStop="currentStop"></StopArrivalDepartureComponent>
</template>

<script>
import StopArrivalDepartureComponent from "./StopArrivalDepartureComponent/StopArrivalDepartureComponent.vue";
export default {
  components: { StopArrivalDepartureComponent },
  props: ["currentStop"],
  data() {
    return {
      activeTab: ""
    };
  }
};
</script>
