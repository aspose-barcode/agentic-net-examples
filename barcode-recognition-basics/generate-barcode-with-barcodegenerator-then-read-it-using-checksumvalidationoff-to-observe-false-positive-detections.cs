// Title: Generate and Read Code11 Barcode with Checksum Validation Options
// Description: Demonstrates generating a Code11 barcode image using BarcodeGenerator and then reading it twice—once with default checksum validation and once with checksum validation turned off—to illustrate false‑positive detection.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating one‑dimensional barcodes, BarCodeReader for decoding them, and the ChecksumValidation enum to control checksum handling. Typical scenarios include validating barcode data integrity during scanning and troubleshooting checksum‑related issues. Developers often need to toggle checksum validation to compare results or handle legacy barcodes.
// Prompt: Generate a barcode with BarcodeGenerator, then read it using ChecksumValidation.Off to observe false positive detections.
// Tags: code11, barcode, generation, recognition, checksumvalidation, off, default, aspose.barcode, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Code11 barcode, saves it as PNG, and reads it with different checksum validation settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the PNG image
        string imagePath = Path.Combine(tempFolder, "code11.png");

        // ------------------------------------------------------------
        // Generate a Code11 barcode and save it as a PNG file
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            // Save the barcode image to the specified path
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine("Barcode generated at: " + imagePath);
        Console.WriteLine();

        // ------------------------------------------------------------
        // Read the barcode with the default checksum validation setting
        // ------------------------------------------------------------
        Console.WriteLine("Reading with ChecksumValidation.Default:");
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code11))
        {
            // Apply the default checksum validation behavior
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

            // Iterate through all detected barcodes (should be one in this case)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Checksum (OneD): {result.Extended.OneD.CheckSum}");
            }
        }

        Console.WriteLine();

        // ------------------------------------------------------------
        // Read the same barcode with checksum validation turned off
        // This may reveal false‑positive detections where the checksum is ignored
        // ------------------------------------------------------------
        Console.WriteLine("Reading with ChecksumValidation.Off:");
        using (BarCodeReader readerOff = new BarCodeReader(imagePath, DecodeType.Code11))
        {
            // Disable checksum validation
            readerOff.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            foreach (BarCodeResult result in readerOff.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Checksum (OneD): {result.Extended.OneD.CheckSum}");
            }
        }

        // Cleanup (optional): delete the temporary folder and its contents
        // Directory.Delete(tempFolder, true);
    }
}