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
            this.lbMainMenu = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbMainMenu
            // 
            this.lbMainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMainMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMainMenu.FormattingEnabled = true;
            this.lbMainMenu.ItemHeight = 16;
            this.lbMainMenu.Location = new System.Drawing.Point(16, 91);
            this.lbMainMenu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbMainMenu.Name = "lbMainMenu";
            this.lbMainMenu.Size = new System.Drawing.Size(488, 388);
            this.lbMainMenu.TabIndex = 0;
            this.lbMainMenu.DoubleClick += new System.EventHandler(this.lbMainMenu_DoubleClick);
            this.lbMainMenu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbMainMenu_KeyDown);
            // 
            // ViewDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 496);
            this.Controls.Add(this.lbMainMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ViewDlg";
            this.Text = "Chương trình tập đánh chữ";
            this.Load += new System.EventHandler(this.ViewDlg_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbMainMenu;

    }
}

