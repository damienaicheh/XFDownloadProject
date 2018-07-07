using System;
using System.IO;
using XFDownloadProject.Services.Interfaces;

namespace XFDownloadProject.iOS.Services
{
	public class FileService : IFileService
    {
        public string GetStorageFolderPath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library");
            return libFolder;
        }
    }
}
