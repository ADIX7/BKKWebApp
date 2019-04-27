#!/usr/bin/env dotnet-script

#r "../../bin/Debug/netcoreapp2.2/BKKWebApp.dll"

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using BKKWebApp.Hubs;

public static string GetScriptFolder([CallerFilePath] string path = null) => Path.GetDirectoryName(path);
public static string GetScriptFile([CallerFilePath] string path = null) => Path.GetFileName(path);


var folderName = GetScriptFolder();
var fileName = GetScriptFile();

var targetFileName = Path.Combine(folderName, fileName.Replace(".csx", ".js"));
var Output = new StreamWriter(targetFileName);
Output.WriteLine(@"import { Observable } from 'rxjs';
export default {
    initializeSignalRHandlers: function (signalRConnection) {

        let observables = {};
        let methods = {};");
var serverType = typeof(IApiHubServerFunctions);

foreach (var method in serverType.GetMethods())
{
    var methodName = method.Name.Substring(3);

    var parameters = string.Join(", ", method.GetParameters().Select(p => p.Name));

    Output.WriteLine($@"
        //==={method.Name} {string.Join(" ", method.GetParameters().Select(p => p.ParameterType.ToString() + " " + p.Name))}===
        let {methodName}Observers = [];

        let {methodName}Observable = Observable.create(observer => {{
            {methodName}Observers.push(observer);
        }});

        signalRConnection.on(""{methodName}"", data => {{
            {methodName}Observers.forEach(observer => {{
                observer.next(data);
            }});
        }});

        methods.get{methodName} = function ({parameters}) {{
            signalRConnection.invoke(""Get{methodName}"", {parameters});
        }}

        observables.{methodName} = {methodName}Observable;
        //==={method.Name} END===
");
}

Output.WriteLine(@"
        return {
            observables: observables,
            methods: methods,
            getObservable: handlerName => observables[handlerName]
        };
    }
}");
Output.Close();