// Title: Generate Dutch KIX 2‑state Postal Barcode with Validation and Checksum
// Description: Demonstrates creating a Dutch KIX barcode from a numeric string, validating the input, and saving the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator with EncodeTypes.DutchKIX, configure visual parameters, and verify the barcode using BarCodeReader. Developers working with postal symbologies often need to generate KIX barcodes, validate numeric data, and read back the encoded information.
// Prompt: Generate a Dutch KIX 2‑state postal barcode with numeric input validation and automatic checksum.
// Tags: barcode, generation, recognition, dutch kix, postal, validation, checksum, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating and reading a Dutch KIX 2‑state postal barcode with numeric validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Dutch KIX barcode from a numeric string, saves it as PNG, and reads it back to verify.
    /// </summary>
    static void Main()
    {
        // Sample numeric input; replace with any numeric string as needed
        string input = "1234567890";

        // Validate that the input contains only digits
        if (string.IsNullOrEmpty(input) || !IsAllDigits(input))
        {
            throw new ArgumentException("Input must be a non‑empty string of numeric digits only.");
        }

        // Prepare a temporary output folder and file path
        string outputFolder = Path.Combine(Path.GetTempPath(), "KIXDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "DutchKIX.png");

        // Generate Dutch KIX barcode using the validated input
        using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, input))
        {
            // Optional visual settings: set module size and bar height
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.BarHeight.Pixels = 50;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Dutch KIX barcode saved to: {outputPath}");

        // Read back the barcode image to verify its content
        using (var bitmap = (Bitmap)Image.FromFile(outputPath))
        {
            BaseDecodeType decodeType = DecodeType.DutchKIX;
            using (var reader = new BarCodeReader(bitmap, decodeType))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
            }
        }
    }

    // Helper method to ensure a string consists solely of digit characters
    static bool IsAllDigits(string s)
    {
        foreach (char c in s)
        {
            if (c < '0' || c > '9')
                return false;
        }
        return true;
    }
}