// Title: Code128 Barcode Generation with Checksum Validation
// Description: Demonstrates generating a Code128 barcode, automatically calculating its checksum, and then reading it back with checksum validation enabled.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with BarcodeSettings.ChecksumValidation to verify the integrity of Code128 symbols. Developers commonly need to ensure data accuracy when scanning barcodes in inventory, shipping, or point‑of‑sale systems, and this pattern illustrates the typical workflow for checksum verification.
// Prompt: Enable checksum validation for Code128 barcodes and report any verification failures during processing.
// Tags: code128, checksum, validation, barcode generation, barcode recognition, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it as PNG, and reading it back with checksum validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, validates its checksum during reading, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "Code128ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "code128.png");

        // Generate Code128 barcode (checksum is obligatory and automatically calculated)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read barcode with checksum validation enabled
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Turn on checksum verification for Code128
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool anyResult = false;
            foreach (var result in reader.ReadBarCodes())
            {
                anyResult = true;
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Checksum (if available): {result.Extended.OneD.CheckSum}");
            }

            // Inform the user if no barcode was detected or validation failed
            if (!anyResult)
            {
                Console.WriteLine("No barcode detected or checksum validation failed.");
            }
        }

        // Clean up temporary files
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}