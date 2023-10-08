using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace xdPatcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Info inf = new Info();

        // list of xdelta files
        List<string> files = new List<string>();
        string romExtension = "";
        string[] ROMExtensionChoices = { ".z64", ".nds" };

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
            open.Filter = "All files|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                ogROM.Text = open.FileName;
                romExtension = Path.GetExtension(open.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var folderBrowser = new CommonOpenFileDialog { IsFolderPicker = true };
            string fn = "";

            if (folderBrowser.ShowDialog() == CommonFileDialogResult.Ok)
            {
                fn = folderBrowser.FileName;
                xdeltaDir.Text = folderBrowser.FileName;
                outDir.Text = Path.GetDirectoryName(ogROM.Text) + @"\Patched\";
            }    

            files = Directory.GetFiles(fn).ToList();

            // remove files that don't have a .xdelta extension.
            string targetExtension = ".xdelta";
            files.RemoveAll(fileName => !fileName.EndsWith(targetExtension));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var folderBrowser = new CommonOpenFileDialog { IsFolderPicker = true };
            if (folderBrowser.ShowDialog() == CommonFileDialogResult.Ok)
            {
                String fn = folderBrowser.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int successCount = 0;
            //List<string> failedPatches = new List<string>();

            // make copies of the ROM
            foreach (string f in files) {

                // make a copy of the OG ROM, rename it, and put it in a patched folder directory
                string sourcePath = ogROM.Text;
                string destFolder = Path.GetDirectoryName(sourcePath) + @"\Patched\";

                // make folder if it doesn't already exist
                if (!Directory.Exists(destFolder)) { 
                    Directory.CreateDirectory(destFolder);
                }

                string patchedFileDest = destFolder + Path.GetFileNameWithoutExtension(f) + romExtension;

                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "xdelta3.exe";
                proc.StartInfo.Arguments = " -d -s " + '"' + sourcePath + '"' + " " + '"' + f + '"' + " " + '"' + patchedFileDest + '"';
                proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                toolStripStatusLabel4.Text = "Applying patches, gimme a sec...";
                proc.Start();
                proc.WaitForExit();
                try
                {
                    toolStripStatusLabel4.Text = $"Applied patch {f} to {patchedFileDest}";
                    successCount++;
                }
                catch (Exception ex)
                {
                    toolStripStatusLabel4.Text = $"Failed to apply patch {f} to {patchedFileDest}";
                    //failedPatches.Add(f);
                }
            }
            toolStripStatusLabel4.Text = "Applied patches.";
            MessageBox.Show($"Applied {successCount}" + "/" + files.Count() + " patches.");

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Info inf = new Info();
            inf.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Width = 536;
            //this.Height = 209;
            ogROM.Select();
            if(WindowState == FormWindowState.Maximized)
            {
                Application.Exit();
            }
        }

        private void label7_Click(object sender, EventArgs e){}

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "Browse for an original ROM";
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "";
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "Browse for a xdelta patch file";
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "";
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "Choose the output file";
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "";
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "Browse for an original ROM";
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "";
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "Browse for a modified ROM";
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "";
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "Choose the output file";
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "";
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "Apply patch";
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "";
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "Make patch";
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "";
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "Shows the info";
        }

        private void button9_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "";
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        { 
        }
    }
}
