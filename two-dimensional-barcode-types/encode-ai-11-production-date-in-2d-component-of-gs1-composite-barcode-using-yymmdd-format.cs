using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a GS1 Composite barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates and saves a GS1 Composite barcode image.
    /// </summary>
    static void Main()
    {
        // Define the linear component (GTIN) and the 2D component (AI 11 - production date) in YYMMDD format.
        string linearPart = "(01)01234567890123";
        string twoDPart   = "(11)230915"; // production date: 15 Sep 2023

        // Combine linear and 2D parts using the '|' separator required for GS1 Composite barcodes.
        string codeText = $"{linearPart}|{twoDPart}";

        // Initialize the barcode generator for a GS1 Composite barcode with the combined data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Configure the linear component to use GS1 Code 128 encoding.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Configure the 2D component to use MicroPDF417 with CC_A mode.
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional visual settings: set module width (X dimension) and bar height in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Define the output file path and save the generated barcode image.
            string outputPath = "gs1composite.png";
            generator.Save(outputPath);

            // Inform the user where the barcode image has been saved.
            Console.WriteLine($"GS1 Composite barcode saved to: {outputPath}");
        }
    }
}