using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a Code128 barcode with custom margins and padding,
/// and save it as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, applies custom
    /// margin and padding settings, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file name and location.
        const string outputPath = "custom_margin_padding.png";

        // Initialize a BarcodeGenerator for Code128 with the sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // --------------------------------------------------------------------
            // Configure barcode padding (quiet zone) to increase scanner tolerance.
            // Each side is set to 30 points.
            // --------------------------------------------------------------------
            generator.Parameters.Barcode.Padding.Left.Point   = 30f;
            generator.Parameters.Barcode.Padding.Top.Point    = 30f;
            generator.Parameters.Barcode.Padding.Right.Point  = 30f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 30f;

            // --------------------------------------------------------------------
            // Define the overall image dimensions, effectively creating a margin
            // around the barcode within the image canvas.
            // --------------------------------------------------------------------
            generator.Parameters.ImageWidth.Point  = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // --------------------------------------------------------------------
            // Disable automatic sizing to ensure the explicit dimensions above are used.
            // --------------------------------------------------------------------
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // --------------------------------------------------------------------
            // Optional visual settings: white background and black bars for contrast.
            // --------------------------------------------------------------------
            generator.Parameters.BackColor          = Color.White;
            generator.Parameters.Barcode.BarColor   = Color.Black;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}