// Title: Save BarCodeReader state to XML in memory stream
// Description: Demonstrates how to export a BarCodeReader's configuration to an XML memory stream and later import it to read a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode reading and configuration category. It shows how to use BarCodeReader, BarcodeSettings, QualitySettings, and the ExportToXml/ImportFromXml methods to persist reader state. Typical use cases include saving reader configurations for later reuse, sharing settings across services, or caching. Developers often need to serialize reader settings to XML or JSON for deployment or testing scenarios.
// Prompt: Save the reader state to a memory stream in XML format for later deserialization.
// Tags: barcode, reader, xml, serialization, deserialization, memorystream, aspose.barcode, qrcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code, serializes a BarCodeReader's state to XML,
/// deserializes it, and reads the barcode from the generated image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, state export/import,
    /// and cleanup logic.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for demo files
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // --------------------------------------------------------------------
        // Generate a simple QR barcode and save it to a PNG file
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Export the BarCodeReader configuration to an XML memory stream
        // --------------------------------------------------------------------
        using (var ms = new MemoryStream())
        {
            // Create a BarCodeReader, configure its settings, and export to XML
            using (var reader = new BarCodeReader())
            {
                reader.SetBarCodeReadType(DecodeType.QR);
                reader.BarcodeSettings.StripFNC = true;
                reader.QualitySettings.XDimension = XDimensionMode.Small;
                reader.ExportToXml(ms);
            }

            // Reset the stream position so it can be read from the beginning
            ms.Position = 0;

            // ----------------------------------------------------------------
            // Import the reader state from the XML stream and perform barcode reading
            // ----------------------------------------------------------------
            using (var importedReader = BarCodeReader.ImportFromXml(ms))
            {
                importedReader.SetBarCodeImage(imagePath);
                importedReader.SetBarCodeReadType(DecodeType.QR);
                var results = importedReader.ReadBarCodes();

                Console.WriteLine($"Barcodes read: {results.Length}");
                foreach (var result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directories
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}