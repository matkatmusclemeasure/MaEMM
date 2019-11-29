using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MaEMMDataAccessLogic
{
    public class SaveData : ISaveData
    {
        private string saveInformation;
        private string filename; 

        //public async void saveDatastring(string dataArranged)
        //{
        //    if (File.Exists("")) //her skal stå noget andet for at se om filen eksisterer på SD-kortet 
        //    {
        //        Stream s = File.Open(path, FileMode.Append); //sti skal tilføjes
        //        StreamWriter sw = new StreamWriter(s);

        //        sw.WriteLine(dataArranged);
        //    }
        //    else
        //    {
        //        Stream s = File.Open(path, FileMode.CreateNew); //sti skal tilføjes 
        //        StreamWriter sw = new StreamWriter(s);

        //        sw.WriteLine(dataArranged);
        //    }
        //}

        public void createDirectory(string saveInformation_)
        {
            IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            myIsolatedStorage.CreateDirectory("TextFilesFolder");
            filename = "TextFilesFolder\\Samplefile.txt";
            save(saveInformation_);
        }


        public void save(string saveInformation_)
        {
            //var removableDevices = KnownFolders.RemovableDevices;
            //var externalDrives = await removableDevices.GetFoldersAsync();
            //var drive0 = externalDrives[0];

            //var Folder = await drive0.CreateFolderAsync("Calibration");
            //var File = await Folder.CreateFileAsync("Calibrationfile.txt");

            IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

            if (!myIsolatedStorage.FileExists(filename))
            {
                using (StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream(filename, FileMode.Create, FileAccess.Write, myIsolatedStorage)))
                {
                    writeFile.WriteLine(saveInformation_);
                    // writeFile.Close();
                }


            }

            //https://stackoverflow.com/questions/34385625/saving-files-on-raspberry-pi-with-windows-iot
        }
    }
}
