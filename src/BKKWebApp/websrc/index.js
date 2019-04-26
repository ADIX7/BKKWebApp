import Vue from 'vue';
import AppComponent from './components/AppComponent/AppComponent.vue';
import signalRHandler from './SignalRHandlers';
const signalR = require("@aspnet/signalr");

import "bootstrap";
import "leaflet";

var signalRConnection = new signalR.HubConnectionBuilder().withUrl("/apiHub").build();

let signalRApi = signalRHandler.initializeSignalRHandlers(signalRConnection);
/*let handler = signalRApi.getObservable("StopsForLocation");

handler.subscribe(
  {
    next: data => console.log(data),
    error: err => console.error('something wrong occurred: ' + err),
    complete: () => console.log('done'),
  }
);*/

signalRConnection.start();
  //.then(() => signalRApi.methods.getStopsForLocation(47.477, 19.292));

new Vue({
  el: '#app',
  render: h => h(AppComponent),
  provide: function () {
    return {
      signalRApi: signalRApi
    }
  }
});

