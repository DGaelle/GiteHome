using GiteHouse.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiteHouse.Core
{
    interface IUnitOfWork:IDisposable
    {
        IRegionRepository Regions { get; }
        int Complete();
    }
}
