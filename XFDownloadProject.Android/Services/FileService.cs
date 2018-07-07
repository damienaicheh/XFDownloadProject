using System;
using XFDownloadProject.Services.Interfaces;

namespace XFDownloadProject.Droid.Services
{
	public class FileService : IFileService
    {
        public string GetStorageFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}
