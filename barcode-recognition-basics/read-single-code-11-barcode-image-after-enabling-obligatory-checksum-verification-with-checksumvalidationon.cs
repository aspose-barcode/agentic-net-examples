using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and reading of a Code11 barcode with checksum validation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code11 barcode image, reads it back with checksum validation, and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // Define the numeric text to encode in the Code11 barcode.
        string codeText = "123456";

        // Build a temporary file path for the barcode image.
        string tempImagePath = Path.Combine(Path.GetTempPath(), "code11.png");

        // -------------------- Barcode Generation --------------------
        // Create a BarcodeGenerator for Code11 and set checksum inclusion.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code11, codeText))
        {
            // Enable checksum generation.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode image to the temporary path.
            generator.Save(tempImagePath);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(tempImagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -------------------- Barcode Reading --------------------
        // Initialize a BarCodeReader for Code11 with checksum validation enabled.
        using (var reader = new BarCodeReader(tempImagePath, DecodeType.Code11))
        {
            // Require checksum verification during reading.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Read all barcodes from the image.
            var results = reader.ReadBarCodes();

            // Output detection results.
            if (results.Length == 0)
            {
                Console.WriteLine("No Code11 barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Code11 barcode: {result.CodeText}");
                }
            }
        }

        // -------------------- Cleanup --------------------
        // Attempt to delete the temporary image file; ignore any errors.
        try
        {
            File.Delete(tempImagePath);
        }
        catch
        {
            // Cleanup failure is non‑critical; continue execution.
        }
    }
}