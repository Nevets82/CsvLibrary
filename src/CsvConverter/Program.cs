using Nevets.IO.Csv;
using System.IO;

namespace CsvConverter
{
    public class Program
    {

        static void Main(string[] args)
        {
            var program = new Program(args);

            program.Execute();
        }


            
        private string[] _args;



        private void Execute()
        {
            var inputFile = this.GetInputFile();
            var outputFile = this.GetOutputFile();

            this.ConvertToHtml(inputFile, outputFile);
        }

        private string GetInputFile()
        {
            return this._args[0];
        }

        private string GetOutputFile()
        {
            return this._args[0] + ".html";
        }





        private void ConvertToHtml(string inputFile, string outputFile)
        {
            using (var textWriter = new StreamWriter(outputFile))
            {
                this.WriteHtmlHeader(textWriter, Path.GetFileNameWithoutExtension(inputFile));

                using (var csvReader = new CsvReader(inputFile, new CsvOptions { HasHeader = true, Seperator = ';' }))
                {
                    textWriter.WriteLine("\t\t<table>");

                    if (csvReader.Options.HasHeader)
                    {
                        this.WriteTableHeader(textWriter, csvReader.Header);
                    }

                    this.WriteTableBody(textWriter, csvReader);

                    textWriter.WriteLine("\t\t</table>");
                }

                this.WriteHtmlFooter(textWriter);
            }
        }

        private void WriteTableHeader(StreamWriter textWriter, string[] header)
        {
            textWriter.WriteLine("\t\t\t<thead>");
            this.WriteTableRow(textWriter, header, "th");
            textWriter.WriteLine("\t\t\t</thead>");
        }

        private void WriteTableBody(StreamWriter textWriter, CsvReader csvReader)
        {
            textWriter.WriteLine("\t\t\t<tbody>");

            var record = csvReader.ReadRecord();

            while (record != null)
            {
                this.WriteTableRow(textWriter, record, "td");

                record = csvReader.ReadRecord();
            }

            textWriter.WriteLine("\t\t\t</tbody>");
        }

        private void WriteTableRow(StreamWriter textWriter, string[] record, string elementName)
        {
            textWriter.WriteLine("\t\t\t\t<tr>");

            foreach (var value in record)
            {
                textWriter.WriteLine("\t\t\t\t\t<{0}>{1}</{0}>", elementName, value.Replace("\r\n", "<br />"));
            }

            textWriter.WriteLine("\t\t\t\t</tr>");
        }

        private void WriteHtmlHeader(TextWriter textWriter, string title)
        {
            textWriter.WriteLine("<!DOCTYPE HTML>");
            textWriter.WriteLine("<html>");
            textWriter.WriteLine("\t<head>");
            textWriter.WriteLine("\t\t<title>{0}</title>", title);
            textWriter.WriteLine("\t</head>");
            textWriter.WriteLine("\t<body>");
            textWriter.WriteLine("\t\t<h1>{0}</h1>", title);
        }

        private void WriteHtmlFooter(TextWriter textWriter)
        {
            textWriter.WriteLine("\t</body>");
            textWriter.WriteLine("</html>");
        }



        public Program(string[] args)
        {
            // Initialize field members.
            this._args = args;
        }
    }
}
