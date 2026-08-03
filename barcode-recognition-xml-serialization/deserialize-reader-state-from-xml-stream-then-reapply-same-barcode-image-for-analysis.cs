// Title: Deserialize BarCodeReader state from XML and reuse the same image
// Description: Demonstrates exporting a BarCodeReader's state to an XML stream, importing it back, and reapplying the original barcode image for further analysis.
// Category-Description: This example belongs to the Aspose.BarCode serialization and deserialization category. It showcases the use of BarCodeReader.ExportToXml, BarCodeReader.ImportFromXml, and related classes such as BarcodeGenerator. Developers often need to persist reader configurations, share them across services, or reload them for repeated scans without reconfiguring the reader each time.
// Prompt: Deserialize the reader state from an XML stream, then reapply the same barcode image for analysis.
// Tags: code128, serialization, png, barcodereader, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, exports the reader state to XML,
/// imports it back, and reuses the same image for barcode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, state serialization,
    /// deserialization, and recognition without requiring interactive console input.
    /// </summary>
    static void Main()
    {
        // Define the barcode text to encode.
        const string codeText = "1234567890";

        // Path for the temporary PNG image that will hold the generated barcode.
        const string imagePath = "temp_barcode.png";

        // Generate a Code128 barcode and save it as a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Load the generated PNG image into a bitmap for processing.
        using (var bitmap = new Bitmap(imagePath))
        {
            // Initialize a BarCodeReader and configure it to decode Code128 symbology.
            using (var reader = new BarCodeReader())
            {
                reader.SetBarCodeReadType(DecodeType.Code128);
                reader.SetBarCodeImage(bitmap);

                // Export the current reader configuration and state to an in‑memory XML stream.
                using (var xmlStream = new MemoryStream())
                {
                    reader.ExportToXml(xmlStream);
                    xmlStream.Position = 0; // Reset stream position for subsequent reading.

                    // Import the previously saved state into a new BarCodeReader instance.
                    var importedReader = BarCodeReader.ImportFromXml(xmlStream);

                    // Reassign the same bitmap image to the imported reader for analysis.
                    importedReader.SetBarCodeImage(bitmap);

                    // Execute barcode recognition and output results to the console.
                    foreach (var result in importedReader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Detected Text: {result.CodeText}");
                    }
                }
            }
        }

        // Attempt to delete the temporary image file; ignore any errors that occur.
        if (File.Exists(imagePath))
        {
            try
            {
                File.Delete(imagePath);
            }
            catch
            {
                // Suppress cleanup exceptions.
            }
        }
    }
}