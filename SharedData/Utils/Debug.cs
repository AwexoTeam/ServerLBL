﻿using System;
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
        Log(LogLevel.Minimal, text, true);
    }

    public static void Log(LogLevel logLevel, string text, bool includeParentesis=true, bool isError = false)
    {
        if (isError)
            logLevel = LogLevel.Minimal;

        if(logLevel <= level)
        {
            string write = "";
            if (includeParentesis)
            {
                Console.ForegroundColor = GetColor(logLevel);
                write = "[SERVER]: ";
                Console.Write(write);
            }
            
            Console.ForegroundColor = isError ? ConsoleColor.Red : ConsoleColor.Gray; 
            Console.WriteLine(text);
        }
    }
    public static void LogWithTime(LogLevel logLevel, string text, bool isError = false)
    {
        if (logLevel <= level)
        {
            DateTime currTime = DateTime.Now;
            string paretnesis = '[' + currTime.ToString("T") + "]: ";
            Console.ForegroundColor = GetColor(logLevel);
            Console.Write(paretnesis);

            Log(logLevel, text, false, isError);
        }
    }
    public static void LogWithBacktrack(LogLevel logLevel, string text, bool isError = false)
    {
        if (logLevel <= level)
        {
            MethodBase mth = new StackTrace().GetFrame(1).GetMethod();

            string parentesis = '[' + mth.ReflectedType.Name + "." + mth.Name + "()]: ";
            Console.ForegroundColor = GetColor(logLevel);

            Console.Write(parentesis);
            Log(logLevel, text, false, isError);
        }
    }

    private static ConsoleColor GetColor(LogLevel logLevel)
    {
        ConsoleColor rtn;

        switch (logLevel)
        {
            case LogLevel.Minimal:
                rtn = ConsoleColor.White;
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

    #region Overload for objects log
    public static void Log(object text) { Log(text.ToString()); }
    public static void LogWithTime(LogLevel logLevel, object obj, bool isError = false)
    {
        LogWithTime(logLevel, obj.ToString(), isError);
    }
    public static void LogWithBacktrack(LogLevel logLevel, object obj, bool isError = false)
    {
        LogWithBacktrack(logLevel, obj.ToString(), isError);
    }
    #endregion
    #region TelepathyLogs
    public static void TelepathyLog(string str)
    {
        LogWithTime(LogLevel.Minimal, str);
    }
    public static void TelepathyLogWarning(string str)
    {
        LogWithTime(LogLevel.Verbose, str);
    }
    public static void TelepathyError(string str)
    {
        LogWithTime(LogLevel.Minimal, str, true);
    }
    #endregion
}
