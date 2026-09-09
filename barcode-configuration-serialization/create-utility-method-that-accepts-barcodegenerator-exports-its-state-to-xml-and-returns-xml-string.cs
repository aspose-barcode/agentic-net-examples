// Title: Export BarcodeGenerator State to XML
// Description: Demonstrates how to export the configuration of an Aspose.BarCode BarcodeGenerator to an XML string for persistence or debugging.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It shows how to use the BarcodeGenerator class together with its ExportToXml method to capture the generator's current state. Typical use cases include saving barcode settings, transferring configurations between services, or troubleshooting. Developers working with barcode creation often need to serialize generator parameters, and this snippet illustrates the standard approach.
// Prompt: Create a utility method that accepts a BarcodeGenerator, exports its state to XML, and returns the XML string.
// Tags: barcode symbology, export, xml, aspose.barcode, generation, utility

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a sample demonstrating how to export a BarcodeGenerator's state to an XML string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a BarcodeGenerator, optionally modifies its parameters,
    /// exports its state to XML, and writes the result to the console.
    /// </summary>
    static void Main()
    {
        // Create a BarcodeGenerator for a QR code with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Optionally adjust a parameter (e.g., X-dimension in pixels).
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Export the generator's current configuration to an XML string.
            string xml = ExportGeneratorStateToXml(generator);

            // Output the exported XML to the console.
            Console.WriteLine("Exported XML:");
            Console.WriteLine(xml);
        }
    }

    /// <summary>
    /// Exports the provided <see cref="BarcodeGenerator"/> state to an XML string.
    /// </summary>
    /// <param name="generator">The BarcodeGenerator instance whose state will be serialized.</param>
    /// <returns>An XML string representing the generator's configuration.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="generator"/> is null.</exception>
    static string ExportGeneratorStateToXml(BarcodeGenerator generator)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));

        // Use a memory stream to capture the XML output from ExportToXml.
        using (var memoryStream = new MemoryStream())
        {
            generator.ExportToXml(memoryStream);
            memoryStream.Position = 0; // Reset stream position for reading.

            // Read the entire XML content from the memory stream.
            using (var reader = new StreamReader(memoryStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}