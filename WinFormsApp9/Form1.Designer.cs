namespace WinFormsApp9
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
            treeView1 = new TreeView();
            textBox1 = new TextBox();
            btnOpen = new Button();
            btnDelete = new Button();
            btnCreate = new Button();
            btnMove = new Button();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.Location = new Point(37, 35);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(428, 346);
            treeView1.TabIndex = 0;
            treeView1.AfterSelect += treeView1_AfterSelect;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(484, 35);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(304, 181);
            textBox1.TabIndex = 2;
            // 
            // btnOpen
            // 
            btnOpen.Location = new Point(484, 222);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(75, 23);
            btnOpen.TabIndex = 3;
            btnOpen.Text = "Відкрити";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += bntOpen_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(646, 222);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Видалити";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(565, 222);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 5;
            btnCreate.Text = "Створити";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // btnMove
            // 
            btnMove.Location = new Point(552, 251);
            btnMove.Name = "btnMove";
            btnMove.Size = new Size(101, 23);
            btnMove.TabIndex = 6;
            btnMove.Text = "Перемістити";
            btnMove.UseVisualStyleBackColor = true;
            btnMove.Click += btnMove_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnMove);
            Controls.Add(btnCreate);
            Controls.Add(btnDelete);
            Controls.Add(btnOpen);
            Controls.Add(textBox1);
            Controls.Add(treeView1);
            Name = "Form1";
            Text = "Габор 201-ТН";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView treeView1;
        private TextBox textBox1;
        private Button btnOpen;
        private Button btnDelete;
        private Button btnCreate;
        private Button btnMove;
    }
}
