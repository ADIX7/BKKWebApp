#!/usr/bin/env dotnet-script
#r "nuget: Newtonsoft.Json, 12.0.1"

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static string GetScriptFolder([CallerFilePath] string path = null) => Path.GetDirectoryName(path);

public static string FirstLetterToUpperCase(this string s)
{
    if (string.IsNullOrEmpty(s))
        throw new ArgumentException("There is no first letter");

    char[] a = s.ToCharArray();
    a[0] = char.ToUpper(a[0]);
    return new string(a);
}
public static string FirstLetterToLowerCase(this string s)
{
    if (string.IsNullOrEmpty(s))
        throw new ArgumentException("There is no first letter");

    char[] a = s.ToCharArray();
    a[0] = char.ToLower(a[0]);
    return new string(a);
}

public static string GetTypeText(string type)
{
    return type;
}

interface INameProvider
{
    string GetName();
    string GetPastName();
}

class VerbSubjectNameProvider : INameProvider
{
    string Verb;
    string Subject;
    public VerbSubjectNameProvider(string verb, string subject)
    {
        Verb = verb.FirstLetterToUpperCase();
        Subject = subject.FirstLetterToUpperCase();
    }
    public string GetName()
    {
        return Verb + Subject;
    }

    public string GetPastName()
    {
        return Subject + Verb + (Verb.EndsWith("e") ? "d" : "ed");
    }
}

class SimpleNameProvider : INameProvider
{
    string Name;
    public SimpleNameProvider(string name)
    {
        Name = name.FirstLetterToUpperCase();
    }

    public string GetName()
    {
        return Name;
    }

    public string GetPastName()
    {
        return Name + "Completed";
    }
}

var folderName = GetScriptFolder();

var dataDir = new DirectoryInfo(Path.Combine(folderName, "../../Metadata/events"));

foreach (var jsonFile in dataDir.GetFiles("*.json"))
{
    Console.WriteLine(jsonFile.FullName);
    var jobject = JObject.Parse(File.ReadAllText(jsonFile.FullName));
    foreach (var child in jobject.Root)
    {
        var classData = ((JProperty)child).Value;

        var name = classData["name"]?.ToString();
        var verb = classData["verb"]?.ToString();
        var subject = classData["subject"]?.ToString();

        INameProvider nameProvider = name == null ? (INameProvider)new VerbSubjectNameProvider(verb, subject) : new SimpleNameProvider(name);

        //Event
        var targetFileName = Path.Combine(folderName, "../Data/Events", nameProvider.GetPastName() + "Event.cs");
        var Output = new StreamWriter(targetFileName);
        List<string> parameters = new List<string>();

        Output.WriteLine($@"using System;
using BKKWebApp.Data.Base;

namespace BKKWebApp.Data.Events
{{
    public class {nameProvider.GetPastName()}Event : Event
    {{");

        foreach (var prop in classData["properties"])
        {
            var propName = prop["name"].ToString();
            var type = GetTypeText(prop["type"].ToString());

            Output.WriteLine($"\t\tpublic {type} {propName.FirstLetterToUpperCase()} {{ get; set; }}");
            parameters.Add(type + " " + propName.FirstLetterToLowerCase());
        }

        Output.WriteLine($@"
        public {nameProvider.GetPastName()}Event() {{ }}
        public {nameProvider.GetPastName()}Event(Guid aggregateId, int version{(parameters.Count == 0 ? "" : ", " + string.Join(", ", parameters))}) : base(aggregateId, version)
        {{");

        foreach (var prop in classData["properties"])
        {
            var propName = prop["name"].ToString();
            var type = GetTypeText(prop["type"].ToString());

            Output.WriteLine($"\t\t\t{propName.FirstLetterToUpperCase()} = {propName.FirstLetterToLowerCase()};");
        }

        Output.WriteLine($@"        }}
    }}
}}");
        Output.Close();

        //Commands
        parameters = new List<string>();
        targetFileName = Path.Combine(folderName, "../Data/Commands", nameProvider.GetName() + ".cs");
        Output = new StreamWriter(targetFileName);
        Output.WriteLine($@"using System;
using BKKWebApp.Data.Base;

namespace BKKWebApp.Data.Commands
{{
    public class {nameProvider.GetName()} : Command
    {{");

        foreach (var prop in classData["properties"])
        {
            var propName = prop["name"].ToString();
            var type = GetTypeText(prop["type"].ToString());

            Output.WriteLine($"\t\tpublic readonly {type} {propName.FirstLetterToUpperCase()};");
            parameters.Add(type + " " + propName.FirstLetterToLowerCase());
        }

        Output.WriteLine($@"
        public {nameProvider.GetName()}({(parameters.Count == 0 ? "" : string.Join(", ", parameters))})
        {{");

        foreach (var prop in classData["properties"])
        {
            var propName = prop["name"].ToString();
            var type = GetTypeText(prop["type"].ToString());

            Output.WriteLine($"\t\t\t{propName.FirstLetterToUpperCase()} = {propName.FirstLetterToLowerCase()};");
        }

        Output.WriteLine($@"        }}
    }}
}}");
        Output.Close();
    }
}
