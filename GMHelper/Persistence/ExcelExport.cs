﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GM.Model;
using Microsoft.Office.Interop.Excel;

namespace GM.Persistence
{
    public class ExcelExport: IExport
    {
        public void Export (List<Skater> players, string path)
        {
            Application myExcelApplication = null;

            try
            {
                myExcelApplication = new  Application();

                var myExcelWorkbook = myExcelApplication.Workbooks.Add(System.Reflection.Missing.Value);
                var myExcelWorkSheet = (Worksheet ) myExcelWorkbook.ActiveSheet;

                List<string> headers = players.First().Values.Keys.ToList();
                for (var h = 1; h<=headers.Count; h++)
                {
                    myExcelWorkSheet.Cells[1, h] = headers[h-1];
                }

                Parallel.For(0,
                             players.Count,
                             p =>
                             {
                                 Parallel.For(0,
                                              players[p].Values.Count,
                                              v =>
                                              {
                                                  myExcelWorkSheet.Cells[p + 2, v + 1] = players[p].Values[headers[v]];
                                              });
                             });

                myExcelWorkbook.Close(true, path, System.Reflection.Missing.Value);

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