import Vue from 'vue'
import AppComponent from './components/AppComponent/AppComponent.vue'
import "bootstrap";
import "leaflet";

new Vue({
  el: '#app',
  render: h => h(AppComponent)
});

