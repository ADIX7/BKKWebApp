import Vue from 'vue';
import AppComponent from './components/AppComponent/AppComponent.vue';
import signalRHandler from './SignalRHandlers';
import { Observable } from 'rxjs';
const signalR = require("@aspnet/signalr");

import "bootstrap";
import "leaflet";

//SignalR init
var signalRConnection = new signalR.HubConnectionBuilder().withUrl("/apiHub").build();
let signalRApi = signalRHandler.initializeSignalRHandlers(signalRConnection);
signalRConnection.start();


//Responsive UI
let vueData = {
  desktopMode: true
}

var vm = new Vue({
  el: '#app',
  render: h => h(AppComponent),
  data() {
    return {
      externalData: vueData
    };
  },
  provide: function () {
    return {
      signalRApi: signalRApi,
      externalData: this.externalData,
      screenModeChanged: this.$options.screenModeChanged
    }
  },
  screenModeChanged: null,
  screenModeChangedObservers: [],
  beforeCreate: function () {
    let _this = this;
    this.$options.screenModeChanged = Observable.create(observer => {
      _this.$options.screenModeChangedObservers.push(observer);
    });
  }
});


function screenWidthHandler(data) {
  if (data.matches) {
    vueData.desktopMode = false;
  }
  else {
    vueData.desktopMode = true;
  }

  vm.$options.screenModeChangedObservers.forEach(observer => {
    observer.next({ desktopMode: vueData.desktopMode });
  });
}

var screenWidthChecker = window.matchMedia("(max-width: 1000px)");
screenWidthChecker.addListener(screenWidthHandler);
screenWidthHandler(screenWidthChecker);