using XFDownloadProject.Services.Interfaces;

namespace XFDownloadProject.UWP.Services
{
    public class FileService : IFileService
    {
        public string GetStorageFolderPath()
        {
            return Windows.Storage.ApplicationData.Current.LocalFolder.Path;
        }
    }
}