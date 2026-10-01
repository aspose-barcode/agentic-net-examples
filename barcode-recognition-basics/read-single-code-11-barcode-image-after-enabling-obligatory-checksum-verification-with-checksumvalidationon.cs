// Title: Read Code 11 barcode with checksum validation
// Description: Generates a Code 11 barcode image, then reads it while enforcing mandatory checksum verification.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create a barcode, BarCodeReader to decode it, and how to enable obligatory checksum validation via BarcodeSettings.ChecksumValidation. Typical use cases include validating Code 11 barcodes in logistics or inventory systems where checksum integrity is required. Developers often need to generate barcodes, read them from images, and ensure data correctness using these core API classes.
/// Prompt: Read a single Code 11 barcode image after enabling obligatory checksum verification with ChecksumValidation.On.
// Tags: code11, barcode, checksum, read, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code 11 barcode, saving it to a temporary file,
/// and reading it back with obligatory checksum validation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Creates a temporary barcode image, reads it with checksum validation,
    /// outputs the result, and cleans up temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code11Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "code11.png");

        // Generate a Code 11 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code11, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file exists before attempting to read
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image file was not found.");
            return;
        }

        // Read the barcode with checksum validation turned on
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code11))
        {
            // Enable obligatory checksum verification
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Perform the reading
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected or checksum validation failed.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Code Text : {result.CodeText}");
                    Console.WriteLine($"Code Type : {result.CodeTypeName}");
                }
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect demo execution
        }
    }
}