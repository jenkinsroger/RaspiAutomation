using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nist;


namespace HomeAutomation
{
    class UpdateTimeClass
    {
        private string syncErrorText = null;        // set to update/clear an NIST sync error condition
        private DateTime prevSyncTime = DateTime.MinValue;
        private double prevAdjustment = 0;

        private void Synchronize()
        {
            try
            {
                NistClock nistClock = new NistClock();
                TimeSpan error = nistClock.SynchronizeLocalClock();
                prevAdjustment = error.TotalSeconds;
                syncErrorText = string.Empty;   // clear any prior error text
                prevSyncTime = DateTime.Now;
            }
            catch (Exception)
            {
                syncErrorText = "There was an error trying to synchronize with the NIST timeserver.\nAre you connected to the Internet?";
            }

            
        }
    }
}
