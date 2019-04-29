<template>
  <div id="mainMap"></div>
</template>
<script>
export default {
  props: ["lat", "lng"],
  mounted: function() {
    let _this = this;

    var mainMap = L.map("mainMap").setView([this.lat, this.lng], 13);

    var osmUrl = "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png";
    var osmAttrib =
      'Map data © <a href="https://openstreetmap.org">OpenStreetMap</a> contributors';
    var osm = new L.TileLayer(osmUrl, {
      minZoom: 13,
      maxZoom: 30,
      attribution: osmAttrib
    });
    mainMap.addLayer(osm);

    mainMap.on("moveend", function(e) {
      let center = mainMap.getCenter();

      _this.$emit("update:lat", center.lat);
      _this.$emit("update:lng", center.lng);
    });
  }
};
</script>

<style lang="scss">
@import "../../../css/common";

#mainMap {
  height: 100%;
}
</style>
