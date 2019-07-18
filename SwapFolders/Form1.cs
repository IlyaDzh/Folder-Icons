using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwapFolders
{
    public partial class Form1 : Form
    {
        string filePath = "";

        public Form1()
        {
            InitializeComponent();
        }

        void RestartExplorer()
        {
            Process[] explorer = Process.GetProcessesByName("explorer");
            foreach (Process process in explorer)
            {
                process.Kill();
            }
            Process.Start("explorer.exe");
        }

        void SwapIcon()
        {
            DirectoryInfo folder = new DirectoryInfo($@"{textBox1.Text}\");
            filePath = folder.FullName + "desktop.ini";
            File.Delete(filePath);

            using (StreamWriter file = new StreamWriter(filePath))
            {
                file.WriteLine("[.ShellClassInfo]");
                file.WriteLine(@"IconFile=D:\Swap Folders\icl\fColors.icl");
                file.WriteLine("IconIndex = 3");
            }
            folder.Attributes = folder.Attributes | FileAttributes.ReadOnly;
            File.SetAttributes(filePath, FileAttributes.Hidden | FileAttributes.System | FileAttributes.Archive);

            RestartExplorer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SwapIcon();
        }

        private void buttonOpenFIle_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}