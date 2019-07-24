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

        public Form1(string[] args)
        {
            InitializeComponent();
            
            if (args.Length > 0)
                ChangeIcon(args[0], args[1]);

            InitList(PATH_COLORS, imageList1, listView1);
            
            myIconsList.ImageSize = new Size(38, 38);
            myIconsList.ColorDepth = ColorDepth.Depth32Bit;
            InitList(PATH_MY_ICONS, myIconsList, listView2);
        }

        void ChangeIcon(string pathFile, string pathIcon)
        {
            DirectoryInfo folder = new DirectoryInfo($@"{pathFile}\");
            string desktopFile = folder.FullName + "desktop.ini";
            File.Delete(desktopFile);

            using (StreamWriter file = new StreamWriter(desktopFile))
            {
                file.WriteLine("[.ShellClassInfo]");
                file.WriteLine($@"IconResource={pathIcon}, 0");
            }
            folder.Attributes = folder.Attributes | FileAttributes.ReadOnly;
            File.SetAttributes(desktopFile, FileAttributes.Hidden | FileAttributes.System | FileAttributes.Archive);

            RestartExplorer(pathFile);
            Close();
        }

        void InitList(string pathDir, ImageList il, ListView lv)
        {
            lv.LargeImageList = il;
            DirectoryInfo dir = new DirectoryInfo(pathDir);
            foreach (FileInfo file in dir.GetFiles())
            {
                try
                {
                    il.Images.Add(file.Name, Image.FromFile(file.FullName));
                }
                catch { }
            }
            for (int i = 0; i < il.Images.Count; i++)
            {
                lv.Items.Add($"{i+1}", i);
            }
        }

        void RestartExplorer(string openFile)
        {
            Process[] explorer = Process.GetProcessesByName("explorer");
            foreach (Process process in explorer)
            {
                process.Kill();
            }
            Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"/n, /select, {openFile}"});
        }

        void SwapIcon(string path, ImageList il, ListView lv)
        {
            DirectoryInfo folder = new DirectoryInfo($@"{textBoxFile.Text}\");
            filePath = folder.FullName + "desktop.ini";
            File.Delete(filePath);

            using (StreamWriter file = new StreamWriter(filePath))
            {
                file.WriteLine("[.ShellClassInfo]");
                file.WriteLine($@"IconResource={PathIcon(path, il.Images.Keys[lv.SelectedIndices[0]])}, 0");
            }
            folder.Attributes = folder.Attributes | FileAttributes.ReadOnly;
            File.SetAttributes(filePath, FileAttributes.Hidden | FileAttributes.System | FileAttributes.Archive);

            RestartExplorer(folder.FullName);
        }

        string PathIcon(string path, string iconName)
        {
            return $@"{path}\{iconName}";
        }

        //
        // Controller
        //

        private void buttonApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.CanSelect)
                    SwapIcon(PATH_COLORS, imageList1, listView1);
                else if (listView2.CanSelect)
                    SwapIcon(PATH_MY_ICONS, myIconsList, listView2);
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
            //скопировать выбранный файл в папку
            //обновить list
            //listView2.LargeImageList = myIconsList;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //File.Copy(openFileDialog1.FileName, $@"{PATH_MY_ICONS}\{openFileDialog1.FileName}");
                //myIconsList.Images.Add(Image.FromFile(openFileDialog1.FileName));
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