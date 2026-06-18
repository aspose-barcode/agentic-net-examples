using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it as a PNG,
/// and then reading it back to verify readability using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, saves it, and validates that it can be read.
    /// </summary>
    static void Main()
    {
        // Output file name for the generated barcode image
        const string outputPath = "barcode.png";

        // Text to encode in the barcode
        const string codeText = "1234567890";

        // ------------------------------------------------------------
        // Generate barcode with Interpolation mode and 300 DPI resolution
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Use interpolation auto-size mode for better scaling
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set image resolution to 300 DPI
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that the image file was created successfully
        // ------------------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to create barcode image at '{outputPath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Read the saved barcode image and validate readability
        // ------------------------------------------------------------
        using (var bitmap = new Bitmap(outputPath))
        {
            // Initialize a barcode reader that supports all barcode types
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                int count = 0; // Counter for detected barcodes

                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                    count++;
                }

                // Report the outcome based on detection count
                if (count > 0)
                {
                    Console.WriteLine("Barcode read successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to read barcode.");
                }
            }
        }
    }
}