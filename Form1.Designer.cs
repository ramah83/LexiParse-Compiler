using System.Windows.Forms;
using System.Drawing;

namespace ScannerParserProject
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            txtInput = new TextBox();
            btnScan = new Button();
            btnParse = new Button();
            label1 = new Label();
            label2 = new Label();
            btnLoadExample = new Button();
            btnClear = new Button();
            splitContainer1 = new SplitContainer();
            txtOutput = new TextBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // txtInput
            // 
            txtInput.BackColor = Color.OldLace;
            txtInput.Dock = DockStyle.Fill;
            txtInput.Font = new Font("Consolas", 13.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtInput.Location = new Point(0, 0);
            txtInput.Margin = new Padding(4, 5, 4, 5);
            txtInput.Multiline = true;
            txtInput.Name = "txtInput";
            txtInput.ScrollBars = ScrollBars.Both;
            txtInput.Size = new Size(898, 282);
            txtInput.TabIndex = 0;
            // 
            // btnScan
            // 
            btnScan.BackColor = Color.BlanchedAlmond;
            btnScan.Font = new Font("Microsoft Sans Serif", 12F);
            btnScan.ForeColor = Color.Brown;
            btnScan.Location = new Point(16, 18);
            btnScan.Margin = new Padding(4, 5, 4, 5);
            btnScan.Name = "btnScan";
            btnScan.Size = new Size(172, 46);
            btnScan.TabIndex = 2;
            btnScan.Text = "Scanner";
            btnScan.UseVisualStyleBackColor = false;
            btnScan.Click += btnScan_Click;
            // 
            // btnParse
            // 
            btnParse.BackColor = Color.BlanchedAlmond;
            btnParse.Font = new Font("Microsoft Sans Serif", 12F);
            btnParse.ForeColor = Color.Brown;
            btnParse.Location = new Point(196, 18);
            btnParse.Margin = new Padding(4, 5, 4, 5);
            btnParse.Name = "btnParse";
            btnParse.Size = new Size(172, 46);
            btnParse.TabIndex = 3;
            btnParse.Text = "Parser";
            btnParse.UseVisualStyleBackColor = false;
            btnParse.Click += btnParse_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.NavajoWhite;
            label1.Location = new Point(16, 77);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(143, 27);
            label1.TabIndex = 4;
            label1.Text = "Source Code:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(16, 462);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(63, 18);
            label2.TabIndex = 5;
            label2.Text = "Output:";
            // 
            // btnLoadExample
            // 
            btnLoadExample.BackColor = Color.BlanchedAlmond;
            btnLoadExample.Font = new Font("Microsoft Sans Serif", 12F);
            btnLoadExample.ForeColor = Color.Brown;
            btnLoadExample.Location = new Point(556, 18);
            btnLoadExample.Margin = new Padding(4, 5, 4, 5);
            btnLoadExample.Name = "btnLoadExample";
            btnLoadExample.Size = new Size(177, 46);
            btnLoadExample.TabIndex = 6;
            btnLoadExample.Text = "Show Example";
            btnLoadExample.UseVisualStyleBackColor = false;
            btnLoadExample.Click += btnLoadExample_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.BlanchedAlmond;
            btnClear.Font = new Font("Microsoft Sans Serif", 12F);
            btnClear.ForeColor = Color.Brown;
            btnClear.Location = new Point(376, 18);
            btnClear.Margin = new Padding(4, 5, 4, 5);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(172, 46);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(16, 108);
            splitContainer1.Margin = new Padding(4, 5, 4, 5);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(txtInput);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(txtOutput);
            splitContainer1.Size = new Size(898, 565);
            splitContainer1.SplitterDistance = 282;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 8;
            // 
            // txtOutput
            // 
            txtOutput.BackColor = Color.OldLace;
            txtOutput.Dock = DockStyle.Fill;
            txtOutput.Font = new Font("Consolas", 12F, FontStyle.Italic);
            txtOutput.Location = new Point(0, 0);
            txtOutput.Margin = new Padding(4, 5, 4, 5);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.ReadOnly = true;
            txtOutput.ScrollBars = ScrollBars.Both;
            txtOutput.Size = new Size(898, 277);
            txtOutput.TabIndex = 1;
            // 
            // button1
            // 
            button1.BackColor = Color.BlanchedAlmond;
            button1.Font = new Font("Microsoft Sans Serif", 12F);
            button1.ForeColor = Color.Brown;
            button1.Location = new Point(741, 18);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(177, 46);
            button1.TabIndex = 9;
            button1.Text = "Close Program";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SaddleBrown;
            ClientSize = new Size(930, 690);
            Controls.Add(button1);
            Controls.Add(splitContainer1);
            Controls.Add(btnClear);
            Controls.Add(btnLoadExample);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnParse);
            Controls.Add(btnScan);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Compiler";
            Load += Form1_Load_1;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLoadExample;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TextBox txtOutput;
        private Button button1;
    }
}