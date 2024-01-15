namespace SelfHosting.Client_WinFormsNET8
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            flowLayoutPanel1 = new FlowLayoutPanel();
            loadbutton = new Button();
            numericUpDown1 = new NumericUpDown();
            orderbutton = new Button();
            dataGridView1 = new DataGridView();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(loadbutton);
            flowLayoutPanel1.Controls.Add(numericUpDown1);
            flowLayoutPanel1.Controls.Add(orderbutton);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1925, 48);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // loadbutton
            // 
            loadbutton.AutoSize = true;
            loadbutton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            loadbutton.Location = new Point(3, 3);
            loadbutton.Name = "loadbutton";
            loadbutton.Size = new Size(88, 42);
            loadbutton.TabIndex = 0;
            loadbutton.Text = "Laden";
            loadbutton.UseVisualStyleBackColor = true;
            loadbutton.Click += loadbutton_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(97, 3);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(192, 39);
            numericUpDown1.TabIndex = 1;
            // 
            // orderbutton
            // 
            orderbutton.AutoSize = true;
            orderbutton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            orderbutton.Location = new Point(295, 3);
            orderbutton.Name = "orderbutton";
            orderbutton.Size = new Size(121, 42);
            orderbutton.TabIndex = 0;
            orderbutton.Text = "Bestellen";
            orderbutton.UseVisualStyleBackColor = true;
            orderbutton.Click += orderbutton_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 48);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.Size = new Size(1925, 724);
            dataGridView1.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1925, 772);
            Controls.Add(dataGridView1);
            Controls.Add(flowLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Button loadbutton;
        private NumericUpDown numericUpDown1;
        private Button orderbutton;
        private DataGridView dataGridView1;
    }
}
