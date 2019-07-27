using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FolderIcons
{
    public partial class Form1
    {
        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (textBoxFile.Text == "")
            {
                MessageBox.Show("Введите путь к папке!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (listView1.CanSelect)
                    ChangeIcon(PATH_COLORS, imageList1, listView1);
                else if (listView2.CanSelect)
                    ChangeIcon(PATH_MY_ICONS, myIconsList, listView2);
                else if (listView3.CanSelect)
                    ChangeIcon(PATH_ADDITIONAl, additionalList, listView3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDefault_Click(object sender, EventArgs e)
        {
            if (textBoxFile.Text == "")
            {
                MessageBox.Show("Введите путь к папке!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DirectoryInfo folder = new DirectoryInfo($@"{textBoxFile.Text}\");
            filePath = folder.FullName + "desktop.ini";
            File.Delete(filePath);

            RestartExplorer(folder.FullName);
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
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!Directory.Exists(PATH_MY_ICONS))
                    Directory.CreateDirectory(PATH_MY_ICONS);
                foreach (var item in openFileDialog1.FileNames)
                {
                    if(!File.Exists($"{PATH_MY_ICONS}\\{Path.GetFileName(item)}") && Path.GetExtension(item) == ".ico")
                    {
                        File.Copy(item, $"{PATH_MY_ICONS}\\{Path.GetFileName(item)}");
                    }
                }
            }
            myIconsList.Images.Clear();
            listView2.Clear();
            InitList(PATH_MY_ICONS, myIconsList, listView2);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 0) return;
            var key = myIconsList.Images.Keys[listView2.SelectedIndices[0]];

            try
            {
                if (File.Exists($"{PATH_MY_ICONS}\\{key}"))
                    File.Delete($"{PATH_MY_ICONS}\\{key}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            myIconsList.Images.Clear();
            listView2.Clear();
            InitList(PATH_MY_ICONS, myIconsList, listView2);
        }

        private void buttonUpdate_Click(object sender, System.EventArgs e)
        {
            myIconsList.Images.Clear();
            listView2.Clear();
            InitList(PATH_MY_ICONS, myIconsList, listView2);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (RegistryKey keyMenu = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\FolderIcons"))
                {
                    keyMenu.SetValue("Icon", $"\"{PATH_EXE}\\Icons\\IconProgram.ico\"");
                    keyMenu.SetValue("MUIVerb", "Изменить цвет папки");
                    keyMenu.SetValue("SubCommands", "IconsProgram;f1;f2;f3;f4;f5;f6;f7;f8;f9;f10;f11;f12;f13;f14;f15;");
                }

                RegistryKey keySubMenu = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\IconsProgram");
                keySubMenu.SetValue("", "Open the program");
                keySubMenu.SetValue("Icon", $"\"{PATH_EXE}\\Icons\\IconProgram.ico\"");
                keySubMenu.SetValue("CommandFlags", 0x40, RegistryValueKind.DWord);
                keySubMenu = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\IconsProgram\command");
                keySubMenu.SetValue("", $"{PATH_EXE}\\FolderIcons.exe \"%1\"");

                CreateSubKeyAndSetValue("Blue", "f1", $"{PATH_EXE}\\Icons\\Colors\\Ablue.ico");
                CreateSubKeyAndSetValue("Coffee", "f2", $"{PATH_EXE}\\Icons\\Colors\\Acoffee.ico");
                CreateSubKeyAndSetValue("Dark gray", "f3", $"{PATH_EXE}\\Icons\\Colors\\Adark gray.ico");
                CreateSubKeyAndSetValue("Gray", "f4", $"{PATH_EXE}\\Icons\\Colors\\Agray.ico");
                CreateSubKeyAndSetValue("Green", "f5", $"{PATH_EXE}\\Icons\\Colors\\Agreen.ico");
                CreateSubKeyAndSetValue("Lime", "f6", $"{PATH_EXE}\\Icons\\Colors\\Alime.ico");
                CreateSubKeyAndSetValue("Maroon", "f7", $"{PATH_EXE}\\Icons\\Colors\\Amaroon.ico");
                CreateSubKeyAndSetValue("Navy", "f8", $"{PATH_EXE}\\Icons\\Colors\\Anavy.ico");
                CreateSubKeyAndSetValue("Orange", "f9", $"{PATH_EXE}\\Icons\\Colors\\Aorange.ico");
                CreateSubKeyAndSetValue("Pink", "f10", $"{PATH_EXE}\\Icons\\Colors\\Apink.ico");
                CreateSubKeyAndSetValue("Purple", "f11", $"{PATH_EXE}\\Icons\\Colors\\Apurple.ico");
                CreateSubKeyAndSetValue("Race blue", "f12", $"{PATH_EXE}\\Icons\\Colors\\Arace blue.ico");
                CreateSubKeyAndSetValue("Red", "f13", $"{PATH_EXE}\\Icons\\Colors\\Ared.ico");
                CreateSubKeyAndSetValue("White", "f14", $"{PATH_EXE}\\Icons\\Colors\\Awhite.ico");
                CreateSubKeyAndSetValue("Yellow", "f15", $"{PATH_EXE}\\Icons\\Colors\\Ayellow.ico");
            }
            catch
            {
                MessageBox.Show("Необходимо запустить программу от имени Администратора!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            addToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = true;
        }

        private void deleteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                Registry.ClassesRoot.DeleteSubKey(@"Folder\shell\FolderIcons");
                Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\IconsProgram");
                for (int i = 1; i < 16; i++)
                {
                    Registry.LocalMachine.DeleteSubKeyTree($@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\f{i}");
                }
            }
            catch
            {
                MessageBox.Show("Необходимо запустить программу от имени Администратора!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            addToolStripMenuItem.Enabled = true;
            deleteToolStripMenuItem.Enabled = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FolderIcons v1.0 by IlyaD", "About program", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }

        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"/n, /select, {PATH_EXE}" });
        }
    }
}