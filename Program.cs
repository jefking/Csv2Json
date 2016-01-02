using System;
using System.IO;
using System.Text;

namespace Delimited
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\jefkin\OneDrive\Kevin\al-data.{0}";
            var data = File.ReadAllText(string.Format(path, "csv"), Encoding.UTF8);
            var datas = data.Split('|');
            var output = new StringBuilder();
            output.AppendFormat("[{0}", Environment.NewLine);

            for (var i = 0; i <= datas.Length - 3; i+=3)
            {
                output.Append("\t{\r\n");
                for (var ii = 0; ii < 3; ii++)
                {
                    var name = ii == 0 ? "Haida" : ii == 1 ? "Notes" : "Definition";
                    var eol = ii == 2 ? string.Empty : ",";
                    var d = datas[i + ii];
                    if (!string.IsNullOrWhiteSpace(d))
                    {
                        d = d.Replace(Environment.NewLine, " - ").Replace('"'.ToString(), "\\\"").Trim(' ');
                        while (d.Contains("\t"))
                        {
                            d = d.Replace("\t", " ");
                        }
                        while (d.Contains("  "))
                        {
                            d = d.Replace("  ", " ");
                        }
                        output.AppendFormat("\t\t\"{0}\": \"{1}\"{2}{3}", name, d, eol, Environment.NewLine);
                    }
                }
                output.Append("\t}");
                if (i != datas.Length - 3)
                {
                    output.Append(',');
                }
                output.Append("\r\n");
            }

            output.AppendFormat("]{0}", Environment.NewLine);
            File.WriteAllText(string.Format(path, "js"), output.ToString(), Encoding.UTF8);
        }
    }
}