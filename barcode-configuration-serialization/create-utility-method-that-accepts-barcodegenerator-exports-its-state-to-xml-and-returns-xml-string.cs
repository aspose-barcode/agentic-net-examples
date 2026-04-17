using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExportExample
{
    class Program
    {
        static void Main()
        {
            // Create a sample barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
            {
                // Set a few parameters (optional)
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Export generator state to XML string
                string xml = ExportGeneratorToXml(generator);

                // Output the XML
                Console.WriteLine(xml);
            }
        }

        /// <summary>
        /// Exports the state of a BarcodeGenerator to an XML string.
        /// </summary>
        /// <param name="generator">The BarcodeGenerator instance.</param>
        /// <returns>XML representation of the generator's settings.</returns>
        static string ExportGeneratorToXml(BarcodeGenerator generator)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));

            // Use a memory stream to capture the XML output
            using (var memoryStream = new MemoryStream())
            {
                // Export to the stream; returns true on success
                bool success = generator.ExportToXml(memoryStream);
                if (!success)
                    throw new InvalidOperationException("Failed to export barcode settings to XML.");

                // Reset stream position to read from the beginning
                memoryStream.Position = 0;

                // Read the XML content as a string
                using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}