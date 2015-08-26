using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakesTestClassLibrary
{
    public interface IUpgradeService
    {
        int CurrentSWVersion(int x);

        bool IsSWUpgradeRequired(int DeviceID);

        DateTime LastUpgradeDate(int DeviceID);

        bool UpgradeDevice(int DeviceID);
    }
}
