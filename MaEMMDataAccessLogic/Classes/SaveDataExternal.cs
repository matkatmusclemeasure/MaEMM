using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MaEMMDataAccessLogic
{
    class SaveDataExternal : ISaveData
    {
        public string getLatestCalibration()
        {
            throw new NotImplementedException();
        }

        public async void save(string testInformation_)
        {
            StorageFolder UsbDrive = (await Windows.Storage.KnownFolders.RemovableDevices.GetFoldersAsync()).FirstOrDefault();

            if (UsbDrive == null)
            {
                //System.Diagnostics.Debug.WriteLine("USB Drive not found");
            }
            else
            {   StorageFolder TestDataFolder = ApplicationData.Current.LocalFolder;
                StorageFile TestDataFile = await TestDataFolder.CreateFileAsync("MyFolder\\MyFile.txt", CreationCollisionOption.OpenIfExists);

                if (TestDataFile != null)
                {
                    await FileIO.AppendTextAsync(TestDataFile, testInformation_);
                }
            }
        }

        public void startSaving(string saveInformation_)
        {
            throw new NotImplementedException();
        }
    }
}
//https://iot-developer.net/windows-iot/uwp-programming-in-c/files/text-files
//https://docs.microsoft.com/en-us/uwp/api/Windows.Storage.KnownFolders?redirectedfrom=MSDN
