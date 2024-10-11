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

namespace Emailch
{
    public partial class splash : Form
    {
        private BackgroundWorker backgroundWorker;
        public splash()
        {
            InitializeComponent();
            // Initialize the BackgroundWorker
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += bw1_DoWork;
            backgroundWorker.RunWorkerCompleted += bw1_RunWorkerCompleted;

            // Start the BackgroundWorker when the form loads
            this.Load += Splash_Load;
        }
        private void Splash_Load(object sender, EventArgs e)
        {
            // Start the background worker when the splash form loads
            backgroundWorker.RunWorkerAsync();
        }
               
        // When the background work is completed, switch to the Launch form
        private void bw1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Hide the splash form
            this.Hide();

            // Show the Launch form
            Launch launchForm = new Launch();
            launchForm.Show();
        }

        private void bw1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Simulate a delay (3 seconds)
            Thread.Sleep(3000); // 3000 milliseconds = 3 seconds
        }
    }
}
