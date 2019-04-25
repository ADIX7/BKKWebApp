<template>
  <div id="mainMap"></div>
</template>
<script>
export default {
  mounted: function() {
    var mainMap = L.map("mainMap").setView([47.497, 19.065], 13);

    var osmUrl='https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
	var osmAttrib='Map data © <a href="https://openstreetmap.org">OpenStreetMap</a> contributors';
	var osm = new L.TileLayer(osmUrl, {minZoom: 8, maxZoom: 12, attribution: osmAttrib});	
	mainMap.addLayer(osm);
  }
};
</script>

<style lang="scss">
#mainMap {
  height: calc(100% - 42px);
}
</style>
