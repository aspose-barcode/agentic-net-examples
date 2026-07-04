// Title: Code128 Barcode with Uniform Padding and Rotation
// Description: Generates a Code128 barcode with 20-pixel padding on all sides, rotates it 90°, saves to PNG, and verifies readability to ensure no clipping.
// Prompt: Set uniform Padding of 20 pixels around a Code128 barcode and verify no clipping after rotation.
// Tags: code128, padding, rotation, png, aspose.barcode, barcodegeneration, barcoderecognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a Code128 barcode with uniform padding, rotating it,
/// saving the image, and verifying that the barcode can still be read.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies padding and rotation,
    /// saves the image, and checks that the barcode is readable after rotation.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "code128_padded_rotated.png";

        // Remove any existing file with the same name to avoid conflicts
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Create a Code128 barcode generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply uniform padding of 20 pixels on all four sides
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;

            // Rotate the barcode image by 90 degrees
            generator.Parameters.RotationAngle = 90f;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        // Ensure the barcode image was successfully created before attempting to read it
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Error: Barcode image was not created.");
            return;
        }

        // Use BarCodeReader to verify that the rotated barcode can be decoded (i.e., not clipped)
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code128))
        {
            bool found = false;

            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Read barcode: Type={result.CodeType}, CodeText={result.CodeText}");
                found = true;
            }

            // If no barcode was detected, report a possible clipping issue
            if (!found)
            {
                Console.WriteLine("Failed to read the barcode. It may be clipped after rotation.");
            }
        }
    }
}