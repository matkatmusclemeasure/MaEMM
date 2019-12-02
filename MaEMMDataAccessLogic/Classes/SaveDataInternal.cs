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
        private string filename; 

        public void createDirectory(string saveInformation_)
        {
            IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            myIsolatedStorage.CreateDirectory("TextFilesFolder");
            filename = "TextFilesFolder\\Samplefile.txt";
            save(saveInformation_);
        }


        public void save(string saveInformation_)
        {
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
