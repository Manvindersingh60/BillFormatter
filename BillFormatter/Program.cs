using System;
using System.IO;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Configuration;

namespace BillFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["Source"]);

            foreach (var file in files)
            {
                FormatFile(file);
                Console.WriteLine(file);
            }
            Console.WriteLine("Done");
            Console.Read();
        }

        public static  void FormatFile(string fileName)
        {

            PdfReader reader = new PdfReader(fileName);
            ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
            string currentText = PdfTextExtractor.GetTextFromPage(reader, 1, strategy);

            string text = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
            //Console.Write(text);
            //Console.Read();
            //int x=text.IndexOf("Bill Details");
            int yearIndex = text.LastIndexOf("2016", StringComparison.Ordinal);
            if (yearIndex < 0)
            {
                yearIndex = text.LastIndexOf("2017", StringComparison.Ordinal);
            }
            string date = text.Substring(yearIndex - 8, 6);
            date = date.Replace(" ", "_");
            if (text.Contains(("AM")))
            {
                date = date + "_Morning";

            }
            else
            {
                int timeIndex = text.IndexOf("PM");
                string rideStartHour = text.Substring(timeIndex - 6, 2);
                rideStartHour=rideStartHour.Trim();
                int time = Convert.ToInt16(rideStartHour);
                if (time < 2||time==12)
                {
                    date = date + "_Morning";
                }

                else
                {
                    date = date + "_Evening";
                }
            }
            reader.Dispose();
            string newFile = @"E:\bills\march-april\" + date + ".pdf";
            if (!File.Exists(newFile))
            {
                File.Copy(fileName, newFile); 
            }
            else
            {
                Console.WriteLine(fileName+"   "+newFile);
                File.Copy(fileName, @"E:\bills\dublicate_march-april\" + date + ".pdf");
            }
            //File.Move(fileName, @"E:\"+date+".pdf");
        }
    }
}
