// Title: Validate Quiet Zone of Han Xin Barcode
// Description: Generates a Han Xin barcode with a defined quiet zone, saves it as PNG, and verifies detection using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use the BarcodeGenerator class to create a barcode with specific padding (quiet zone) and the BarCodeReader class to decode the image. Typical use cases include ensuring barcode readability in automated scanning systems where quiet zone dimensions are critical. Developers often need to adjust XDimension and padding to meet scanner specifications, making this pattern common in barcode validation workflows.
// Prompt: Validate that generated Han Xin barcode meets required quiet zone dimensions for scanner reliability.
// Tags: hanxin, barcode, quiet zone, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Han Xin barcode with explicit quiet zone padding
/// and validates its readability using Aspose.BarCode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary barcode image, reads it back,
    /// and reports whether the quiet zone is sufficient for detection.
    /// </summary>
    static void Main()
    {
        // Sample Han Xin code text
        const string codeText = "1234567890";

        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "HanXinQuietZone_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated PNG image
        string barcodePath = Path.Combine(tempFolder, "hanxin.png");

        // Generate Han Xin barcode with explicit quiet zone (padding)
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Set module size (pixel width of a single barcode element)
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Define a quiet zone of 10 pixels on each side
            generator.Parameters.Barcode.Padding.Left.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

            // Save the barcode image to the temporary path
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Load the image and attempt to read the barcode
        using (var bitmap = new Bitmap(barcodePath))
        {
            // Use the appropriate decode type for Han Xin
            using (var reader = new BarCodeReader(bitmap, DecodeType.HanXin))
            {
                var results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected. Quiet zone may be insufficient.");
                }
                else
                {
                    var result = results[0];
                    bool textMatches = string.Equals(result.CodeText, codeText, StringComparison.Ordinal);
                    Console.WriteLine($"Barcode detected. CodeText match: {textMatches}");
                    Console.WriteLine($"Image dimensions: {bitmap.Width}x{bitmap.Height} pixels");
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
            // Ignored - cleanup failure should not affect validation result
        }
    }
}