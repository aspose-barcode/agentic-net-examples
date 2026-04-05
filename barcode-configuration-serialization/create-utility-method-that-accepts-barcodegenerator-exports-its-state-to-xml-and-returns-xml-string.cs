using System;
using System.IO;
using Aspose.BarCode.Generation;

namespace BarcodeXmlExportDemo
{
    class Program
    {
        // Utility method that exports a BarcodeGenerator's state to an XML string.
        static string ExportGeneratorToXmlString(BarcodeGenerator generator)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));

            // Export to a memory stream, then read the XML content as a string.
            using (var memoryStream = new MemoryStream())
            {
                bool success = generator.ExportToXml(memoryStream);
                if (!success)
                    throw new InvalidOperationException("Export to XML failed.");

                memoryStream.Position = 0; // Reset stream position for reading.

                using (var reader = new StreamReader(memoryStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        static void Main(string[] args)
        {
            // Create a simple barcode generator.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "123ABC";

                // Export its configuration to XML string.
                string xml = ExportGeneratorToXmlString(generator);

                // Output the XML to console.
                Console.WriteLine("Exported BarcodeGenerator XML:");
                Console.WriteLine(xml);
            }
        }
    }
}