// Title: Export and Import BarCodeReader State via XML MemoryStream
// Description: Demonstrates saving a BarCodeReader's configuration to a memory stream in XML format and later restoring it to read barcodes.
// Category-Description: This example belongs to the Aspose.BarCode state management category, illustrating how to serialize and deserialize a BarCodeReader using ExportToXml and ImportFromXml. It covers barcode generation, recognition, and configuration persistence with classes such as BarcodeGenerator, BarCodeReader, and related settings. Developers often need to store reader settings for later reuse or transfer across application domains, and this snippet shows a typical workflow for QR code handling.
// Prompt: Save the reader state to a memory stream in XML format for later deserialization.
// Tags: barcode symbology, export, import, xml, memory stream, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates exporting a BarCodeReader's state to an XML memory stream and importing it back for barcode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR barcode, exports the reader state to XML, imports it, and reads the barcode.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder and file path for the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // --------------------------------------------------------------------
        // Generate a sample QR barcode and save it as a PNG file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was created successfully.
        // --------------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Create a BarCodeReader, configure its settings, and export its state to a MemoryStream in XML format.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader())
        {
            reader.SetBarCodeReadType(DecodeType.QR);
            reader.SetBarCodeImage(imagePath);
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings.XDimension = XDimensionMode.Small;

            using (var memoryStream = new MemoryStream())
            {
                // Serialize the reader's configuration to XML inside the memory stream.
                reader.ExportToXml(memoryStream);
                Console.WriteLine($"Reader state exported to memory stream (length: {memoryStream.Length} bytes).");

                // Reset the stream position to the beginning before importing.
                memoryStream.Position = 0;

                // ----------------------------------------------------------------
                // Import the reader state from the XML memory stream and reassign image and decode type.
                // ----------------------------------------------------------------
                using (var importedReader = BarCodeReader.ImportFromXml(memoryStream))
                {
                    // The image path and decode type are not stored in the XML, so they must be set again.
                    importedReader.SetBarCodeImage(imagePath);
                    importedReader.SetBarCodeReadType(DecodeType.QR);

                    // Perform barcode reading using the imported configuration.
                    var results = importedReader.ReadBarCodes();
                    Console.WriteLine($"Barcodes read after import: {results.Length}");
                    foreach (var result in results)
                    {
                        Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directories.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome.
        }
    }
}