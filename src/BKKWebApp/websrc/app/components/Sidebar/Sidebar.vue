<script>
var $ = require("jquery");
export default {
  props: ["current-view"],
  data() {
    return {
      isOpen: false
    };
  },
  methods: {
    openSideBar() {
      document.getElementById("mainSideBar").style.width = "250px";
      this.isOpen = true;
    },
    closeSideBar() {
      document.getElementById("mainSideBar").style.width = "0";
      this.isOpen = false;
    },
    toggleSideBar() {
      if (this.isOpen) {
        this.closeSideBar();
        this.isOpen = false;
      } else {
        this.openSideBar();
        this.isOpen = true;
      }
    },
    openView(view) {
      this.$emit("open-view", view);
      this.closeSideBar();
    }
  },
  mounted: function() {
    $("#_antiForgeryHolder").children("input").appendTo("#logoutForm");
    $("#_antiForgeryHolder").remove();
  },
  computed: {
    userName: function() {
      return userName;
    }
  }
};
</script>

<template src="./Sidebar.html"></template>
<style lang="scss" src="./Sidebar.scss"></style>
