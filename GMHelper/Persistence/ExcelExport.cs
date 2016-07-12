using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GM.Model;
using Microsoft.Office.Interop.Excel;

namespace GM.Persistence
{
    public class ExcelExport
    {
        public void Export (List<Skater> players, string path)
        {
            Application myExcelApplication = null;

            try
            {
                myExcelApplication = new  Application();

                var myExcelWorkbook = myExcelApplication.Workbooks.Add(Missing.Value);
                var myExcelWorkSheet = (Worksheet ) myExcelWorkbook.ActiveSheet;

                List<string> headers = players.First().Values.Keys.ToList();
                for (var h = 1; h<=headers.Count; h++)
                {
                    myExcelWorkSheet.Cells[1, h] = headers[h-1];
                }

                for (int p = 0; p < players.Count; p++)
                {
                    for (int v = 0; v < players[p].Values.Count; v++)
                    {
                        myExcelWorkSheet.Cells[p + 2, v + 1] = players[p].Values[headers[v]];
                    }
                }

                myExcelWorkSheet.Cells[1, players.Count + 1] = "Team";
                for (var p = 0; p < players.Count; p++)
                {
                    myExcelWorkSheet.Cells[p+2, headers.Count+1] = players[p].Team.Name;
                }

                myExcelWorkbook.Close(true, path, Missing.Value);

            }
            finally
            {
                // Excel beenden 
                if ( myExcelApplication != null )
                {
                    myExcelApplication.Quit();
                }

            }
        }
         
    }
}