using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helper.FileHelper
{
    public interface IFileService
    {
        string UploadFile(IFormFile file,string root);
        string Update(IFormFile file,string filePath,string root);
        void Delete(string filePath);  
        
    }
}
