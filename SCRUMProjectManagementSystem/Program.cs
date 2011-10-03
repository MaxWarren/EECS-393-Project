﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SCRUMProjectManagementSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DatabaseLayer.DatabaseLayer.tryOpen();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SPMS());
        }
    }
}
