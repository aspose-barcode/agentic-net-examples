using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a <see cref="BarcodeGenerator"/> configuration to an XML string.
/// </summary>
class Program
{
    /// <summary>
    /// Exports the state of a <see cref="BarcodeGenerator"/> to an XML string.
    /// </summary>
    /// <param name="generator">The barcode generator whose configuration will be exported.</param>
    /// <returns>An XML representation of the generator's parameters.</returns>
    static string ExportGeneratorToXml(BarcodeGenerator generator)
    {
        // Use a memory stream to hold the XML data.
        using (var memoryStream = new MemoryStream())
        {
            // Export the generator's parameters to the stream.
            generator.ExportToXml(memoryStream);
            // Reset the stream position to the beginning for reading.
            memoryStream.Position = 0;
            // Read the entire XML content from the stream.
            using (var reader = new StreamReader(memoryStream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    /// <summary>
    /// Entry point of the application. Creates a barcode generator, configures it,
    /// exports its state to XML, and writes the XML to the console.
    /// </summary>
    static void Main()
    {
        // Create a sample barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Configure a few visual parameters.
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Export the generator's state to XML.
            string xml = ExportGeneratorToXml(generator);

            // Output the exported XML to the console.
            Console.WriteLine("Exported XML:");
            Console.WriteLine(xml);
        }
    }
}