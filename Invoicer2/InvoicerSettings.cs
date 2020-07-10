using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Invoicer2
{
    public partial class InvoicerSettings : Form
    {
        string currentDir = Directory.GetCurrentDirectory();
        public static string jobListPath = "";
        public static bool settingsSaved = false;

        public InvoicerSettings()
        {
            InitializeComponent();
        }

        private void InvoicerSettings_Load(object sender, EventArgs e)
        {
            loadValues();
            settingsSaved = false;
        }

        private void loadValues()
        {
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load(currentDir + @"\Config\config.xml");
            string prefix = configDoc.SelectSingleNode("//prefix").InnerText;
            bool genPDFs = configDoc.SelectSingleNode("//genPdfs").InnerText == "true" ? true : false;
            string jobListPath = configDoc.SelectSingleNode("//jobListPath").InnerText;
            string backupFreq = configDoc.SelectSingleNode("//backupFreq").InnerText;
            string backupLocation = configDoc.SelectSingleNode("//backupLoc").InnerText;

            prefixTextBox.Text = prefix;
            pdfCheckBox.Checked = genPDFs;
            locationTextBox.Text = currentDir;
            jobListPathTextBox.Text = jobListPath;
            comboBoxBackupFreq.Text = backupFreq;
            backupTextBox.Text = backupLocation;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string prefix = prefixTextBox.Text;
            bool genPDFs = pdfCheckBox.Checked;
            string jobListPath = jobListPathTextBox.Text;
            string backupFreq = comboBoxBackupFreq.Text;
            string backupLocation = backupTextBox.Text;

            XmlDocument configDoc = new XmlDocument();
            configDoc.Load(currentDir + @"\Config\config.xml");
            configDoc.SelectSingleNode("//prefix").InnerText = prefix;
            configDoc.SelectSingleNode("//genPdfs").InnerText = genPDFs ? "true" : "false";
            configDoc.SelectSingleNode("//jobListPath").InnerText = jobListPath;
            configDoc.SelectSingleNode("//backupFreq").InnerText = backupFreq;
            configDoc.SelectSingleNode("//backupLoc").InnerText = backupLocation;
            configDoc.Save(currentDir + @"\Config\config.xml");

            settingsSaved = true;
        }

        private void gotoFolder_Click(object sender, EventArgs e)
        {
            Process.Start(currentDir);
        }

        private void selectJobListButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectJobListDialog = new OpenFileDialog();
            selectJobListDialog.InitialDirectory = currentDir;
            selectJobListDialog.Filter = "docx files (*.docx)|*.docx";
            selectJobListDialog.FilterIndex = 2;
            selectJobListDialog.RestoreDirectory = true;

            if (selectJobListDialog.ShowDialog() == DialogResult.OK)
            {
                jobListPath = selectJobListDialog.FileName;
                jobListPathTextBox.Text = jobListPath;
            }
        }

        private void comboBoxBackupFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBackupFreq.Text == "0")
            {
                backupFolderButton.Enabled = false;
            } else
            {
                backupFolderButton.Enabled = true;
            }
        }

        private void backupFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog selectBackupLocation = new FolderBrowserDialog();
            selectBackupLocation.RootFolder = Environment.SpecialFolder.MyComputer;
            
            if (selectBackupLocation.ShowDialog() == DialogResult.OK)
            {
                backupTextBox.Text = selectBackupLocation.SelectedPath;
            }
        }
    }
}
