using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotnet_lab10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            backgroundWorker1.DoWork += (o, a) =>
            {

                for (int i = 1; i <= 100; i++)
                {

                    if ((backgroundWorker1.CancellationPending == true))
                    {
                        a.Cancel = true;
                        return;
                    }
                    else
                    {
                        Thread.Sleep(500);
                        backgroundWorker1.ReportProgress(i);
                    }

                }
                a.Result = "Gotovo!";
            };

            backgroundWorker1.RunWorkerCompleted += (o, a) =>
            {
                if (a.Cancelled)
                {
                    lblStatus.Text = "Prekinuto";
                    pbStatus.Value = 0;
                }
                else
                    lblStatus.Text = a.Result.ToString();
            };

            backgroundWorker1.ProgressChanged += (o, a) =>
            {
                lblStatus.Text = a.ProgressPercentage + "%";
                pbStatus.Value = a.ProgressPercentage;
            };

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void btnObradi_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == false)
            {
                pbStatus.Value = 0;
                lblStatus.Text = "0%";
                backgroundWorker1.RunWorkerAsync();
                
            }
               
        }

        private void btnZaustavi_Click(object sender, EventArgs e)
        {

            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
        }
    }
}
