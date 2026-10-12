// Title: Generate Code 16K barcode with custom XDimension and quiet‑zone scaling
// Description: Demonstrates how to set the XDimension to 0.33 mm for a Code 16K barcode, adjust quiet‑zone coefficients, save the image as PNG, and verify the result by reading the barcode back.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include customizing barcode dimensions, quiet‑zone handling, and validating output in automated pipelines. Developers often need to fine‑tune XDimension and quiet‑zone settings to meet printing specifications and ensure reliable scanning.
// Prompt: Set XDimension to 0.33 mm for Code 16K generation, verify quiet zone scaling.
// Tags: code16k, xdimension, quietzone, barcode-generation, barcode-recognition, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code 16K barcode with a specific XDimension,
/// custom quiet‑zone coefficients, saves it as a PNG file, and then reads it back
/// to verify the encoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saving, and verification.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary output folder for the generated barcode image.
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "Code16K_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string barcodePath = Path.Combine(outputFolder, "code16k.png");

        // --------------------------------------------------------------------
        // Generate a Code 16K barcode with XDimension set to 0.33 mm.
        // Adjust quiet‑zone coefficients to demonstrate scaling behavior.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose16K"))
        {
            // Set the module width (XDimension) in millimeters.
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;

            // Configure quiet‑zone scaling coefficients (default minimum values).
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 1;

            // Save the generated barcode as a PNG image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Output information about the generated barcode.
        Console.WriteLine($"Barcode saved to: {barcodePath}");
        Console.WriteLine($"XDimension set to: {0.33f} mm");
        Console.WriteLine($"QuietZoneLeftCoef: 10, QuietZoneRightCoef: 1");

        // --------------------------------------------------------------------
        // Verify the barcode by reading it back using BarCodeReader.
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Code16K;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            var results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }
    }
}