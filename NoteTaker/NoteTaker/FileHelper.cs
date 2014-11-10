﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Interface;
using Xamarin.Forms;

namespace NoteTaker
{
    static class FileHelper
    {
        private static IFileHelper fileHelper = DependencyService.Get<IFileHelper>();

        public static Task<bool> ExistsAsync(string filename)
        {
           return fileHelper.ExistsAsync(filename);
        }
        public static Task WriteTextAsync(string filename, string text)
        {
           return fileHelper.WriteTextAsync(filename, text);
        }
        public static Task<string> ReadTextAsync(string filename)
        {
           return fileHelper.ReadTextAsync(filename);
        }

        public static Task<IEnumerable<string>> GetFilesAsync()
        {
            return fileHelper.GetFilesAsync();
        }

        public static Task DeleteFileAsync(string filename)
        {
            return fileHelper.DeleteFileAsync(filename);
        }
    }
}
