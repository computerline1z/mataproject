namespace TypingBC.Presentation.View
{
    partial class ViewDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewDlg));
            this.lbMainMenu = new System.Windows.Forms.ListBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.layer4LabelString = new System.Windows.Forms.Label();
            this.layer4RTBVungTapGo = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtbHint = new System.Windows.Forms.RichTextBox();
            this.lbMenu = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMainMenu
            // 
            this.lbMainMenu.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMainMenu.FormattingEnabled = true;
            this.lbMainMenu.HorizontalScrollbar = true;
            this.lbMainMenu.IntegralHeight = false;
            this.lbMainMenu.ItemHeight = 23;
            this.lbMainMenu.Location = new System.Drawing.Point(17, 122);
            this.lbMainMenu.Margin = new System.Windows.Forms.Padding(6);
            this.lbMainMenu.Name = "lbMainMenu";
            this.lbMainMenu.Size = new System.Drawing.Size(434, 393);
            this.lbMainMenu.TabIndex = 0;
            this.lbMainMenu.DoubleClick += new System.EventHandler(this.lbMainMenu_DoubleClick);
            this.lbMainMenu.SelectedValueChanged += new System.EventHandler(this.lbMainMenu_SelectedValueChanged);
            this.lbMainMenu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            // 
            // layer4LabelString
            // 
            this.layer4LabelString.AutoSize = true;
            this.layer4LabelString.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layer4LabelString.Location = new System.Drawing.Point(13, 15);
            this.layer4LabelString.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.layer4LabelString.Name = "layer4LabelString";
            this.layer4LabelString.Size = new System.Drawing.Size(167, 23);
            this.layer4LabelString.TabIndex = 2;
            this.layer4LabelString.Text = "Chuỗi cần phải gõ:\n";
            this.layer4LabelString.Visible = false;
            // 
            // layer4RTBVungTapGo
            // 
            this.layer4RTBVungTapGo.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layer4RTBVungTapGo.Location = new System.Drawing.Point(17, 122);
            this.layer4RTBVungTapGo.Margin = new System.Windows.Forms.Padding(4);
            this.layer4RTBVungTapGo.Name = "layer4RTBVungTapGo";
            this.layer4RTBVungTapGo.Size = new System.Drawing.Size(434, 393);
            this.layer4RTBVungTapGo.TabIndex = 0;
            this.layer4RTBVungTapGo.Text = "";
            this.layer4RTBVungTapGo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbHint);
            this.groupBox1.Location = new System.Drawing.Point(487, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 407);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hướng dẫn";
            // 
            // rtbHint
            // 
            this.rtbHint.AutoWordSelection = true;
            this.rtbHint.Location = new System.Drawing.Point(22, 31);
            this.rtbHint.Name = "rtbHint";
            this.rtbHint.ReadOnly = true;
            this.rtbHint.Size = new System.Drawing.Size(355, 370);
            this.rtbHint.TabIndex = 0;
            this.rtbHint.Text = "";
            this.rtbHint.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // lbMenu
            // 
            this.lbMenu.AutoSize = true;
            this.lbMenu.Location = new System.Drawing.Point(131, 38);
            this.lbMenu.Name = "lbMenu";
            this.lbMenu.Size = new System.Drawing.Size(168, 23);
            this.lbMenu.TabIndex = 6;
            this.lbMenu.Text = "Menu chương trình";
            // 
            // ViewDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(911, 525);
            this.Controls.Add(this.lbMenu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.layer4RTBVungTapGo);
            this.Controls.Add(this.layer4LabelString);
            this.Controls.Add(this.lbMainMenu);
            this.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chương trình tập đánh chữ";
            this.Load += new System.EventHandler(this.ViewDlg_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbMainMenu;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label layer4LabelString;
        private System.Windows.Forms.RichTextBox layer4RTBVungTapGo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtbHint;
        private System.Windows.Forms.Label lbMenu;

    }
}

