namespace HongHu
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    internal class MyBackgroundWorker
    {
        public BackgroundWorker backgroundWorker1 = new BackgroundWorker();

        public MyBackgroundWorker()
        {
            this.backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = this.ComputeFibonacci((int) e.Argument, worker, e);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
            }
        }

        private void cancelAsync()
        {
            this.backgroundWorker1.CancelAsync();
        }

        private long ComputeFibonacci(int n, BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            return 0L;
        }

        private void InitializeBackgoundWorker()
        {
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
        }

        public void startAsync()
        {
            this.backgroundWorker1.RunWorkerAsync();
        }
    }
}

