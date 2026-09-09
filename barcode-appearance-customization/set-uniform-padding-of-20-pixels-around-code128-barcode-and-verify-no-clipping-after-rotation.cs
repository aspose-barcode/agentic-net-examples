// Title: Code128 Barcode with Uniform Padding and Rotation
// Description: Demonstrates how to apply a uniform 20‑pixel padding around a Code128 barcode, rotate it, and verify that the barcode remains readable without clipping.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use the BarcodeGenerator class to configure barcode appearance (padding, rotation) and the BarCodeReader class to validate the output. Developers working with barcode imaging often need to adjust margins and orientation to fit layout requirements while ensuring the barcode can still be decoded.
// Prompt: Set uniform Padding of 20 pixels around a Code128 barcode and verify no clipping after rotation.
// Tags: code128, padding, rotation, barcode generation, barcode recognition, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with uniform padding, rotates it, and verifies that it can be decoded without clipping.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary directory, generates a padded and rotated barcode,
    /// saves it as PNG, and checks that the barcode can be read correctly.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the output file path and the text to encode
        string barcodePath = Path.Combine(outputDir, "Code128_Padded_Rotated.png");
        string codeText = "ASPOSE";

        // Generate the barcode with uniform 20‑pixel padding on all sides and rotate it by 45 degrees
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;
            generator.Parameters.RotationAngle = 45f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the generated barcode can be decoded (ensuring no clipping occurred)
        bool decoded = false;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                if (result.CodeText == codeText)
                {
                    decoded = true;
                    break;
                }
            }
        }

        // Output the verification result
        Console.WriteLine(decoded
            ? $"Barcode decoded successfully from '{barcodePath}'."
            : $"Failed to decode barcode from '{barcodePath}'.");
    }
}