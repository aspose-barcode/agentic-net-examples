// Title: Read Code 11 barcode with checksum validation
// Description: Demonstrates generating a Code 11 barcode image, saving it as PNG, and reading it back while enforcing mandatory checksum verification.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create 1‑D barcodes and BarCodeReader to decode them, highlighting the ChecksumValidation setting for reliable data integrity checks. Developers working with inventory, tracking, or legacy systems often need to generate and validate Code 11 barcodes, making this pattern a common requirement in barcode‑centric applications.
// Prompt: Read a single Code 11 barcode image after enabling obligatory checksum verification with ChecksumValidation.On.
// Tags: code11, barcode, checksum, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Code 11 barcode, saves it to a temporary PNG file,
/// and then reads the barcode back with checksum validation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it with checksum verification,
    /// and outputs the decoded information to the console.
    /// </summary>
    static void Main(string[] args)
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "Code11Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "code11.png");

        // ------------------------------------------------------------
        // Generate a Code 11 barcode image and save it as PNG
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image not found.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode with checksum validation set to On
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code11))
        {
            // Enforce mandatory checksum verification
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Attempt to read all barcodes from the image
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected or checksum validation failed.");
            }
            else
            {
                // Output details of each detected barcode
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"1D Value: {result.Extended.OneD.Value}");
                    Console.WriteLine($"1D CheckSum: {result.Extended.OneD.CheckSum}");
                }
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directory
        // ------------------------------------------------------------
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}