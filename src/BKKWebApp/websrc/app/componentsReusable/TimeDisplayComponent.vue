<template>
  <span>
    <span class="wheelChairImage" v-if="route.wheelchairAccessible"></span>
    <span v-if="route.arrivalDepartureTime.timeAfter() < 60">
      <a
        v-if="route.arrivalDepartureTime.isPredicted"
        href="#"
        v-on:click="$emit('route-clicked', route.tripId)"
      >{{getTimeMessage(route.arrivalDepartureTime.timeAfter())}}</a>
      <span v-else>{{getTimeMessage(route.arrivalDepartureTime.timeAfter())}}</span>
    </span>
    <span v-else>{{route.arrivalDepartureTime.timeAt()}}</span>
  </span>
</template>
<script>
export default {
  props: ["route"],
  methods: {
    getTimeMessage(time) {
      if (time == 0) return "MOST";
      else return time + "'";
    }
  }
};
</script>

