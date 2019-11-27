using System;
using System.IO;

namespace TriAngleUi.Common
{
    public class FileRead
    {
        public static string[] Read(string filePath)=>
            File.ReadAllLines(Environment.CurrentDirectory + @"\" + filePath);
    }
}