using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakesTestClassLibrary
{
    public class UpgradeService : IUpgradeService
    {

        public int CurrentSWVersion(int x)
        {
            return 1;
        }

        public bool IsSWUpgradeRequired(int DeviceID)
        {
            return false;
        }

        public DateTime LastUpgradeDate(int DeviceID)
        {
            return DateTime.Now;
        }

        public bool UpgradeDevice(int DeviceID)
        {
            return false;
        }
    }
}
