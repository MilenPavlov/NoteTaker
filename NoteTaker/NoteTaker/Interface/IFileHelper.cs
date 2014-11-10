using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTaker.Interface
{
    public interface IFileHelper
    {
        Task<bool> ExistsAsync(string filename );

        Task WriteTextAsync(string filename, string text);

        Task<string> ReadTextAsync(string filename);

        Task<IEnumerable<string>> GetFilesAsync();
        Task DeleteFileAsync(string filename);
    }
}
