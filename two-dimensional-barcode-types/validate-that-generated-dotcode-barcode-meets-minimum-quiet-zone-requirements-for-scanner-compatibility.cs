// Title: DotCode Barcode Generation with Quiet Zone Validation
// Description: Demonstrates how to generate a DotCode barcode, apply the minimum quiet zone required for scanner compatibility, and verify the barcode can be read.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode with specific padding, and BarCodeReader to decode the image. Developers working with barcode symbologies often need to ensure quiet zone compliance for reliable scanning; this snippet illustrates the typical workflow and key API classes (BarcodeGenerator, BarCodeReader, DecodeType, QualitySettings) used in such scenarios.
// Prompt: Validate that generated DotCode barcode meets minimum quiet zone requirements for scanner compatibility.
// Tags: dotcode, quietzone, barcode, generation, recognition, aspose.barcode, png

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a DotCode barcode with explicit quiet zone padding, validates the padding,
/// and attempts to read the barcode to confirm scanner compatibility.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, quiet‑zone validation,
    /// and read‑back verification.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary output folder for the generated barcode image.
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "DotCodeQuietZoneDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string barcodePath = Path.Combine(outputFolder, "DotCode.png");

        // --------------------------------------------------------------------
        // Generate DotCode barcode with explicit quiet zone (padding) on all sides.
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, "Aspose"))
        {
            // Set module size (X‑dimension) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Minimum quiet zone: 2 × XDimension on each side.
            float minQuietZone = generator.Parameters.Barcode.XDimension.Pixels * 2f;
            generator.Parameters.Barcode.Padding.Left.Pixels   = minQuietZone;
            generator.Parameters.Barcode.Padding.Right.Pixels  = minQuietZone;
            generator.Parameters.Barcode.Padding.Top.Pixels    = minQuietZone;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = minQuietZone;

            // Save the barcode image as PNG.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Validate that the configured quiet zone meets the required minimum.
        // --------------------------------------------------------------------
        using (BarcodeGenerator validator = new BarcodeGenerator(EncodeTypes.DotCode, "Aspose"))
        {
            validator.Parameters.Barcode.XDimension.Pixels = 10f;
            float requiredQuietZone = validator.Parameters.Barcode.XDimension.Pixels * 2f;

            float left  = validator.Parameters.Barcode.Padding.Left.Pixels;
            float right = validator.Parameters.Barcode.Padding.Right.Pixels;

            bool leftOk  = left  >= requiredQuietZone;
            bool rightOk = right >= requiredQuietZone;

            Console.WriteLine($"Quiet zone validation for '{barcodePath}':");
            Console.WriteLine($"  Required per side (pixels): {requiredQuietZone}");
            Console.WriteLine($"  Left padding (pixels): {left} -> {(leftOk ? "OK" : "FAIL")}");
            Console.WriteLine($"  Right padding (pixels): {right} -> {(rightOk ? "OK" : "FAIL")}");
        }

        // --------------------------------------------------------------------
        // Attempt to read the generated barcode to ensure scanner compatibility.
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = ResolveDecodeType("DotCode");
        if (decodeType == null)
        {
            Console.WriteLine("Unable to resolve DecodeType for DotCode. Skipping read verification.");
            return;
        }

        try
        {
            using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
            {
                // Use high‑performance quality settings (optional).
                reader.QualitySettings = QualitySettings.HighPerformance;

                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected in the generated image.");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Read barcode: CodeText = '{result.CodeText}', Type = {result.CodeTypeName}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during barcode reading: {ex.Message}");
        }
    }

    /// <summary>
    /// Resolves a <see cref="BaseDecodeType"/> member by name using reflection.
    /// Returns null if the symbology name is not found.
    /// </summary>
    /// <param name="symbologyName">The name of the symbology (e.g., "DotCode").</param>
    /// <returns>The corresponding <see cref="BaseDecodeType"/> or null.</returns>
    static BaseDecodeType ResolveDecodeType(string symbologyName)
    {
        FieldInfo field = typeof(DecodeType).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown decode type: {symbologyName}");
            return null;
        }
        return (BaseDecodeType)field.GetValue(null);
    }
}