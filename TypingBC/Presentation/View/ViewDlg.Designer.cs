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
            this.groupBoxHint = new System.Windows.Forms.GroupBox();
            this.rtbHint = new System.Windows.Forms.RichTextBox();
            this.groupBoxWorkSpace = new System.Windows.Forms.GroupBox();
            this.Clock = new System.Windows.Forms.Timer(this.components);
            this.groupBoxHint.SuspendLayout();
            this.groupBoxWorkSpace.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMainMenu
            // 
            this.lbMainMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMainMenu.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMainMenu.FormattingEnabled = true;
            this.lbMainMenu.HorizontalScrollbar = true;
            this.lbMainMenu.IntegralHeight = false;
            this.lbMainMenu.ItemHeight = 23;
            this.lbMainMenu.Location = new System.Drawing.Point(3, 28);
            this.lbMainMenu.Margin = new System.Windows.Forms.Padding(6);
            this.lbMainMenu.Name = "lbMainMenu";
            this.lbMainMenu.Size = new System.Drawing.Size(438, 276);
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
            this.layer4RTBVungTapGo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layer4RTBVungTapGo.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layer4RTBVungTapGo.Location = new System.Drawing.Point(3, 28);
            this.layer4RTBVungTapGo.Margin = new System.Windows.Forms.Padding(4);
            this.layer4RTBVungTapGo.Name = "layer4RTBVungTapGo";
            this.layer4RTBVungTapGo.Size = new System.Drawing.Size(438, 276);
            this.layer4RTBVungTapGo.TabIndex = 1;
            this.layer4RTBVungTapGo.Text = "";
            this.layer4RTBVungTapGo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.layer4RTBVungTapGo.TextChanged += new System.EventHandler(this.layer4RTBVungTapGo_TextChanged);
            // 
            // groupBoxHint
            // 
            this.groupBoxHint.Controls.Add(this.rtbHint);
            this.groupBoxHint.Location = new System.Drawing.Point(487, 108);
            this.groupBoxHint.Name = "groupBoxHint";
            this.groupBoxHint.Size = new System.Drawing.Size(399, 307);
            this.groupBoxHint.TabIndex = 1;
            this.groupBoxHint.TabStop = false;
            this.groupBoxHint.Text = "Hướng dẫn";
            // 
            // rtbHint
            // 
            this.rtbHint.AutoWordSelection = true;
            this.rtbHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbHint.Location = new System.Drawing.Point(3, 28);
            this.rtbHint.Name = "rtbHint";
            this.rtbHint.ReadOnly = true;
            this.rtbHint.Size = new System.Drawing.Size(393, 276);
            this.rtbHint.TabIndex = 0;
            this.rtbHint.Text = "";
            this.rtbHint.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // groupBoxWorkSpace
            // 
            this.groupBoxWorkSpace.Controls.Add(this.layer4RTBVungTapGo);
            this.groupBoxWorkSpace.Controls.Add(this.lbMainMenu);
            this.groupBoxWorkSpace.Location = new System.Drawing.Point(12, 108);
            this.groupBoxWorkSpace.Name = "groupBoxWorkSpace";
            this.groupBoxWorkSpace.Size = new System.Drawing.Size(444, 307);
            this.groupBoxWorkSpace.TabIndex = 0;
            this.groupBoxWorkSpace.TabStop = false;
            this.groupBoxWorkSpace.Text = "Menu";
            // 
            // ViewDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(470, 420);
            this.Controls.Add(this.groupBoxWorkSpace);
            this.Controls.Add(this.groupBoxHint);
            this.Controls.Add(this.layer4LabelString);
            this.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chương trình tập đánh chữ";
            this.Load += new System.EventHandler(this.ViewDlg_Load);
            this.groupBoxHint.ResumeLayout(false);
            this.groupBoxWorkSpace.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbMainMenu;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label layer4LabelString;
        private System.Windows.Forms.RichTextBox layer4RTBVungTapGo;
        private System.Windows.Forms.GroupBox groupBoxHint;
        private System.Windows.Forms.RichTextBox rtbHint;
        private System.Windows.Forms.GroupBox groupBoxWorkSpace;
        private System.Windows.Forms.Timer Clock;

    }
}

