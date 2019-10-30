using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaEMMDataAccessLogic
{
    class SaveData : ISaveData
    {

        public void saveDatastring(string dataArranged)
        {
            if(File.Exists) //her skal stå noget andet for at se om filen eksisterer på SD-kortet 
            {
                Stream s = File.Open(path, FileMode.Append); //sti skal tilføjes
                StreamWriter sw = new StreamWriter(s);

                sw.WriteLine(dataArranged);
            }
            else
            {
                Stream s = File.Open(path, FileMode.CreateNew); //sti skal tilføjes 
                StreamWriter sw = new StreamWriter(s);

                sw.WriteLine(dataArranged);
            }
           
        }
    }
}
