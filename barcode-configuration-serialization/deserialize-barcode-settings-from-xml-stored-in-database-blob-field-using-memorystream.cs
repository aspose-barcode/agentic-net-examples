// Title: Deserialize barcode settings from XML stored in a database BLOB using MemoryStream
// Description: Demonstrates how to export barcode generator settings to XML, store them in a memory stream (simulating a DB BLOB), and then import the settings to generate a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode settings serialization category. It shows how to use BarcodeGenerator.ExportToXml and BarcodeGenerator.ImportFromXml to persist and restore barcode configuration. Typical use cases include saving barcode settings in a database, transferring configurations between services, or version‑controlling barcode parameters. Developers working with barcode generation often need to serialize settings for reuse, and these APIs provide a straightforward XML‑based approach.
// Prompt: Deserialize barcode settings from XML stored in a database BLOB field using a MemoryStream.
// Tags: barcode, serialization, xml, memorystream, aspnet, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that serializes barcode generator settings to XML,
/// simulates storing them in a database BLOB using a MemoryStream,
/// and then deserializes the settings to create a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode,
    /// exports its settings to XML, imports them back, and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        string codeText = "1234567890";

        // Create a barcode generator for Code128 with the specified text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Adjust the X-dimension (module width) of the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Use a MemoryStream to simulate a BLOB field in a database.
            using (MemoryStream ms = new MemoryStream())
            {
                // Serialize the generator's settings to XML and write them into the stream.
                generator.ExportToXml(ms);
                // Reset the stream position to the beginning for reading.
                ms.Position = 0;

                // Deserialize the settings from the XML stored in the MemoryStream.
                using (BarcodeGenerator loadedGenerator = BarcodeGenerator.ImportFromXml(ms))
                {
                    // Determine a temporary file path for the output PNG image.
                    string outputPath = Path.Combine(Path.GetTempPath(), "barcode_from_xml.png");
                    // Save the barcode image using the deserialized settings.
                    loadedGenerator.Save(outputPath, BarCodeImageFormat.Png);
                    // Inform the user where the image was saved.
                    Console.WriteLine("Barcode saved to: " + outputPath);
                }
            }
        }
    }
}