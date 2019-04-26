import { Observable } from 'rxjs';

export default {
    initializeSignalRHandlers: function (signalRConnection) {

        let observables = {};
        let methods = {};

        //StopsForLocation
        let StopsForLocationObservers = [];
        let StopsForLocationObservable = Observable.create(observer => {
            StopsForLocationObservers.push(observer);
        });

        signalRConnection.on("StopsForLocation", data => {
            StopsForLocationObservers.forEach(observer => {
                observer.next(data);
            });
        });

        methods.getStopsForLocation = function (lat, lng) {
            signalRConnection.invoke("GetStopsForLocation", lat, lng);
        }

        observables.StopsForLocation = StopsForLocationObservable;

        return {
            observables: observables,
            methods: methods,
            getObservable: handlerName => observables[handlerName]
        };
    }
}