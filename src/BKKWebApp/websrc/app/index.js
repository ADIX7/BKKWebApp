import Vue from 'vue';
import AppComponent from './components/AppComponent/AppComponent.vue';
import signalRHandler from './SignalRHandlers';
const signalR = require("@aspnet/signalr");

import "bootstrap";
import "leaflet";

var signalRConnection = new signalR.HubConnectionBuilder().withUrl("/apiHub").build();
let signalRApi = signalRHandler.initializeSignalRHandlers(signalRConnection);
signalRConnection.start();

new Vue({
  el: '#app',
  render: h => h(AppComponent),
  provide: function () {
    return {
      signalRApi: signalRApi
    }
  }
});

