// Title: Generate Code128 barcode and read with checksum validation off
// Description: Demonstrates creating a Code128 barcode image and reading it while disabling checksum validation to show false positive detection handling.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, illustrating the use of BarcodeGenerator for image creation and BarCodeReader with customizable settings such as ChecksumValidation. Developers often need to generate barcodes, save them in various formats, and later decode them while controlling validation behavior to handle imperfect scans or custom checksum requirements.
// Prompt: Generate a barcode with BarcodeGenerator, then read it using ChecksumValidation.Off to observe false positive detections.
// Tags: code128, barcode generation, barcode recognition, checksumvalidation, off, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and reading with checksum validation disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode image, then reads it with checksum validation turned off.
    /// </summary>
    static void Main()
    {
        // Define the file path where the barcode image will be saved
        string imagePath = "sample.png";

        // Create a Code128 barcode containing the specified data and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789012"))
        {
            // Persist the generated barcode image to disk
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at {imagePath}");
            return;
        }

        // Initialize a barcode reader for the saved image, targeting Code128 symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Turn off checksum validation to allow detection of barcodes even when checksums are incorrect
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Output basic barcode information
                Console.WriteLine($"Detected Type: {result.CodeType}");
                Console.WriteLine($"CodeText: {result.CodeText}");

                // If extended 1D barcode data is available, display value and checksum details
                if (result.Extended != null && result.Extended.OneD != null)
                {
                    Console.WriteLine($"Value: {result.Extended.OneD.Value}");
                    Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                }
            }
        }
    }
}