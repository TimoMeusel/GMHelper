using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GM.Model;

namespace GM.Persistence
{
    public class CsvExport : IExport
    {
        public void Export (List<Skater> players, string path)
        {
            StringBuilder builder = new StringBuilder();

            List<string> headers = players.First().Values.Keys.ToList();
            headers.Add("Team");

            var header = string.Join("\t", headers);
            builder.AppendLine(header);
            
            foreach (var player in players)
            {
                var line = string.Join("\t", player.Values.Values.Concat(new[] { player.Team.Name }));
                builder.AppendLine(line);
            }

            File.WriteAllText(path, builder.ToString());
        }
    }
}
