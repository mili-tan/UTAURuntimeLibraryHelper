﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;


namespace UTAURtLibHelper
{
    public partial class FormBlockHoles : Form
    {
        string libFile;
        string SystemFile = "";
        string WinFile;
        bool winType = Environment.Is64BitOperatingSystem;

        public FormBlockHoles()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = false;
            DialogResult Result = openFileDialog1.ShowDialog();
            if (Result == DialogResult.OK)
            {
                libFile = openFileDialog1.FileName;
                btnReg.Enabled = true;
            }
        }

        private void FormBlockHoles_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            btnReg.Enabled = false;
            if (winType == true)
            {
                WinFile = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
                SystemFile = WinFile + @"\SysWOW64\";
            }
            if (winType == false)
            {
                WinFile= Environment.GetFolderPath(Environment.SpecialFolder.Windows);
                SystemFile = WinFile + @"\system32\";
            }
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(libFile);
            String FileName = info.OriginalFilename;

            try
            {
                File.Copy(libFile, SystemFile + FileName, false);
                MessageBox.Show(FileName + " 写入成功。");
            }
            catch (Exception errorMsg)
            {
                MessageBox.Show(errorMsg.Message + "\r\n" + FileName + " 文件未写入。");
            }

            RegRun.RegLib(SystemFile + FileName);
        }
    }
}
