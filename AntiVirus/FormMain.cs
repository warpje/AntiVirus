using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntiVirus
{

    public partial class FormMain : Form
    {

        public FormMain()
        {
            InitializeComponent();
        }

        private void Refresh (int refresh = 1000)
        {
            ListViewProcess.Items.Clear();
            Process[] allProc = Process.GetProcesses();
            foreach (Process p in allProc)
            {
                ListViewItem Processes = new ListViewItem(p.ProcessName);
                Processes.SubItems.Add(Convert.ToString(p.Id));
                Processes.SubItems.Add(Convert.ToString(CPInfo.GetProcessCPU(p.ProcessName) + "%"));
                Processes.SubItems.Add(Misc.SizeSuffix(p.VirtualMemorySize64));
                Processes.SubItems.Add("False");
                
                ListViewProcess.Items.Add(Processes);
            }
        }

        public async Task DoMyThing(CancellationToken token = default(CancellationToken))
        {
            while (!token.IsCancellationRequested)
            {
                ProgressCPU.Value = CPInfo.CPUInfo();
                ProgressMemory.Value = CPInfo.MemInfo();
                ProgressDisk.Value = CPInfo.DiskInfo();
                try
                {
                    await Task.Delay(900);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
            while (!token.IsCancellationRequested)
            {
                this.Refresh();
                LabelUpload.Text = Internet.CheckInternetSpeed();
                LabelPing.Text = Internet.CheckPing();
                try
                {
                    await Task.Delay(TimeSpan.FromMinutes(2), token);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            DoMyThing();
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
             Refresh();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ProgressCPU.Value = CPInfo.CPUInfo();
            ProgressMemory.Value = CPInfo.MemInfo();
            ProgressDisk.Value = CPInfo.DiskInfo();
            LabelUpload.Text = Internet.CheckInternetSpeed();
            LabelPing.Text = Internet.CheckPing();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void killToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName(ListViewProcess.SelectedItems[0].Text))
            {
                process.Kill();
            }
            Refresh();
        }

        private void ButtonScan_Click(object sender, EventArgs e)
        {

        }
    }
}
