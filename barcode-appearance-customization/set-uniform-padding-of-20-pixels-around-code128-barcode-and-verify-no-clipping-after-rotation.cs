// Title: Code128 barcode with uniform padding and rotation verification
// Description: Demonstrates setting a 20-pixel padding around a Code128 barcode, rotating it, and confirming that the image is not clipped.
// Category-Description: This example belongs to the Aspose.BarCode image generation and recognition category. It shows how to use BarcodeGenerator to configure padding and rotation, and BarCodeReader to validate the output. Developers often need to adjust barcode margins and verify readability after transformations, especially for printing and scanning workflows.
// Prompt: Set uniform Padding of 20 pixels around a Code128 barcode and verify no clipping after rotation.
// Tags: code128, padding, rotation, png, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates setting uniform padding around a Code128 barcode, rotating it, and verifying readability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, and validates that it can be read after rotation.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "code128.png";

        // Create a Code128 barcode generator with the sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply a uniform padding of 20 pixels on all four sides
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;

            // Rotate the barcode image by 90 degrees
            generator.Parameters.RotationAngle = 90f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Error: Barcode image was not created.");
            return;
        }

        // Use BarCodeReader to decode the saved image and confirm the content matches the original text
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();

            // Check if at least one barcode was read and the decoded text is correct
            if (results.Length > 0 && results[0].CodeText == "1234567890")
            {
                Console.WriteLine("Success: Barcode read correctly after rotation. No clipping detected.");
            }
            else
            {
                Console.WriteLine("Warning: Barcode could not be read after rotation. Possible clipping.");
            }
        }
    }
}