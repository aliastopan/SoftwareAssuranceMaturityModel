using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareAssuranceMaturityModel.Application.Common.Helpers
{
    public class Marshal
    {
        public static string GetDataStorage(string file, string depthPath)
        {
            string directory = ".datastorage";
            string storage = Directory.GetCurrentDirectory();
            storage = Path.GetFullPath(Path.Combine(storage, depthPath));

            return Path.GetFullPath(Path.Combine(storage, directory, file));
        }
    }
}