// Title: Read barcodes from a JPEG image using BarCodeReader
// Description: Demonstrates how to generate a Code128 barcode, save it as a JPEG, and then read it back using Aspose.BarCode's BarCodeReader to obtain detection details.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader, DecodeType, and BarCodeResult classes. Typical scenarios include scanning images, extracting barcode data, and analyzing detection quality. Developers often need to read barcodes from various image formats and retrieve region information for further processing.
// Prompt: Read barcodes from a JPEG file using BarCodeReader constructor and retrieve detection results.
// Tags: barcode, read, jpeg, barcodereader, detection, aspose.barcode, code128, decode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, saving to JPEG, and reading detection results using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode image, reads it, and outputs detection information.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.jpg");

        // Generate a simple Code128 barcode and save as JPEG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Jpeg);
        }

        // Verify the file exists before attempting to read
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Barcode image not found: {barcodePath}");
            return;
        }

        // Read barcodes from the JPEG file using BarCodeReader
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Optional: set quality settings if desired
            // reader.QualitySettings = QualitySettings.HighPerformance;

            // Perform the detection
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Output details for each detected barcode
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}");
                    Console.WriteLine($"Text: {result.CodeText}");
                    Console.WriteLine($"Quality: {result.ReadingQuality}");

                    // Retrieve region bounds
                    var bounds = result.Region.Rectangle;
                    Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}");
                    Console.WriteLine($"Angle: {result.Region.Angle}");
                    Console.WriteLine(new string('-', 40));
                }
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}