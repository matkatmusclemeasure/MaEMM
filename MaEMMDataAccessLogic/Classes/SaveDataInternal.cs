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

        public void startSaving(string saveInformation_)
        {

            IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

            if(!File.Exists("CalibrationFolder"))
            {
                myIsolatedStorage.CreateDirectory("CalibrationFolder");
                filename = "CalibrationFolder\\Calibrations.txt";
                save(saveInformation_); 
            }
            else if (File.Exists("CalibrationFolder"))
            {
                filename = "CalibrationFolder\\Calibrations.txt";
                save(saveInformation_); 
            }
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
            else if (myIsolatedStorage.FileExists(filename))
            {
                using (StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream(filename, FileMode.Append, FileAccess.Write, myIsolatedStorage)))
                {
                    writeFile.WriteLineAsync(saveInformation_); 
                }
            }
            //https://stackoverflow.com/questions/34385625/saving-files-on-raspberry-pi-with-windows-iot
        }

        public string getLatestCalibration()
        {
            // Load the text block from file

            StreamReader streamReader = new StreamReader(new FileStream(filename, FileMode.Open));
            string latestCalibration = streamReader.ReadLine(); 

            streamReader.Dispose();

            return latestCalibration;
        }
    }
}
