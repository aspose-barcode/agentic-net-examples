// Title: Read Code 11 barcode with checksum validation
// Description: Generates a Code 11 barcode image and then reads it while enforcing mandatory checksum verification.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create a 1‑D barcode, and BarCodeReader with ChecksumValidation.On to validate the checksum during decoding. Developers working with inventory, tracking, or legacy systems often need to ensure data integrity of Code 11 barcodes, making checksum validation a common requirement.
// Prompt: Read a single Code 11 barcode image after enabling obligatory checksum verification with ChecksumValidation.On.
// Tags: code11, barcode, checksum, generation, recognition, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code 11 barcode image and reading it back with checksum validation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a barcode, and reads it while verifying the checksum.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "Code11Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the barcode image
        string imagePath = Path.Combine(tempDir, "code11.png");

        // -------------------------------------------------
        // Generate a Code 11 barcode image and save it as PNG
        // -------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode to the specified file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -------------------------------------------------
        // Read the barcode image with checksum validation turned on
        // -------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code11))
        {
            // Enable mandatory checksum verification
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes (only one expected)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"1D Value: {result.Extended.OneD.Value}");
                Console.WriteLine($"1D CheckSum: {result.Extended.OneD.CheckSum}");
            }
        }
    }
}