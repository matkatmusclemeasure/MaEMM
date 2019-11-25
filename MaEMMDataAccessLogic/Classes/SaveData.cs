using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MaEMMDataAccessLogic
{
    class SaveData : ISaveData
    {

        public async void saveDatastring(string dataArranged)
        {
            //Nedenstående skulle oprette en tekstfil på usb drev
            // derudover er der tilføjet en file type association under app manifest. 
            var removableDevices = KnownFolders.RemovableDevices;
            var externalDrives = await removableDevices.GetFoldersAsync();
            var drive0 = externalDrives[0];

            var logFolder = await drive0.CreateFolderAsync("Log");
            var logFile = await logFolder.CreateFileAsync("Log.txt");

            var byteArray = new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77 };
            using (var sourceStream = new MemoryStream(byteArray).AsRandomAccessStream())
            {
                using (var destinationStream = (await logFile.OpenAsync(FileAccessMode.ReadWrite)).GetOutputStreamAt(0))
                {
                    await Windows.Storage.Streams.RandomAccessStream.CopyAndCloseAsync(sourceStream, destinationStream);
                }
            }

            if (File.Exists("")) //her skal stå noget andet for at se om filen eksisterer på SD-kortet 
            {
                //Stream s = File.Open(path, FileMode.Append); //sti skal tilføjes
                //StreamWriter sw = new StreamWriter(s);

                //sw.WriteLine(dataArranged);
            }
            else
            {
                //Stream s = File.Open(path, FileMode.CreateNew); //sti skal tilføjes 
                //StreamWriter sw = new StreamWriter(s);

                //sw.WriteLine(dataArranged);
            }
        }
    }
}
