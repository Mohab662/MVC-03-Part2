using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace MVC_03.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\", FolderName);

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            string filePath = Path.Combine(folderPath, fileName);

            using var fileStreams = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStreams);

            return fileName;

        }

        public static void DeleteFile(string FileName, string FolderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\", FolderName, FileName);

            if (File.Exists(filePath)) 
            {
                File.Delete(filePath);
            }
        }
    }
}
