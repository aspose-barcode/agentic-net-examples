using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Align human‑readable text to the right
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Enable automatic scaling (interpolation) for narrow label printing
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define the target image size (e.g., 150 pt × 50 pt) suitable for a narrow label
            generator.Parameters.ImageWidth.Point = 150f;
            generator.Parameters.ImageHeight.Point = 50f;

            // Optional: increase resolution for better print quality
            generator.Parameters.Resolution = 300; // DPI

            // Save the generated barcode image
            generator.Save("label.png");
        }
    }
}