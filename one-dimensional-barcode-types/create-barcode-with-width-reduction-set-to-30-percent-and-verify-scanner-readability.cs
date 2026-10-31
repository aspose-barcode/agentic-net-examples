// Title: Generate Code128 barcode with 30% width reduction and verify readability
// Description: This example creates a Code128 barcode, reduces its width by 30 percent, saves it as a PNG file, and checks that it can be decoded correctly.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. It uses BarcodeGenerator to customize barcode dimensions (XDimension and BarWidthReduction) and BarCodeReader to validate the output. Typical for developers who need to fine‑tune barcode size for limited space while ensuring scanner compatibility. Suitable for collections of examples on barcode customization, image export, and read‑back verification.
// Prompt: Create a barcode with width reduction set to 30 percent and verify scanner readability.
// Tags: code128, width-reduction, barcode-generation, barcode-recognition, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a 30% width reduction and verifying its readability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and validates it using a reader.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory for the output image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);

        // Define the output file path and the barcode text
        string barcodePath = Path.Combine(tempDir, "barcode30pr.png");
        string codeText = "ASPOSE30";

        // Generate a Code128 barcode with a 30% width reduction (approximate)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the base module width (X-dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Reduce the barcode width by 30% of the base module width
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 3f; // 30% of 10px

            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the saved barcode can be read correctly
        bool readable = false;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            if (results != null && results.Length > 0 && results[0].CodeText == codeText)
            {
                readable = true;
            }
        }

        // Output the results
        Console.WriteLine($"Barcode generated at: {barcodePath}");
        Console.WriteLine($"Readability verification: {(readable ? "Success" : "Failure")}");
    }
}