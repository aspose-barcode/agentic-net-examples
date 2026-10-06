// Title: Read JPEG barcode with HighQuality preset using Aspose.BarCode
// Description: Demonstrates generating a Code128 barcode, saving it as a JPEG, and reading it back with the HighQuality quality preset for balanced speed and accuracy.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader to decode them, and QualitySettings to control decoding performance. Typical scenarios include scanning barcodes from image files in desktop or web applications where developers need a reliable trade‑off between speed and precision.
// Prompt: Create a BarCodeReader instance that reads JPEG images and applies HighQuality preset for balanced speed.
// Tags: barcode, code128, jpeg, highquality, qualitysettings, barcodereader, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a Code128 barcode, saves it as a JPEG,
/// and reads it back using the HighQuality preset for balanced speed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "sample.jpg");

        // Generate a Code128 barcode and save it as a JPEG file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Jpeg);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the JPEG image using the HighQuality preset
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Apply the HighQuality setting for a balanced speed/accuracy trade‑off
            reader.QualitySettings = QualitySettings.HighQuality;

            // Decode all barcodes found in the image
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");

            // Output each decoded barcode's type and text
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical for this demo
        }
    }
}