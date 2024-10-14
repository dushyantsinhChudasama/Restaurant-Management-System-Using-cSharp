
namespace Restaurant_Management_System.views
{
    partial class frmSpecialOrderView
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddSpecialOrder = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.guna2MessageDialog1 = new Guna.UI2.WinForms.Guna2MessageDialog();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(37, 162);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1046, 458);
            this.flowLayoutPanel1.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(42, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 33);
            this.label2.TabIndex = 16;
            this.label2.Text = "Special Orders";
            // 
            // btnAddSpecialOrder
            // 
            this.btnAddSpecialOrder.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(87)))), ((int)(((byte)(255)))));
            this.btnAddSpecialOrder.BorderRadius = 12;
            this.btnAddSpecialOrder.BorderThickness = 2;
            this.btnAddSpecialOrder.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddSpecialOrder.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddSpecialOrder.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddSpecialOrder.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddSpecialOrder.FillColor = System.Drawing.Color.Transparent;
            this.btnAddSpecialOrder.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSpecialOrder.ForeColor = System.Drawing.Color.Black;
            this.btnAddSpecialOrder.Image = global::Restaurant_Management_System.Properties.Resources.addIcons;
            this.btnAddSpecialOrder.ImageSize = new System.Drawing.Size(28, 28);
            this.btnAddSpecialOrder.Location = new System.Drawing.Point(891, 38);
            this.btnAddSpecialOrder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddSpecialOrder.Name = "btnAddSpecialOrder";
            this.btnAddSpecialOrder.Size = new System.Drawing.Size(192, 49);
            this.btnAddSpecialOrder.TabIndex = 15;
            this.btnAddSpecialOrder.Text = "Add New Order";
            this.btnAddSpecialOrder.Click += new System.EventHandler(this.btnAddOrder_Click);
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.guna2Separator1.FillThickness = 2;
            this.guna2Separator1.Location = new System.Drawing.Point(37, 118);
            this.guna2Separator1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(1046, 13);
            this.guna2Separator1.TabIndex = 14;
            // 
            // guna2MessageDialog1
            // 
            this.guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            this.guna2MessageDialog1.Caption = "RMS";
            this.guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
            this.guna2MessageDialog1.Parent = null;
            this.guna2MessageDialog1.Style = Guna.UI2.WinForms.MessageDialogStyle.Light;
            this.guna2MessageDialog1.Text = null;
            // 
            // frmSpecialOrderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 694);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAddSpecialOrder);
            this.Controls.Add(this.guna2Separator1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmSpecialOrderView";
            this.Text = "frmSpecialOrderView";
            this.Load += new System.EventHandler(this.frmSpecialOrderView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Button btnAddSpecialOrder;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private Guna.UI2.WinForms.Guna2MessageDialog guna2MessageDialog1;
    }
}