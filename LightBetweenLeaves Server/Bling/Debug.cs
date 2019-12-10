using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public enum LogLevel
{
    Minimal,
    Default,
    Verbose,
    Debug,
}

public static class Debug
{
    public static LogLevel level = LogLevel.Debug;

    public static void Log(string text)
    {
        Console.WriteLine(text);
    }

    public static void Log(LogLevel logLevel, string text, bool includeParentesis=true, bool isError = false)
    {
        if(logLevel >= level)
        {
            
            string write = "";
            if (includeParentesis)
            {
                Console.ForegroundColor = GetColor(logLevel);
                write = "[SERVER]: ";
                Console.Write(write);
            }
            
            Console.ForegroundColor = isError ? ConsoleColor.Red : ConsoleColor.Gray; 
            Console.Write(text);
            Console.Write(Environment.NewLine);
        }
    }

    public static void LogWithTime(LogLevel logLevel, string text, bool isError = false)
    {
        DateTime currTime = DateTime.Now;
        string paretnesis = '[' + currTime.ToString("T") + "]: ";
        Console.ForegroundColor = GetColor(logLevel);
        Console.Write(paretnesis);

        Log(logLevel, text, false, isError);
    }

    public static void LogWithBacktrack(LogLevel logLevel, string text, bool isError = false)
    {
        MethodBase mth = new StackTrace().GetFrame(1).GetMethod();
        
        string parentesis = '[' + mth.ReflectedType.Name + "." + mth.Name + "()]: ";
        Console.ForegroundColor = GetColor(logLevel);

        Console.Write(parentesis);
        Log(logLevel, text, false, isError);
    }

    private static ConsoleColor GetColor(LogLevel logLevel)
    {
        ConsoleColor rtn;

        switch (logLevel)
        {
            case LogLevel.Minimal:
                rtn = ConsoleColor.Gray;
                break;
            case LogLevel.Default:
                rtn = ConsoleColor.White;
                break;
            case LogLevel.Verbose:
                rtn = ConsoleColor.Green;
                break;
            case LogLevel.Debug:
                rtn = ConsoleColor.Magenta;
                break;
            default:
                rtn = ConsoleColor.White;
                break;
        }

        return rtn;
    }
}
