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
        int filesInFolder;

        public Form1()
        {
            InitializeComponent();
            
            filesInFolder = CountFiles(@"D:\WindowsForms\SwapFolders\SwapFolders\Icons\Colors");
            listView.SmallImageList = imageList1;
            listView.LargeImageList = imageList1;
            for (int i = 0; i < filesInFolder; i++)
            {
                listView.Items.Add($"{i}", i);
            }
        }

        int CountFiles(string path)
        {
            return new DirectoryInfo(path).GetFiles(@"*.*", SearchOption.AllDirectories).Length;
        }
        
        string PathIcon(string iconName)
        {
            return $@"D:\WindowsForms\SwapFolders\SwapFolders\Icons\Colors\{iconName}";
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
            DirectoryInfo folder = new DirectoryInfo($@"{textBoxFile.Text}\");
            filePath = folder.FullName + "desktop.ini";
            File.Delete(filePath);

            using (StreamWriter file = new StreamWriter(filePath))
            {
                file.WriteLine("[.ShellClassInfo]");
                file.WriteLine($@"IconResource={PathIcon(imageList1.Images.Keys[listView.SelectedIndices[0]])}, 0");
            }
            folder.Attributes = folder.Attributes | FileAttributes.ReadOnly;
            File.SetAttributes(filePath, FileAttributes.Hidden | FileAttributes.System | FileAttributes.Archive);

            RestartExplorer();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            try
            {
                SwapIcon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOpenFIle_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFile.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void textBoxFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void textBoxFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (Directory.Exists(file[0]))
                textBoxFile.Text = file[0];
            else
                MessageBox.Show("Это не папка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}