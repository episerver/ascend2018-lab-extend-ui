using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using PCLStorage;

namespace EPiServerContentApi.App
{
    public static class SiteManager
    {
        public static async Task<string> GetSitePath()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("site", CreationCollisionOption.ReplaceExisting).ConfigureAwait(false);
            
            await Unzip(folder);
            var test = await folder.GetFilesAsync();
            
            
            var htmlFile = await folder.GetFileAsync("dist/index.html").ConfigureAwait(false);
            var resultingPath = htmlFile.Path;
            return resultingPath;
        }

        private static async Task Unzip(IFolder folder)
        {
            try
            {
                var assembly = typeof(MainPage).GetTypeInfo().Assembly;
                var stream = assembly.GetManifestResourceStream("EPiServerContentApi.App.Site.dist.zip");
                
                    using (var zipArchive = new ZipFile(stream))
                    {
                        foreach (ZipEntry zipEntry in zipArchive)
                        {
                            if (!zipEntry.IsFile)
                            {
                                continue;           // Ignore directories
                            }

                            String entryFileName = zipEntry.Name;
                            // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                            // Optionally match entrynames against a selection list here to skip as desired.
                            // The unpacked length is available in the zipEntry.Size property.

                            byte[] buffer = new byte[4096]; // 4K is optimum
                            Stream zipStream = zipArchive.GetInputStream(zipEntry);

                            // Manipulate the output filename here as desired.
                            String fullZipToPath = Path.Combine(folder.Path, entryFileName);
                            string directoryName = Path.GetDirectoryName(fullZipToPath);

                            var folderForFile = folder;

                            if (directoryName.Length > 0)
                            {
                                folderForFile = await folder.CreateFolderAsync(directoryName, CreationCollisionOption.OpenIfExists).ConfigureAwait(false);
                            }

                            string fileName = Path.GetFileName(entryFileName);

                            var fileToWrite = await folderForFile.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting).ConfigureAwait(false);

                            using (var fileWriteStream = await fileToWrite.OpenAsync(FileAccess.ReadAndWrite).ConfigureAwait(false))
                            {
                                StreamUtils.Copy(zipStream, fileWriteStream, buffer);
                            }
                        }
                    }

            }
            catch (Exception e)
            {

            }
        }
    }
}
