using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExportExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
            {
                // Set some visual parameters
                generator.Parameters.Barcode.BarColor = Color.Blue;
                generator.Parameters.BackColor = Color.Yellow;

                // Serialize settings to a memory stream
                using (MemoryStream xmlStream = new MemoryStream())
                {
                    bool exported = generator.ExportToXml(xmlStream);
                    Console.WriteLine("Export to XML succeeded: " + exported);

                    // Reset stream position to read the XML content
                    xmlStream.Position = 0;
                    using (StreamReader sr = new StreamReader(xmlStream, Encoding.UTF8, true, 1024, leaveOpen: true))
                    {
                        string xmlContent = sr.ReadToEnd();
                        Console.WriteLine("Exported XML:");
                        Console.WriteLine(xmlContent);
                    }
                }
            }
        }
    }
}