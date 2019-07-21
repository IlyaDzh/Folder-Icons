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
        public readonly string PATH_COLORS = @"D:\WindowsForms\SwapFolders\SwapFolders\Icons\Colors";
        public readonly string PATH_MY_ICONS = @"D:\WindowsForms\SwapFolders\SwapFolders\Icons\My icons";
        ImageList myIconsList = new ImageList();
        string filePath = "";

        public Form1()
        {
            InitializeComponent();
            
            listView1.LargeImageList = imageList1;
            for (int i = 0; i < imageList1.Images.Count; i++)
            {
                listView1.Items.Add($"{i}", i);
            }

            //DirectoryInfo dir1 = new DirectoryInfo(PATH_COLORS);
            //foreach (FileInfo file in dir1.GetFiles())
            //{
            //    try
            //    {
            //        imageList1.Images.Add(Image.FromFile(file.FullName));
            //    }
            //    catch { }
            //}
            //for (int i = 0; i < imageList1.Images.Count; i++)
            //{
            //    listView1.Items.Add($"{i}", i);
            //}


            myIconsList.ImageSize = new Size(38, 38);
            listView2.LargeImageList = myIconsList;
            DirectoryInfo dir = new DirectoryInfo(PATH_MY_ICONS);
            foreach (FileInfo file in dir.GetFiles())
            {
                try
                {
                    myIconsList.Images.Add(Image.FromFile(file.FullName));
                }
                catch { }
            }
            for (int i = 0; i < myIconsList.Images.Count; i++)
            {
                listView2.Items.Add($"{i}", i);
            }
        }
        
        string PathIcon(string iconName)
        {
            return $@"{PATH_COLORS}\{iconName}";
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
                file.WriteLine($@"IconResource={PathIcon(imageList1.Images.Keys[listView1.SelectedIndices[0]])}, 0");
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            listView2.LargeImageList = myIconsList;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                myIconsList.Images.Add(Image.FromFile(openFileDialog1.FileName));
            }
            listView2.Clear();
            for (int i = 0; i < myIconsList.Images.Count; i++)
            {
                listView2.Items.Add($"{i}", i);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }
    }
}