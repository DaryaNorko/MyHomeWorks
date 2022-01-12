using HappyPlants.Interface;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace HappyPlants.Models
{
    public sealed class MyLogger : ILogger
    {
        public static int Counter { get; set; } = 1;        
        public string FullPath { get; set; }
        public MyLogger()
        {
            FullPath = CreateFilePath();
        }       
        private void WriteMessageInFile(string messageLevel)
        {
            if (File.Exists(FullPath))
            {
                FileInfo fileInfo = new(FullPath);

                if (fileInfo.Length > 30_000_000)
                {
                    Counter++;
                    FullPath = CreateFilePath();
                }
            }
            using (StreamWriter streamWriter = new(FullPath, true))
            {
                streamWriter.WriteLine(messageLevel);
            }
        }
        public void Debug(MethodBase methodbase, string message)
        {
            string level = "DEBUG: ";
            string dateTime = DateTime.Now.ToString("G");

            string methodName = methodbase.Name;
            string methodNamespace = methodbase.DeclaringType.FullName;

            int threadId = Thread.CurrentThread.ManagedThreadId;
            string threadPriority = Thread.CurrentThread.Priority.ToString();

            string strDebug = $"{level}{message} {dateTime}. Место вызова метода: namespace и класс - {methodNamespace}, имя метода - {methodName}." +
                $" Данные потока: Id - {threadId}, приоритет - {threadPriority}.";

            WriteMessageInFile(strDebug);
        }
        public void Error(MethodBase methodbase, string message)
        {
            string level = "ERROR: ";
            string dateTime = DateTime.Now.ToString("G");

            string methodName = methodbase.Name;
            string methodNamespace = methodbase.DeclaringType.FullName;

            int threadId = Thread.CurrentThread.ManagedThreadId;
            string threadPriority = Thread.CurrentThread.Priority.ToString();

            string strError = $"{level}{message} {dateTime}. Место вызова метода: namespace и класс - {methodNamespace}, имя метода - {methodName}." +
                $" Данные потока: Id - {threadId}, приоритет - {threadPriority}.";

            WriteMessageInFile(strError);
        }
        public void Info(MethodBase methodbase, string message)
        {
            string level = "INFO: ";
            string dateTime = DateTime.Now.ToString("G");

            string methodName = methodbase.Name;
            string methodNamespace = methodbase.DeclaringType.FullName;

            int threadId = Thread.CurrentThread.ManagedThreadId;
            string threadPriority = Thread.CurrentThread.Priority.ToString();

            string strInfo = $"{level}{message} {dateTime}. Место вызова метода: namespace и класс - {methodNamespace}, имя метода - {methodName}." +
                $" Данные потока: Id - {threadId}, приоритет - {threadPriority}.";

            WriteMessageInFile(strInfo);
        }
        private string CreateFilePath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            var logFolderPath = Path.Combine(currentDirectory, "Logs");

            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            var dateTime = DateTime.Now.Date.ToString("yyyyMMdd_");
            var fileName = $"log {dateTime}_[{Counter}].txt";

            return Path.Combine(logFolderPath, fileName);
        }
    }
}
