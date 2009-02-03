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
            this.layer4LabelChuoiCanGo = new System.Windows.Forms.Label();
            this.layer4LabelStringChuoiCanGo = new System.Windows.Forms.Label();
            this.layer4RTBVungTapGo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lbMainMenu
            // 
            this.lbMainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMainMenu.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMainMenu.FormattingEnabled = true;
            this.lbMainMenu.ItemHeight = 23;
            this.lbMainMenu.Location = new System.Drawing.Point(24, 131);
            this.lbMainMenu.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.lbMainMenu.Name = "lbMainMenu";
            this.lbMainMenu.Size = new System.Drawing.Size(473, 303);
            this.lbMainMenu.TabIndex = 0;
            this.lbMainMenu.DoubleClick += new System.EventHandler(this.lbMainMenu_DoubleClick);
            this.lbMainMenu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbMainMenu_KeyDown);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            // 
            // layer4LabelChuoiCanGo
            // 
            this.layer4LabelChuoiCanGo.AutoSize = true;
            this.layer4LabelChuoiCanGo.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layer4LabelChuoiCanGo.Location = new System.Drawing.Point(26, 13);
            this.layer4LabelChuoiCanGo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.layer4LabelChuoiCanGo.Name = "layer4LabelChuoiCanGo";
            this.layer4LabelChuoiCanGo.Size = new System.Drawing.Size(167, 23);
            this.layer4LabelChuoiCanGo.TabIndex = 2;
            this.layer4LabelChuoiCanGo.Text = "Chuỗi cần phải gõ:";
            // 
            // layer4LabelStringChuoiCanGo
            // 
            this.layer4LabelStringChuoiCanGo.AutoSize = true;
            this.layer4LabelStringChuoiCanGo.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layer4LabelStringChuoiCanGo.Location = new System.Drawing.Point(26, 55);
            this.layer4LabelStringChuoiCanGo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.layer4LabelStringChuoiCanGo.Name = "layer4LabelStringChuoiCanGo";
            this.layer4LabelStringChuoiCanGo.Size = new System.Drawing.Size(174, 23);
            this.layer4LabelStringChuoiCanGo.TabIndex = 1;
            this.layer4LabelStringChuoiCanGo.Text = "Đây là chuỗi cần gõ";
            // 
            // layer4RTBVungTapGo
            // 
            this.layer4RTBVungTapGo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.layer4RTBVungTapGo.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layer4RTBVungTapGo.Location = new System.Drawing.Point(24, 131);
            this.layer4RTBVungTapGo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.layer4RTBVungTapGo.Name = "layer4RTBVungTapGo";
            this.layer4RTBVungTapGo.Size = new System.Drawing.Size(473, 303);
            this.layer4RTBVungTapGo.TabIndex = 0;
            this.layer4RTBVungTapGo.Text = "";
            // 
            // ViewDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 453);
            this.Controls.Add(this.layer4RTBVungTapGo);
            this.Controls.Add(this.layer4LabelStringChuoiCanGo);
            this.Controls.Add(this.layer4LabelChuoiCanGo);
            this.Controls.Add(this.lbMainMenu);
            this.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewDlg";
            this.Text = "Chương trình tập đánh chữ";
            this.Load += new System.EventHandler(this.ViewDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbMainMenu;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label layer4LabelChuoiCanGo;
        private System.Windows.Forms.Label layer4LabelStringChuoiCanGo;
        private System.Windows.Forms.RichTextBox layer4RTBVungTapGo;

    }
}

