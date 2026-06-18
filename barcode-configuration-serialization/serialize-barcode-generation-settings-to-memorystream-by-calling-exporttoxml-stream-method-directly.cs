using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates exporting barcode generator settings to XML using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Creates a Code128 barcode generator, enables checksum, and exports its settings to XML.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "12345"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Enable checksum (required for Code128)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Export the generator's configuration to a memory stream in XML format
            using (var memoryStream = new MemoryStream())
            {
                generator.ExportToXml(memoryStream);

                // Rewind the stream to the beginning so it can be read
                memoryStream.Position = 0;

                // Read the XML content from the memory stream
                using (var reader = new StreamReader(memoryStream))
                {
                    string xml = reader.ReadToEnd();

                    // Output the exported XML to the console
                    Console.WriteLine("Exported Barcode Settings XML:");
                    Console.WriteLine(xml);
                }
            }
        }
    }
}