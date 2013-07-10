using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DANikitinTZ.Helpers
{
    public class CSVParser
    {
        public List<string[]> ParseFile(Stream stream)
        {
            List<string[]> parsedData = new List<string[]>();

            try
            {
                using (StreamReader readFile = new StreamReader(stream))
                {
                    string line;
                    string[] row;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        row = line.Split(';');
                        parsedData.Add(row);
                    }
                }
            }
            catch (Exception)
            {

            }
            return parsedData;
        }
    }
}