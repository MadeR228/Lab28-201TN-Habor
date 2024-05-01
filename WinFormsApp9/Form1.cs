using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace WinFormsApp9
{
    public partial class Form1 : Form
    {
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHCreateDirectoryEx(IntPtr hwnd, string pszPath, IntPtr pSecurityDescriptor);

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                TreeNode node = new TreeNode(drive.Name);
                node.Tag = drive.RootDirectory;
                treeView1.Nodes.Add(node);
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DirectoryInfo dirInfo = e.Node.Tag as DirectoryInfo;
            if (dirInfo != null)
            {
                DisplayDirectoryContent(dirInfo, e.Node);
            }
        }


        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (e.Node != null && e.Node.Tag != null)
            {
                object tagObject = e.Node.Tag;

                if (tagObject is DriveInfo)
                {
                    DriveInfo driveInfo = tagObject as DriveInfo;
                    DisplayDriveInfo(driveInfo);
                }
                else if (tagObject is DirectoryInfo)
                {
                    DirectoryInfo dirInfo = tagObject as DirectoryInfo;
                    DisplayDirectoryInfo(dirInfo);
                }
                else if (tagObject is FileInfo)
                {
                    FileInfo fileInfo = tagObject as FileInfo;
                    DisplayFileInfo(fileInfo);
                }
            }
        }

        private void DisplayDriveInfo(DriveInfo driveInfo)
        {
            textBox1.Clear();
            textBox1.AppendText($"����� �����: {driveInfo.Name}\r\n");
            textBox1.AppendText($"��� �����: {driveInfo.DriveType}\r\n");
            textBox1.AppendText($"��������� �����: {driveInfo.TotalSize} ����\r\n");
            textBox1.AppendText($"��������� ������ ������: {driveInfo.AvailableFreeSpace} ����\r\n");
            textBox1.AppendText($"������ ������� �������: {driveInfo.DriveFormat}\r\n");
            textBox1.AppendText($"̳��� ����: {driveInfo.VolumeLabel}\r\n");
        }

        private void DisplayDirectoryInfo(DirectoryInfo dirInfo)
        {
            textBox1.Clear();
            textBox1.AppendText($"��'� �����: {dirInfo.Name}\r\n");
            textBox1.AppendText($"����: {dirInfo.FullName}\r\n");
            textBox1.AppendText($"���� ���������: {dirInfo.CreationTime}\r\n");
            // ������� ���� ���������� �����, �� ��� �����
        }

        private void DisplayFileInfo(FileInfo fileInfo)
        {
            textBox1.Clear();
            textBox1.AppendText($"��'� �����: {fileInfo.Name}\r\n");
            textBox1.AppendText($"�����: {fileInfo.Length} ����\r\n");
            textBox1.AppendText($"������� ����: {fileInfo.LastWriteTime}\r\n");
            // ������� ���� ���������� �����, �� ��� �����
        }

        private void DisplayDirectoryContent(DirectoryInfo directory, TreeNode parentNode)
        {
            try
            {
                parentNode.Nodes.Clear();

                foreach (DirectoryInfo subDir in directory.GetDirectories())
                {
                    TreeNode node = new TreeNode(subDir.Name);
                    node.Tag = subDir; // ��������� ��'���� DirectoryInfo �� ���������� Tag
                    parentNode.Nodes.Add(node);
                }

                foreach (FileInfo file in directory.GetFiles())
                {
                    TreeNode node = new TreeNode(file.Name);
                    node.Tag = file; // ��������� ��'���� FileInfo �� ���������� Tag
                    parentNode.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������� ��� ���������� ����� ��������: {ex.Message}");
            }
        }

        private void bntOpen_Click(object sender, EventArgs e)
        {
            object tagObject = treeView1.SelectedNode.Tag;

            FileInfo fileInfo = tagObject as FileInfo;

            ShellExecute shellExecute = new ShellExecute();
            shellExecute.Verb = "open"; // ���������� "open" ��� �������� �����
            shellExecute.Path = fileInfo.FullName; // ���� � �����
            shellExecute.Execute(); // ��������� ����
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("������� ������� ��� ���������.");
                return;
            }

            object tagObject = treeView1.SelectedNode.Tag;

            if (tagObject == null)
            {
                MessageBox.Show("������� ������� ��� ���������.");
                return;
            }

            var result = MessageBox.Show("�� �������, �� ������ �������� �������� �������?", "ϳ����������� ���������", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (tagObject is FileInfo)
                    {
                        FileInfo fileInfo = tagObject as FileInfo;

                        if (fileInfo != null && File.Exists(fileInfo.FullName))
                        {
                            File.Delete(fileInfo.FullName);
                            MessageBox.Show("���� ��������.");
                            treeView1.SelectedNode.Remove();
                        }
                        else
                        {
                            MessageBox.Show("������� ���� ��� ���������.");
                        }
                    }
                    else if (tagObject is DirectoryInfo)
                    {
                        DirectoryInfo directoryInfo = tagObject as DirectoryInfo;

                        if (directoryInfo != null && Directory.Exists(directoryInfo.FullName))
                        {
                            Directory.Delete(directoryInfo.FullName, true);
                            MessageBox.Show("����� ��������.");
                            treeView1.SelectedNode.Remove();
                        }
                        else
                        {
                            MessageBox.Show("������� ����� ��� ���������.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("������� ������� ��� ���������.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"������� ��� �������� ��������: {ex.Message}");
                }
            }
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Tag == null)
            {
                MessageBox.Show("������� �������, � ����� ������ �������� �������.");
                return;
            }

            string path = (treeView1.SelectedNode.Tag as DirectoryInfo)?.FullName;
            if (path == null)
            {
                MessageBox.Show("������� �������, � ����� ������ �������� �������.");
                return;
            }

            var createDialog = new Form();
            createDialog.Text = "��������� ��������";

            var textBox = new TextBox();
            textBox.Location = new Point(10, 10);
            textBox.Width = 200;
            createDialog.Controls.Add(textBox);

            var comboBox = new ComboBox();
            comboBox.Items.AddRange(new object[] { "�����", "����" });
            comboBox.Location = new Point(10, 40);
            comboBox.Width = 200;
            createDialog.Controls.Add(comboBox);

            var okButton = new Button();
            okButton.Text = "��";
            okButton.Location = new Point(10, 70);
            okButton.Click += (s, args) =>
            {
                string itemName = textBox.Text;
                string selectedItem = comboBox.SelectedItem?.ToString();

                if (string.IsNullOrWhiteSpace(itemName))
                {
                    MessageBox.Show("������ ��'� ��������.");
                    return;
                }

                try
                {
                    string newPath = Path.Combine(path, itemName);

                    if (Directory.Exists(newPath) || File.Exists(newPath))
                    {
                        MessageBox.Show("���� ��� ����� � ����� ������ ��� ����.");
                        return;
                    }

                    if (MessageBox.Show($"�� �������, �� ������ �������� {itemName}?", "ϳ�����������", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (selectedItem == "�����")
                        {
                            Directory.CreateDirectory(newPath);
                            MessageBox.Show("����� ��������.");
                        }
                        else if (selectedItem == "����")
                        {
                            using (File.Create(newPath)) { }
                            MessageBox.Show("���� ��������.");
                        }

                        DisplayDirectoryContent(new DirectoryInfo(path), treeView1.SelectedNode);
                        createDialog.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"������� �� ��� ��������� ��������: {ex.Message}");
                }
            };
            createDialog.Controls.Add(okButton);

            var cancelButton = new Button();
            cancelButton.Text = "������";
            cancelButton.Location = new Point(120, 70);
            cancelButton.Click += (s, args) => createDialog.Close();
            createDialog.Controls.Add(cancelButton);

            createDialog.ClientSize = new Size(240, 120);
            createDialog.StartPosition = FormStartPosition.CenterParent;

            createDialog.ShowDialog();
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Tag == null)
            {
                MessageBox.Show("������� �������, ���� ������� ����������.");
                return;
            }

            string selectedPath = (treeView1.SelectedNode.Tag as FileSystemInfo)?.FullName;
            if (selectedPath == null)
            {
                MessageBox.Show("������� �������, ���� ������� ����������.");
                return;
            }

            var moveDialog = new FolderBrowserDialog();
            moveDialog.Description = "������� ���� ��� ���������� ��������.";
            moveDialog.ShowNewFolderButton = true;

            moveDialog.RootFolder = Environment.SpecialFolder.MyComputer;

            if (moveDialog.ShowDialog() == DialogResult.OK)
            {
                string destinationPath = moveDialog.SelectedPath;

                try
                {
                    string newName = Path.Combine(destinationPath, Path.GetFileName(selectedPath));

                    if (File.Exists(selectedPath))
                    {
                        File.Move(selectedPath, newName);
                        MessageBox.Show("���� ���������.");
                    }
                    else if (Directory.Exists(selectedPath))
                    {
                        if (!Path.GetPathRoot(selectedPath).Equals(Path.GetPathRoot(destinationPath), StringComparison.OrdinalIgnoreCase))
                        {
                            Directory.CreateDirectory(newName);
                            DirectoryCopy(selectedPath, newName, true);

                            Directory.Delete(selectedPath, true);
                        }
                        else
                        {
                            Directory.Move(selectedPath, newName);
                        }
                        MessageBox.Show("����� �����������.");
                    }

                    DisplayDirectoryContent(new DirectoryInfo(Path.GetDirectoryName(selectedPath)), treeView1.SelectedNode.Parent);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"������� ��� ��������� ��������: {ex.Message}");
                }
            }
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}