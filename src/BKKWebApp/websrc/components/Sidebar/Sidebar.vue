<script>
export default {
  data() {
    return {
      isOpen: false
    };
  },
  methods: {
    openSideBar() {
      document.getElementById("mainSideBar").style.width = "250px";
    },
    closeSideBar() {
      document.getElementById("mainSideBar").style.width = "0";
    },
    toggleSideBar(){
        if(this.isOpen){
            this.closeSideBar();
            this.isOpen = false;
        }
        else {
            this.openSideBar();
            this.isOpen = true;
        }
    }
  }
};
</script>

<template src="./Sidebar.html"></template>
<style lang="scss" src="./Sidebar.scss"></style>
