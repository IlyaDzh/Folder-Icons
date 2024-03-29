﻿using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace FolderIcons
{
    public partial class Form1 : Form
    {
        public static readonly string PATH_EXE = Path.GetDirectoryName(Application.ExecutablePath);
        public readonly string PATH_COLORS = $@"{PATH_EXE}\Icons\Colors";
        public readonly string PATH_ADDITIONAl = $@"{PATH_EXE}\Icons\Additional";
        public readonly string PATH_MY_ICONS = $@"{PATH_EXE}\Icons\My icons";
        ImageList myIconsList = new ImageList();
        ImageList additionalList = new ImageList();
        string filePath = "";

        public Form1(string[] args)
        {
            InitializeComponent();
            
            if (args.Length == 2) ChangeIcon(args[0], args[1]);
            if (args.Length == 1) textBoxFile.Text = args[0];

            if (!Directory.Exists(PATH_MY_ICONS)) Directory.CreateDirectory(PATH_MY_ICONS);
            if (!Directory.Exists(PATH_COLORS)) Directory.CreateDirectory(PATH_COLORS);

            InitList(PATH_COLORS, imageList1, listView1);
            additionalList.ImageSize = new Size(38, 38);
            additionalList.ColorDepth = ColorDepth.Depth32Bit;
            InitList(PATH_ADDITIONAl, additionalList, listView3);
            myIconsList.ImageSize = new Size(38, 38);
            myIconsList.ColorDepth = ColorDepth.Depth32Bit;
            InitList(PATH_MY_ICONS, myIconsList, listView2);

            CheckRegistry();
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
                    if (Path.GetExtension(file.FullName)==".ico")
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

        void ChangeIcon(string path, ImageList il, ListView lv)
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

        void CheckRegistry()
        {
            using (RegistryKey reg = Registry.ClassesRoot.OpenSubKey(@"Folder\shell\FolderIcons"))
            {
                if (reg != null)
                {
                    addToolStripMenuItem.Enabled = false;
                    deleteToolStripMenuItem.Enabled = true;
                }
                else
                {
                    addToolStripMenuItem.Enabled = true;
                    deleteToolStripMenuItem.Enabled = false;
                }
            }
        }

        void CreateSubKeyAndSetValue(string color, string pathKey, string pathIcon)
        {
            RegistryKey keySubMenu = Registry.LocalMachine.CreateSubKey($@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{pathKey}");
            keySubMenu.SetValue("", color);
            keySubMenu.SetValue("Icon", pathIcon);
            keySubMenu = Registry.LocalMachine.CreateSubKey($@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{pathKey}\command");
            keySubMenu.SetValue("", $"{PATH_EXE}\\FolderIcons.exe \"%1\" \"{pathIcon}\"");
        }
    }
}