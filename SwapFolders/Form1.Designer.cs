namespace SwapFolders
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonOpenFIle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listView = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxFile
            // 
            this.textBoxFile.AllowDrop = true;
            this.textBoxFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxFile.Location = new System.Drawing.Point(12, 12);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(360, 24);
            this.textBoxFile.TabIndex = 1;
            this.textBoxFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxFile_DragDrop);
            this.textBoxFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBoxFile_DragEnter);
            // 
            // buttonApply
            // 
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonApply.Location = new System.Drawing.Point(134, 348);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(116, 46);
            this.buttonApply.TabIndex = 5;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonOpenFIle
            // 
            this.buttonOpenFIle.BackColor = System.Drawing.Color.Transparent;
            this.buttonOpenFIle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpenFIle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOpenFIle.Location = new System.Drawing.Point(12, 42);
            this.buttonOpenFIle.Name = "buttonOpenFIle";
            this.buttonOpenFIle.Size = new System.Drawing.Size(360, 27);
            this.buttonOpenFIle.TabIndex = 2;
            this.buttonOpenFIle.Text = "Upload folder";
            this.buttonOpenFIle.UseVisualStyleBackColor = false;
            this.buttonOpenFIle.Click += new System.EventHandler(this.buttonOpenFIle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 366);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(13, 117);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(359, 225);
            this.tabControl.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(351, 199);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Colors";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listView
            // 
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView.Location = new System.Drawing.Point(3, 3);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(345, 193);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(351, 199);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "My icons";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "blue.ico");
            this.imageList1.Images.SetKeyName(1, "coffee.ico");
            this.imageList1.Images.SetKeyName(2, "dark gray.ico");
            this.imageList1.Images.SetKeyName(3, "gray.ico");
            this.imageList1.Images.SetKeyName(4, "green.ico");
            this.imageList1.Images.SetKeyName(5, "light blue.ico");
            this.imageList1.Images.SetKeyName(6, "lime.ico");
            this.imageList1.Images.SetKeyName(7, "maroon.ico");
            this.imageList1.Images.SetKeyName(8, "mint.ico");
            this.imageList1.Images.SetKeyName(9, "navy.ico");
            this.imageList1.Images.SetKeyName(10, "olive.ico");
            this.imageList1.Images.SetKeyName(11, "orange.ico");
            this.imageList1.Images.SetKeyName(12, "pink.ico");
            this.imageList1.Images.SetKeyName(13, "purple.ico");
            this.imageList1.Images.SetKeyName(14, "race blue.ico");
            this.imageList1.Images.SetKeyName(15, "red.ico");
            this.imageList1.Images.SetKeyName(16, "space.ico");
            this.imageList1.Images.SetKeyName(17, "teal.ico");
            this.imageList1.Images.SetKeyName(18, "white.ico");
            this.imageList1.Images.SetKeyName(19, "yellow.ico");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 415);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOpenFIle);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.textBoxFile);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonOpenFIle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ImageList imageList1;
    }
}

