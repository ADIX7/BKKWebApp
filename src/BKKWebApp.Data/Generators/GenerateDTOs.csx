#!/usr/bin/env dotnet-script

#r "../bin/Debug/netstandard2.0/BKKWebApp.Data.dll"

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using BKKWebApp.Data.Models;

public static string GetScriptFolder([CallerFilePath] string path = null) => Path.GetDirectoryName(path);

public static string GetTypeText(Type type)
{
    if (typeof(IList).IsAssignableFrom(type) && type.IsGenericType)
    {
        return "List<" + GetTypeText(type.GenericTypeArguments[0]) + ">";
    }
    else if (typeof(IEnumerable).IsAssignableFrom(type) && type.IsGenericType)
    {
        return "IEnumerable<" + GetTypeText(type.GenericTypeArguments[0]) + ">";
    }
    else if (type.IsArray)
    {
        return GetTypeText(type.GetElementType()) + "[]";
    }
    else if (type == typeof(string))
    {
        return "string";
    }
    else if (type == typeof(bool))
    {
        return "boolean";
    }
    else if (type == typeof(int) || type == typeof(float) || type == typeof(double))
    {
        return "number";
    }

    return type.Name;
}

var folderName = GetScriptFolder();

var ensureLoad = new Type[] { typeof(User) };

var modelTypes = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName.Contains("BKKWebApp")).GetTypes().Where(t => t.FullName.Contains("BKKWebApp.Data.Models"));

foreach (Type type in modelTypes)
{
    var targetFileName = Path.Combine(folderName, "../Data/ModelDtos", type.Name + "Dto.cs");
    var Output = new StreamWriter(targetFileName);
    Output.WriteLine($@"using System;
using System.Collections.Generic;

namespace BKKWebApp.Data.ModelDtos
{{
    public class {type.Name}Dto
    {{");

    foreach (PropertyInfo prop in type.GetProperties().Where(p => p.DeclaringType == type))
    {
        Output.WriteLine($"\t\tpublic {GetTypeText(prop.PropertyType)} {prop.Name} {{ get; set; }}");
    }

    Output.WriteLine($@"    }}
}}");
    Output.Close();
}