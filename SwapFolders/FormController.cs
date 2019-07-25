using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace SwapFolders
{
    public partial class Form1
    {
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

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (RegistryKey keyMenu = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\FolderIcons"))
            {
                keyMenu.SetValue("Icon", $@"{Path.GetDirectoryName(Application.ExecutablePath)}\IconProgram.ico");
                keyMenu.SetValue("MUIVerb", "Изменить цвет папки");
                keyMenu.SetValue("SubCommands", "IconsProgram;f1;f2;f3;f4;f5;f6;f7;f8;f9;f10;f11;f12;f13;f14;f15;");
            }

            RegistryKey keySubMenu = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\IconsProgram");
            keySubMenu.SetValue("", "Open the program");
            keySubMenu.SetValue("Icon", $@"{Path.GetDirectoryName(Application.ExecutablePath)}\IconProgram.ico");
            keySubMenu.SetValue("CommandFlags", 0x40, RegistryValueKind.DWord);
            keySubMenu = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\IconsProgram\command");
            keySubMenu.SetValue("", $"{Path.GetDirectoryName(Application.ExecutablePath)}\\SwapFolders.exe \"%1\"");

            CreateSubKeyAndSetValue("Blue", "f1", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Ablue.ico");
            CreateSubKeyAndSetValue("Coffee", "f2", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Acoffee.ico");
            CreateSubKeyAndSetValue("Dark gray", "f3", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Adark gray.ico");
            CreateSubKeyAndSetValue("Gray", "f4", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Agray.ico");
            CreateSubKeyAndSetValue("Green", "f5", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Agreen.ico");
            CreateSubKeyAndSetValue("Lime", "f6", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Alime.ico");
            CreateSubKeyAndSetValue("Maroon", "f7", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Amaroon.ico");
            CreateSubKeyAndSetValue("Navy", "f8", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Anavy.ico");
            CreateSubKeyAndSetValue("Orange", "f9", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Aorange.ico");
            CreateSubKeyAndSetValue("Pink", "f10", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Apink.ico");
            CreateSubKeyAndSetValue("Purple", "f11", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Apurple.ico");
            CreateSubKeyAndSetValue("Race blue", "f12", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Arace blue.ico");
            CreateSubKeyAndSetValue("Red", "f13", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Ared.ico");
            CreateSubKeyAndSetValue("White", "f14", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Awhite.ico");
            CreateSubKeyAndSetValue("Yellow", "f15", "D:\\WindowsForms\\SwapFolders\\SwapFolders\\Icons\\Colors\\Ayellow.ico");

            addToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = true;
        }

        private void deleteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Registry.ClassesRoot.DeleteSubKey(@"Folder\shell\FolderIcons");
            Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\IconsProgram");
            for (int i = 1; i < 16; i++)
            {
                Registry.LocalMachine.DeleteSubKeyTree($@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\f{i}");
            }
            addToolStripMenuItem.Enabled = true;
            deleteToolStripMenuItem.Enabled = false;
        }
    }
}