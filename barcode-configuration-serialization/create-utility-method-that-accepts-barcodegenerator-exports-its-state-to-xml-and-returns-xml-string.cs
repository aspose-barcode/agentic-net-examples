// Title: Export BarcodeGenerator State to XML
// Description: Demonstrates how to export the configuration of a BarcodeGenerator to an XML string using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode configuration export category. It shows how to use the BarcodeGenerator class together with its Parameters property and the ExportToXml method to serialize the generator’s state. Developers often need to persist barcode settings for later reuse, debugging, or sharing across services, making XML export a common task in barcode automation workflows.
// Prompt: Create a utility method that accepts a BarcodeGenerator, exports its state to XML, and returns the XML string.
// Tags: barcode symbology, export, xml, configuration, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample program that creates a barcode generator, configures it, and exports its state to an XML string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Demonstrates barcode generation and XML export.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator with Code128 symbology and sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Configure barcode appearance and behavior.
            generator.Parameters.Barcode.XDimension.Point = 2f;                     // Width of the smallest bar.
            generator.Parameters.Barcode.BarHeight.Point = 40f;                    // Height of the barcode.
            generator.Parameters.Barcode.FilledBars = false;                       // Use unfilled bars.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false; // Suppress validation exceptions.

            // Export the generator's current configuration to an XML string.
            string xml = ExportGeneratorToXml(generator);
            Console.WriteLine(xml);
        }
    }

    /// <summary>
    /// Exports the provided <see cref="BarcodeGenerator"/> instance to an XML string.
    /// </summary>
    /// <param name="generator">The barcode generator whose state will be serialized.</param>
    /// <returns>XML representation of the generator's configuration.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the export operation fails.</exception>
    static string ExportGeneratorToXml(BarcodeGenerator generator)
    {
        // Use a memory stream to capture the XML output.
        using (var memoryStream = new MemoryStream())
        {
            // Perform the export; the method returns true on success.
            bool exported = generator.ExportToXml(memoryStream);
            if (!exported)
                throw new InvalidOperationException("Failed to export barcode generator to XML.");

            // Reset stream position to the beginning before reading.
            memoryStream.Position = 0;
            using (var reader = new StreamReader(memoryStream))
            {
                // Read the entire XML content and return it.
                return reader.ReadToEnd();
            }
        }
    }
}