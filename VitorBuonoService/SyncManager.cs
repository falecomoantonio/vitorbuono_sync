﻿using log4net;
using System;
using System.ComponentModel;
using System.Timers;

namespace VitorBuonoService
{
    public class SyncManager : IDisposable
    {
        #region Private Members

        private readonly ILog logger = LogManager.GetLogger("SyncManager");
        private readonly Timer timerControl;
        private readonly BackgroundWorker backgroundWorker = new BackgroundWorker();

        #endregion

        #region Constructor

        public SyncManager()
        {
            backgroundWorker.DoWork += SyncTasks;
            timerControl = new Timer(5 * 1000);
            timerControl.Elapsed += TimerElapsed;
            timerControl.Start();

            backgroundWorker.RunWorkerAsync();
        }

        #endregion

        #region Private Methods

        private void SyncTasks(object sender, DoWorkEventArgs e)
        {
            try
            {
                Console.WriteLine("Iniciando a aplicação!");
            }
            catch(Exception ex)
            { 

            }
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }

            timerControl.Enabled = false;
            timerControl.Stop();
        }

        #endregion        
    }
}