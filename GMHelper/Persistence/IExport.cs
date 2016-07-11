using System.Collections.Generic;
using GM.Model;

namespace GM.Persistence
{
    interface IExport
    {
        void Export (List<Skater> players, string path);
    }
}
