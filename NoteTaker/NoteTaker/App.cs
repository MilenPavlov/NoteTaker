﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace NoteTaker
{
    public class App
    {
        static NoteFolder noteFolder = new NoteFolder();

        internal static NoteFolder NoteFolder
        {
            get { return noteFolder; }
        }
        public static Page GetMainPage()
        {
            return new NavigationPage(new HomePage());
        }
    }
}
