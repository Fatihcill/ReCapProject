using System;
using System.IO;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.FileHelper
{
    public class FileHelper
    {
        private static string _wwwRoot = Environment.CurrentDirectory + @"\wwwroot";
        
        public static Tuple<string, string> SaveImageFile(string fileName,IFormFile extension)
        {
            string imageExtension = Path.GetExtension(extension.FileName); 
            string newImageName = string.Format("{0:D}{1}", Guid.NewGuid(), imageExtension);
            string imageDirectory = Path.Combine(_wwwRoot, fileName);
            string FullImagePath = Path.Combine(imageDirectory, newImageName); 
            string webImagePath= string.Format("/"+ fileName + "/{0}", newImageName);
            if(!Directory.Exists(imageDirectory))
                Directory.CreateDirectory(imageDirectory);

            using (var fileStream = File.Create(FullImagePath))
            {
                extension.CopyTo(fileStream);
                fileStream.Flush();
            }
            return Tuple.Create(FullImagePath,webImagePath);
        }

        public static bool DeleteImageFile(string fileName)
        {
            string fullPath = Path.Combine(fileName);
            if (File.Exists(_wwwRoot + fullPath))
            {
                File.Delete(_wwwRoot + fullPath);
                return true;
            }
            return false;
        }
    

    }
}