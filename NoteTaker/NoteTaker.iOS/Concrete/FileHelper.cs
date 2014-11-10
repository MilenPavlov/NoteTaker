using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(NoteTaker.iOS.Concrete.FileHelper)) ]

namespace NoteTaker.iOS.Concrete
{
    public class FileHelper : IFileHelper
    {
        public Task<bool> ExistsAsync(string filename)
        {
            var exists = File.Exists(GetFilePath(filename));
            return Task.FromResult(exists);
        }

        private string GetFilePath(string filename)
        {          
            return Path.Combine(GetDocsFolder(), filename);
        }

        public async Task WriteTextAsync(string filename, string text)
        {
            var filePath = GetFilePath(filename);
            using (var writer = File.CreateText(filePath))
            {
                await writer.WriteAsync(text);
            }
        }

        public async Task<string> ReadTextAsync(string filename)
        {
            var filepath = GetFilePath(filename);
            using (var reader = File.OpenText(filepath))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public Task DeleteFileAsync(string filename)
        {
            File.Delete(GetFilePath(filename));
            return Task.FromResult(true);
        }

        public Task<IEnumerable<string>> GetFilesAsync()
        {
            var filenames =
                from filepath in Directory.EnumerateFiles(GetDocsFolder())
                select Path.GetFileName(filepath);

            return Task.FromResult(filenames);
        }

        private string GetDocsFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
    }
}
