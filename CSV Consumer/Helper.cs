using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace CSV_Consumer
{
    class Helper
    {
        //Load files for GUI report
        public static string loadFile(string path)
        {
            var lineNum = File.ReadLines(path).Count() - 1;
            string fileName = path + ": " + lineNum;

            return fileName;
        }

        //Get information for output file
        public static List<dynamic> getData(string path)
        {
            List<string> header = new List<string>();
            List<dynamic> data = new List<dynamic>();
            StreamReader sr = new StreamReader(path);
            string headLine;
            string line;
            dynamic obj = new ExpandoObject();
            IDictionary<string, object> dataDict = (IDictionary<string, object>)obj;
            var lineNum = File.ReadLines(path).Count() - 1;

            //Read the first line (header)
            headLine = sr.ReadLine();
            string[] firstLine = headLine.Split(',');
            foreach(string l in firstLine)
            {
                header.Add(l);
            }

            //Read body data
            while((line = sr.ReadLine()) != null)
            {
                obj = new ExpandoObject();
                //string[] lines = line.Split(',');
                string[] lines = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                for(int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Length == 0)
                        lines[i] = "[BLANK]";

                    ((IDictionary<string, object>)obj).Add(new KeyValuePair<string, object>(header[i], lines[i]));
                }

                data.Add(obj);
            }

            return data;
        }

        //Output information to text file
        public static void saveData(List<dynamic> data, string path)
        {
            StreamWriter sw = null;

            do
            {
                try
                {
                    sw = File.AppendText(path);
                }
                catch(IOException) { }

            } while (sw == null);

            foreach(dynamic d in data)
            {
                IDictionary<string, object> dict = d as IDictionary<string, object>;

                foreach (KeyValuePair<string, object> i in dict)
                    sw.Write(string.Format("{0}:{1}, ", i.Key, i.Value));

                sw.WriteLine();
            }

            sw.Close();
        }
    }
}
