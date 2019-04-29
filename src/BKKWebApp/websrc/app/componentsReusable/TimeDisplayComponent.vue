<template>
  <span>
    <span v-if="route.arrivalDepartureTime.timeAfter() < 60">
      <a
        v-if="route.arrivalDepartureTime.isPredicted"
        href="#"
        v-on:click="$emit('route-clicked', route.tripId)"
      >{{route.arrivalDepartureTime.timeAfter()}}'</a>
      <span v-else>{{route.arrivalDepartureTime.timeAfter()}}'</span>
    </span>
    <span v-else>{{route.arrivalDepartureTime.timeAt()}}</span>
  </span>
</template>
<script>
export default {
  props: ["route"]
};
</script>

