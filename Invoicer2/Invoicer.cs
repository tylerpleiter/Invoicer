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
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;

namespace Invoicer2
{
    public partial class Invoicer : Form
    {
        private const int MAXJOBSPERINVOICE = 10;
        string currentDir = Directory.GetCurrentDirectory();
        string jobListPath = "";
        DateTime currentTime;
        string prefix;
        bool genPDFs;
        bool genReady = true;
        int backupFreq;
        string backupLocation, lastBackup;

        public Invoicer()
        {
            InitializeComponent();
        }

        private void Invoicer_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(currentDir + @"\Config"))
            {
                Directory.CreateDirectory(currentDir + @"\Config");
            }
            updateLog("Program start--------------");
            setupFiles();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            updateLog("Loading settings");
            InvoicerSettings invoicerSettings = new InvoicerSettings();
            invoicerSettings.ShowDialog();
            if (InvoicerSettings.settingsSaved)
            {
                updateLog("Settings saved");
                jobListPath = InvoicerSettings.jobListPath;
                setupFiles();
            }
        }

        private void buttonTransfer_Click(object sender, EventArgs e)
        {
            updateLog("Transferring files");
            string[] outgoingDocFiles = Directory.GetFiles(currentDir + @"\Outgoing", "*.docx");
            string[] outgoingPdfFiles = Directory.GetFiles(currentDir + @"\Outgoing", "*.pdf");
            string dest;
            if (outgoingDocFiles.Length != 0)
            {
                for (int i = 0; i < outgoingDocFiles.Length; i++)
                {
                    dest = outgoingDocFiles[i].Replace("Outgoing", "Invoices\\Docs");
                    try
                    {
                        File.Copy(outgoingDocFiles[i], dest, false);
                    }
                    catch { } 
                    finally
                    {
                        File.Delete(outgoingDocFiles[i]);
                    }
                }
                if (outgoingPdfFiles.Length != 0)
                {
                    for (int i = 0; i < outgoingPdfFiles.Length; i++)
                    {
                        dest = outgoingPdfFiles[i].Replace("Outgoing", "Invoices\\PDFs");
                        try
                        {
                            File.Copy(outgoingPdfFiles[i], dest, false);
                        }
                        catch { }
                        finally
                        {
                            File.Delete(outgoingPdfFiles[i]);
                        }
                    }
                }
                updateLog("Transfer complete");
            }
            else
            {
                updateLog("Outgoing folder empty");
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            updateLog("Opening invoices");
            Process.Start(currentDir + @"\Outgoing");
            string[] outgoingFiles = Directory.GetFiles(currentDir + @"\Outgoing", "*.pdf");
            if (outgoingFiles.Length != 0)
            {
                for (int i = 0; i < outgoingFiles.Length; i++)
                {
                    Process.Start(outgoingFiles[i]);
                }
            } else
            {
                updateLog("Outgoing folder empty");
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            buttonSettings.Enabled = false;
            buttonTransfer.Enabled = false;
            buttonOpen.Enabled = false;
            updateLog("Generating invoices");
            generateInvoices();
            buttonSettings.Enabled = true;
            buttonTransfer.Enabled = true;
            buttonOpen.Enabled = true;
        }

        private void generateInvoices()
        {
            // Check word already open
            try
            {
                Microsoft.Office.Interop.Word.Application wordObj;
                wordObj = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
                for (int i = 0; i < wordObj.Windows.Count; i++)
                {
                    object a = i + 1;
                    Window objWin = wordObj.Windows.get_Item(ref a);
                    if (objWin.Caption == jobListPath.Substring(jobListPath.LastIndexOf(@"\") + 1).ToString())
                    {
                        wordObj.Documents[a].Save();
                        wordObj.Documents[a].Close();
                        if (wordObj.Windows.Count == 0)
                        {
                            wordObj.Quit();
                        }
                        updateLog("Closing existing job list");
                    }
                }
            } catch
            {
                
            }

            updateLog("Starting Microsoft Word");
            var wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.Visible = true;
            updateLog("Opening job list");
            Document jobListDoc = wordApp.Documents.Open(jobListPath);
            Document invoiceDoc;

            // Find next row to fill
            jobListDoc.Tables[1].Rows.Add(jobListDoc.Tables[1].Rows.Last); // Add row to end to prevent index error
            int nextToFill = 2;
            while (jobListDoc.Tables[1].Columns[6].Cells[nextToFill].Range.Text.Length == 10)
            {
                nextToFill++;
            }

            // Check next row need to be processed (check desc and num empty)
            if (jobListDoc.Tables[1].Rows[nextToFill].Cells[1].Range.Text.Length == 2 ||
                jobListDoc.Tables[1].Rows[nextToFill].Cells[5].Range.Text.Length == 2)
            {
                updateLog("No jobs to process");
                jobListDoc.Application.Activate();
                jobListDoc.Close();
                wordApp.Quit();
                return;
            }

            // Find last row to fill
            int lastToFill = nextToFill + 1;
            bool lastRow = false;
            while (!lastRow)
            {
                if (jobListDoc.Tables[1].Rows[lastToFill].Cells[1].Range.Text.Length == 2 ||
                jobListDoc.Tables[1].Rows[lastToFill].Cells[5].Range.Text.Length == 2)
                {
                    lastToFill--;
                    lastRow = true;
                } else
                {
                    lastToFill++;
                }
            }
            //jobListDoc.Range(jobListDoc.Tables[1].Rows[nextToFill].Cells[1].Range.Start, jobListDoc.Tables[1].Rows[lastToFill].Cells[8].Range.Start).Select();
            
            string jobAddress, invoiceNumber, jobNumber;
            string[] jobDescription = new string[MAXJOBSPERINVOICE];
            string[] jobCost = new string[MAXJOBSPERINVOICE];

            bool[] combined = new bool[lastToFill - nextToFill + 1];
            int invCounter = 1; // Counts as invoices generates - used in log update
            int dupCounter = 0; // Counts total duplicates in set
            
            // Iterate through invoices to fill
            for (int i = nextToFill; i <= lastToFill; i++)
            {
                if (!combined[i-nextToFill] && (jobListDoc.Tables[1].Rows[i].Cells[6].Range.Text.Length == 2)) // Check not duplicate and not already filled
                {
                    // Clear description & cost arrays
                    for (int l = 0; l < MAXJOBSPERINVOICE; l++)
                    {
                        jobDescription[l] = null;
                        jobCost[l] = null;
                    }

                    // Collect data for next to fill
                    jobAddress = jobListDoc.Tables[1].Rows[i].Cells[2].Range.Text.ToString();
                    jobAddress = jobAddress.Substring(0, jobAddress.Length - 2);
                    invoiceNumber = jobListDoc.Tables[1].Rows[i].Cells[5].Range.Text.ToString();
                    invoiceNumber = invoiceNumber.Substring(0, invoiceNumber.Length - 2);
                    jobNumber = jobListDoc.Tables[1].Rows[i].Cells[3].Range.Text.ToString();
                    jobNumber = jobNumber.Substring(0, jobNumber.Length - 2);
                    jobDescription[0] = jobListDoc.Tables[1].Rows[i].Cells[1].Range.Text.ToString();
                    jobDescription[0] = jobDescription[0].Substring(0, jobDescription[0].Length - 2);
                    jobCost[0] = jobListDoc.Tables[1].Rows[i].Cells[7].Range.Text.ToString();
                    jobCost[0] = jobCost[0].Substring(0, jobCost[0].Length - 2);
                    
                    int k = 0; // counter for duplicates for current invoice number

                    // Check rest of invoices for identical invoice numbers
                    for (int j = i + 1; j <= lastToFill; j++)
                    {
                        if (jobListDoc.Tables[1].Rows[i].Cells[5].Range.Text == jobListDoc.Tables[1].Rows[j].Cells[5].Range.Text)
                        {
                            combined[j-nextToFill] = true; // prevent from creating duplicate invoice
                            // Add description & cost to array job data array
                            k++; dupCounter++;
                            jobDescription[k] = jobListDoc.Tables[1].Rows[j].Cells[1].Range.Text.ToString();
                            jobDescription[k] = jobDescription[k].Substring(0, jobDescription[k].Length - 2);
                            jobCost[k] = jobListDoc.Tables[1].Rows[j].Cells[7].Range.Text.ToString();
                            jobCost[k] = jobCost[k].Substring(0, jobCost[k].Length - 2);
                        }
                    }

                    updateLog("Generating invoice #" + invoiceNumber + @" (" + (invCounter++).ToString() + @"/" + (lastToFill - nextToFill + 1 - dupCounter).ToString() + @")");

                    // Add data to invoice doc
                    invoiceDoc = wordApp.Documents.Open(currentDir + @"\Config\invoiceTemplate.dotx", true, true);
                    invoiceDoc.Application.Activate();
                    object missing = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Word.Range range;

                    range = invoiceDoc.StoryRanges[WdStoryType.wdMainTextStory];
                    if (!range.Find.Execute("<<number>>", ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, 
                        invoiceNumber, WdReplace.wdReplaceAll, ref missing, ref missing, ref missing, ref missing))
                    {
                        updateLog("<<number>> field not found");
                        invoiceDoc.Application.Activate();
                        invoiceDoc.Close();
                        break;
                    }
                    range = invoiceDoc.StoryRanges[WdStoryType.wdMainTextStory];
                    if (!range.Find.Execute("<<job_address>>", ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                        jobAddress, WdReplace.wdReplaceAll, ref missing, ref missing, ref missing, ref missing))
                    {
                        updateLog("<<job_address>> field not found");
                        invoiceDoc.Application.Activate();
                        invoiceDoc.Close();
                        break;
                    }
                    range = invoiceDoc.StoryRanges[WdStoryType.wdMainTextStory];
                    if (!range.Find.Execute("<<job_number>>", ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                        jobNumber, WdReplace.wdReplaceAll, ref missing, ref missing, ref missing, ref missing))
                    {
                        updateLog("<<job_number>> field not found");
                        invoiceDoc.Application.Activate();
                        invoiceDoc.Close();
                        break;
                    }

                    range = invoiceDoc.StoryRanges[WdStoryType.wdMainTextStory];
                    if (!range.Find.Execute("<<job>>"))
                    {
                        updateLog("<<job>> field not found");
                        invoiceDoc.Application.Activate();
                        invoiceDoc.Close();
                        break;
                    }
                    range.Select();
                    for (int n = 0; n <= k; n++)
                    {
                        wordApp.Selection.Text = "1";
                        wordApp.Selection.MoveRight(WdUnits.wdCell);
                        wordApp.Selection.Text = jobDescription[n];
                        wordApp.Selection.MoveRight(WdUnits.wdCell);
                        wordApp.Selection.Text = jobCost[n];
                        wordApp.Selection.MoveRight(WdUnits.wdCell);
                        wordApp.Selection.Text = jobCost[n];
                        wordApp.Selection.MoveLeft(WdUnits.wdCell, 3);
                        wordApp.Selection.MoveDown();
                    }

                    // Caculate total cost
                    double totalcost = 0.0;
                    for (int n = 0; n <= k; n++)
                    {
                        if (jobCost[n] != null)
                        {
                            totalcost += Convert.ToDouble(jobCost[n].Substring(1,jobCost[n].Length-1));
                        }
                    }
                    range = invoiceDoc.StoryRanges[WdStoryType.wdMainTextStory];
                    range.Find.Execute("<<total_cost>>", ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                        @"$" + totalcost.ToString("#####.00"), WdReplace.wdReplaceAll, ref missing, ref missing, ref missing, ref missing);

                    // Save & close invoice
                    invoiceDoc.Application.Activate();
                    DialogResult retVal = MessageBox.Show("Check invoice then confirm", "Check Invoice", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (retVal != DialogResult.OK)
                    {
                        invoiceDoc.Application.Activate();
                        invoiceDoc.Close(false);
                        jobListDoc.Application.Activate();
                        jobListDoc.Close(false);
                        wordApp.Quit();
                        updateLog("Invoice generation cancelled");
                        return;
                    }
                    updateLog("Saving invoice");
                    invoiceDoc.SaveAs2(currentDir + @"\Outgoing\" + prefix + invoiceNumber + @".docx", WdSaveFormat.wdFormatXMLDocument);
                    if (genPDFs)
                    {
                        updateLog("Creating PDF");
                        invoiceDoc.ExportAsFixedFormat(currentDir + @"\Outgoing\" + prefix + invoiceNumber + @".pdf", WdExportFormat.wdExportFormatPDF);
                    }
                    invoiceDoc.Application.Activate();
                    invoiceDoc.Close();

                    // Add completion date to job list and save
                    updateLog("Updating job list");
                    for (int j = i; j <= lastToFill; j++) // find other jobs from same invoice
                    {
                        string temp = jobListDoc.Tables[1].Rows[j].Cells[5].Range.Text;
                        temp = temp.Substring(0, temp.Length - 2);
                        if (invoiceNumber == temp)
                        {
                            jobListDoc.Tables[1].Rows[j].Cells[6].Range.Text = currentTime.ToString(@"dd/MM/yy");
                        }
                    }
                    jobListDoc.Save();
                }
            }

            // Close job list file
            jobListDoc.Application.Activate();
            jobListDoc.Close();
            wordApp.Quit();

            updateLog("Invoices complete!");

            //Document invoiceTest = wordApp.Documents.Open(currentDir + @"\Config\invoiceTemplate.dotx");
        }
        
        private void setupFiles()
        {
            // Program directory:
            //   Invoicer
            //   --Config
            //   ----config.xml
            //   ----invoiceTemplate.dotx
            //   ----log.txt
            //   --Invoices
            //   ----Docs
            //   ----PDFs
            //   --Outgoing
            //   --Invoicer.exe
            //   JobList.docx ~anywhere
            genReady = true;

            updateLog("Checking program files");
            if (!Directory.Exists(currentDir + @"\Outgoing"))
            {
                updateLog("Creating Outgoing dir");
                Directory.CreateDirectory(currentDir + @"\Outgoing");
            } else {
                updateLog("Outgoing dir exists");
            }

            if (!Directory.Exists(currentDir + @"\Invoices"))
            {
                updateLog("Creating Invoices dir");
                Directory.CreateDirectory(currentDir + @"\Invoices");
            }
            else
            {
                updateLog("Invoices dir exists");
            }

            // Check Invoices sub-directories
            if (!Directory.Exists(currentDir + @"\Invoices\Docs"))
            {
                updateLog("Creating Docs sud-dir");
                Directory.CreateDirectory(currentDir + @"\Invoices\Docs");
            }
            else
            {
                updateLog("Docs sub-dir exists");
            }

            if (!Directory.Exists(currentDir + @"\Invoices\PDFs"))
            {
                updateLog("Creating PDFs sud-dir");
                Directory.CreateDirectory(currentDir + @"\Invoices\PDFs");
            }
            else
            {
                updateLog("PDFs sub-dir exists");
            }

            if (!Directory.Exists(currentDir + @"\Config"))
            {
                updateLog("Creating Config dir");
                Directory.CreateDirectory(currentDir + @"\Config");
            }
            else
            {
                updateLog("Config dir exists");
            }

            if (!File.Exists(currentDir + @"\Config\config.xml"))
            {
                updateLog("Creating config file");
                using (XmlWriter configFile = XmlWriter.Create(currentDir + @"\Config\config.xml"))
                {
                    configFile.WriteStartElement("settings");
                    configFile.WriteElementString("prefix", "I");
                    configFile.WriteElementString("genPdfs", "true");
                    configFile.WriteElementString("jobListPath", "");
                    configFile.WriteElementString("backupFreq", "0");
                    configFile.WriteElementString("backupLoc", "");
                    configFile.WriteElementString("lastBackup", DateTime.Now.ToString());
                    configFile.WriteEndElement();
                    configFile.Flush();
                }
            } else
            {
                updateLog("Config file exists");
            }
            
            loadSettings();
            updateLog("Settings loaded");

            if (!File.Exists(currentDir + @"\Config\invoiceTemplate.dotx"))
            {
                updateLog("Invoice Template Missing");
                MessageBox.Show("Please copy 'invoiceTemplate.dotx' file to Config folder",
                    "Template File missing");
                genReady = false;
            }
            else
            {
                updateLog("invoiceTemplate exists");
            }

            if (jobListPath == "" || !File.Exists(jobListPath))
            {
                updateLog("Job List missing");
                MessageBox.Show("Please go to settings and select Job List file", "Job List Missing");
                genReady = false;
            } else
            {
                updateLog("Job List exists");
            }
            
            if (backupFreq != 0)
            {
                checkBackup();
            } else
            {
                updateLog("Backup off");
            }

            string readyStat = genReady ? "Ready" : "Files missing";
            updateLog("Setup complete - " + readyStat);

            if (genReady)
            {
                buttonTransfer.Enabled = true;
                buttonGenerate.Enabled = true;
                buttonOpen.Enabled = true;
            }

        }

        private void loadSettings()
        {
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load(currentDir + @"\Config\config.xml");
            prefix = configDoc.SelectSingleNode("//prefix").InnerText;
            genPDFs = configDoc.SelectSingleNode("//genPdfs").InnerText == "true" ? true : false;
            jobListPath = configDoc.SelectSingleNode("//jobListPath").InnerText;
            backupFreq = Convert.ToInt32(configDoc.SelectSingleNode("//backupFreq").InnerText);
            backupLocation = configDoc.SelectSingleNode("//backupLoc").InnerText;
            lastBackup = configDoc.SelectSingleNode("//lastBackup").InnerText;
        }

        private void checkBackup()
        {
            // Check backup directory exists
            if (!Directory.Exists(backupLocation))
            {
                updateLog("Backup location invalid");
                return;
            }

            // Check time since last backup
            currentTime = DateTime.Now;
            DateTime lastBackupTime = DateTime.Parse(lastBackup);
            int daysSinceBackup = (currentTime - lastBackupTime).Days;
            if (daysSinceBackup >= backupFreq) // daysSinceBackup >= backupFreq
            {
                // Backup
                updateLog("Backing up files");

                // Check backup destination directories exists else create
                if (!Directory.Exists(backupLocation + @"\InvoiceBackup"))
                {
                    Directory.CreateDirectory(backupLocation + @"\InvoiceBackup");
                }
                if (!Directory.Exists(backupLocation + @"\InvoiceBackup\Docs"))
                {
                    Directory.CreateDirectory(backupLocation + @"\InvoiceBackup\Docs");
                }
                if (!Directory.Exists(backupLocation + @"\InvoiceBackup\PDFs"))
                {
                    Directory.CreateDirectory(backupLocation + @"\InvoiceBackup\PDFs");
                }

                // Copy files across
                string[] docFiles = Directory.GetFiles(currentDir + @"\Invoices\Docs", "*.docx");
                string[] pdfFiles = Directory.GetFiles(currentDir + @"\Invoices\PDFs", "*.pdf");
                int copiedFiles = 0;
                if (docFiles.Length != 0)
                {
                    for (int i = 0; i < docFiles.Length; i++)
                    {
                        try
                        {
                            File.Copy(docFiles[i], backupLocation + @"\InvoiceBackup\Docs\" + docFiles[i].Substring(docFiles[i].LastIndexOf(@"\") + 1));
                            copiedFiles++;
                        }
                        catch { };
                    }
                }
                if (pdfFiles.Length != 0)
                {
                    for (int i = 0; i < pdfFiles.Length; i++)
                    {
                        try
                        {
                            File.Copy(pdfFiles[i], backupLocation + @"\InvoiceBackup\PDFs\" + pdfFiles[i].Substring(pdfFiles[i].LastIndexOf(@"\") + 1));
                            copiedFiles++;
                        }
                        catch { };
                    }
                }
                updateLog("Backup complete - " + copiedFiles.ToString() + " files copied");


            } else
            {
                updateLog("Last backup " + daysSinceBackup + " days ago");
            }

        }
        
        public void updateLog(string strUpdate)
        {
            currentTime = DateTime.Now;
            textBox1.AppendText(Environment.NewLine + @"[" + currentTime.ToString("HH:mm:ss") + @"] " + strUpdate);
            using (StreamWriter log = File.AppendText(currentDir + @"\Config\log.txt"))
            {
                log.WriteLine(@"[" + currentTime.ToString("yy-MM-dd HH:mm:ss") + @"] " + strUpdate);
            }
        }
        
    }
}
