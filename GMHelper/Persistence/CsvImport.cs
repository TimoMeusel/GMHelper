using System.Collections.Generic;
using System.IO;
using System.Linq;
using GM.Model;

namespace GM.Persistence
{
    public class CsvImport
    {
        public IEnumerable<Skater> Load (string path)
        {
            List<Skater> skaters = new List<Skater>();

            List<string> lines = File.ReadLines(path).ToList();
            string header = lines.First();
            lines.RemoveAt(0);

            var headers = header.Split('\t').ToList();
            var team = headers.Last();
            
            foreach (var line in lines)
            {
                var values = line.Split('\t');
                var dict = new Dictionary<string, string>();
                for (int i = 0; i < headers.Count; i++)
                {
                    dict.Add(headers[i], values[i]);
                }

                var skater = new Skater(new Team(dict[team]), dict);
                skaters.Add(skater);
            }

            return skaters;
        }
    }
}
