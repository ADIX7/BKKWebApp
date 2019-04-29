<template>
  <span class="routeContainer">
    <span v-bind:class="{autobuszIcon: route.type === 'BUS'}"></span>
    <span v-bind:class="{estiAutobuszIcon: route.type === 'BUS_ESTI'}"></span>
    {{route.name}} 
    <span class="bkkTripArrowTip"></span> 
    {{headsign}}    
  </span>
</template>

<script>
export default {
  props: ["route", "showHeadsign"],
  methods: {
      shouldDrawHeadsign(){
        return this.showHeadsign === false || this.route.headsign === undefined;
      }
  },
  computed: {
    headsign: function() {
      return this.shouldDrawHeadsign()
        ? ""
        : this.route.headsign;
    }
  }
};
</script>

<style lang="scss">
.routeContainer {
  padding-right: 10px;
}
</style>
