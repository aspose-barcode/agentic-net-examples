// Title: Code128 barcode with uniform padding and rotation
// Description: Demonstrates how to apply a 20‑pixel padding around a Code128 barcode, rotate the image, and verify that the barcode remains readable.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes with custom padding and rotation, and BarCodeReader for validating the output. Developers often need to adjust barcode margins and orientation for layout constraints, ensuring that the resulting image is not clipped and can be decoded reliably.
// Prompt: Set uniform Padding of 20 pixels around a Code128 barcode and verify no clipping after rotation.
// Tags: code128, padding, rotation, barcode, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode with uniform padding, rotating it, and verifying readability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and checks if it can be decoded after rotation.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the output file path and the barcode text
        string barcodePath = Path.Combine(tempFolder, "code128_padding_rotated.png");
        string codeText = "1234567890";

        // Generate a Code128 barcode with uniform 20‑pixel padding and rotate it by 45 degrees
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply uniform padding on all sides
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;

            // Set rotation angle
            generator.Parameters.RotationAngle = 45f; // rotate 45 degrees

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the rotated barcode can be read (ensuring no clipping occurred)
        bool readable = false;
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            var results = reader.ReadBarCodes();
            if (results != null && results.Length > 0 && results[0].CodeText == codeText)
            {
                readable = true;
            }
        }

        // Output the verification result
        Console.WriteLine(readable
            ? "Success: barcode readable after rotation with padding."
            : "Failure: barcode could not be read after rotation.");
    }
}