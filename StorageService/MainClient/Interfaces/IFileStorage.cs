using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MainClient.Interfaces
{
    public interface IFileStorage
    {
        IEnumerable<Uri> ListContents();
    }
}
