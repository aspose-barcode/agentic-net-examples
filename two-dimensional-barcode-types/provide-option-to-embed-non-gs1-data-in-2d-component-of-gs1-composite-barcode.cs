using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a GS1 Composite barcode with a non‑GS1 2D component using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates and saves a GS1 Composite barcode image.
    /// </summary>
    static void Main()
    {
        // Define combined codetext: linear part follows GS1 syntax (AI 01 for GTIN),
        // 2D part contains non‑GS1 data separated by a pipe character.
        string codeText = "(01)01234567890123|HelloWorld";

        // Initialize a barcode generator for GS1 Composite Bar with the combined codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the linear component to GS1 Code128.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set the 2D component to MicroPDF417 (CC_A).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Allow the 2D component to contain non‑GS1 data.
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Optional visual adjustments:
            // - X dimension (module width) set to 2 points.
            // - Bar height set to 50 points.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 50f;

            // Define output file path and save the generated barcode image.
            string outputPath = "gs1_composite_non_gs1.png";
            generator.Save(outputPath);

            // Inform the user where the barcode image was saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}