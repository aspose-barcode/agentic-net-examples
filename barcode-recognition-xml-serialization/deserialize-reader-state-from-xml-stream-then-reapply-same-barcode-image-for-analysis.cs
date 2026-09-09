// Title: Deserialize BarCodeReader state from XML and reuse barcode image
// Description: Demonstrates exporting a BarCodeReader configuration to an XML stream, importing it back, and applying the same barcode image for recognition.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showcasing how to serialize and deserialize BarCodeReader settings using XML. It uses key API classes such as BarcodeGenerator, BarCodeReader, and related settings. Typical use cases include persisting reader configurations, sharing settings across applications, and reapplying them to different images for consistent barcode analysis. Developers often need to export reader state for storage or transport and later import it to maintain identical recognition parameters.
// Prompt: Deserialize the reader state from an XML stream, then reapply the same barcode image for analysis.
// Tags: barcode, serialization, xml, reader, generation, recognition, code128, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates exporting a BarCodeReader's configuration to XML, importing it,
/// and reusing the same barcode image for recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "barcode.png");

        // Generate a sample barcode image (Code128) and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Initialize a BarCodeReader, configure it, and export its state to an XML memory stream
        using (var reader = new BarCodeReader())
        {
            // Load the generated barcode image into the reader
            reader.SetBarCodeImage(barcodePath);
            // Restrict decoding to Code128 symbology
            reader.SetBarCodeReadType(DecodeType.Code128);
            // Example setting: ignore FNC characters during decoding
            reader.BarcodeSettings.StripFNC = true;

            // Export the configured reader state to XML
            using (var ms = new MemoryStream())
            {
                reader.ExportToXml(ms);
                ms.Position = 0; // Reset stream position for reading

                // Import the reader state from the XML stream
                using (var importedReader = BarCodeReader.ImportFromXml(ms))
                {
                    // Reapply the same barcode image and read type (these are not stored in XML)
                    importedReader.SetBarCodeImage(barcodePath);
                    importedReader.SetBarCodeReadType(DecodeType.Code128);

                    // Perform barcode recognition using the imported configuration
                    var results = importedReader.ReadBarCodes();
                    Console.WriteLine($"Barcodes read: {results.Length}");
                    foreach (var result in results)
                    {
                        Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                    }
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}