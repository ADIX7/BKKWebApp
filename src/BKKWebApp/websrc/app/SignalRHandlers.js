import { Observable } from 'rxjs';
export default {
    initializeSignalRHandlers: function (signalRConnection) {

        let observables = {};
        let methods = {};

        //===GetArrivalsAndDeparturesForLocation System.Single lat System.Single lng===
        let ArrivalsAndDeparturesForLocationObservers = [];

        let ArrivalsAndDeparturesForLocationObservable = Observable.create(observer => {
            ArrivalsAndDeparturesForLocationObservers.push(observer);
        });

        signalRConnection.on("ArrivalsAndDeparturesForLocation", data => {
            ArrivalsAndDeparturesForLocationObservers.forEach(observer => {
                observer.next(data);
            });
        });

        methods.getArrivalsAndDeparturesForLocation = function (lat, lng) {
            signalRConnection.invoke("GetArrivalsAndDeparturesForLocation", lat, lng);
        }

        observables.ArrivalsAndDeparturesForLocation = ArrivalsAndDeparturesForLocationObservable;
        //===GetArrivalsAndDeparturesForLocation END===


        //===GetStopsForLocation System.Single lat System.Single lng===
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
        //===GetStopsForLocation END===


        //===GetArrivalsAndDeparturesForStop System.String stopId===
        let ArrivalsAndDeparturesForStopObservers = [];

        let ArrivalsAndDeparturesForStopObservable = Observable.create(observer => {
            ArrivalsAndDeparturesForStopObservers.push(observer);
        });

        signalRConnection.on("ArrivalsAndDeparturesForStop", data => {
            ArrivalsAndDeparturesForStopObservers.forEach(observer => {
                observer.next(data);
            });
        });

        methods.getArrivalsAndDeparturesForStop = function (stopId) {
            signalRConnection.invoke("GetArrivalsAndDeparturesForStop", stopId);
        }

        observables.ArrivalsAndDeparturesForStop = ArrivalsAndDeparturesForStopObservable;
        //===GetArrivalsAndDeparturesForStop END===


        return {
            observables: observables,
            methods: methods,
            getObservable: handlerName => observables[handlerName],
            signalRConnection: signalRConnection
        };
    }
}
