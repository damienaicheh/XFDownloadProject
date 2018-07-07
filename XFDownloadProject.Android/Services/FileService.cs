using System;
using System.IO;
using XFDownloadProject.Services.Interfaces;

namespace XFDownloadProject.Droid.Services
{
	public class FileService : IFileService
    {
        public string GetStorageFolderPath()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            System.Diagnostics.Debug.WriteLine(path);
            return path;
        }
    }
}
