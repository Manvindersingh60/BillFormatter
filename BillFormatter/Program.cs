using System;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace BillFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            PdfReader reader=new PdfReader(@"E:\asd.pdf");

            ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
            string currentText = PdfTextExtractor.GetTextFromPage(reader, 1, strategy);

            string text = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
            int x=text.IndexOf("Bill Details");
            int y = text.IndexOf("2016");
            string z = text.Substring(x+13, y-x-15);
            z = z.Replace(" ", "_");
            if (text.Contains(("AM"))) ;
            {
                z = z + "_Morning";
            }
            Console.Write(text);
            //int x = reader.NumberOfPages;
            Console.Read();
        }
    }
}
