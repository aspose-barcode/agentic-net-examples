// Title: Export BarcodeGenerator State to XML
// Description: Demonstrates how to export the configuration of an Aspose.BarCode BarcodeGenerator to an XML string for persistence or inspection.
// Prompt: Create a utility method that accepts a BarcodeGenerator, exports its state to XML, and returns the XML string.
// Tags: barcode symbology, export, xml, aspose.barcode, utility

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a barcode, modifies its appearance,
/// and exports the generator's state to an XML string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the barcode color to blue (demonstrates property modification)
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Export the current generator configuration to an XML string
            string xml = ExportGeneratorToXml(generator);

            // Write the resulting XML to the console
            Console.WriteLine(xml);
        }
    }

    /// <summary>
    /// Exports the state of the provided <see cref="BarcodeGenerator"/> to an XML string.
    /// </summary>
    /// <param name="generator">The barcode generator whose configuration will be exported.</param>
    /// <returns>An XML string representing the generator's current state.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the export operation fails.</exception>
    static string ExportGeneratorToXml(BarcodeGenerator generator)
    {
        // Use a memory stream as the destination for the XML data
        using (var memoryStream = new MemoryStream())
        {
            // Perform the export; the method returns true on success
            bool exported = generator.ExportToXml(memoryStream);
            if (!exported)
                throw new InvalidOperationException("Failed to export barcode generator to XML.");

            // Reset the stream position to the beginning before reading
            memoryStream.Position = 0;

            // Read the entire XML content from the memory stream
            using (var reader = new StreamReader(memoryStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}