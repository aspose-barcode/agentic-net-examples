using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image suitable for narrow receipts using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "receipt_barcode.png";

        // Initialize a BarcodeGenerator for Code128 with the sample data "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // ------------------------------------------------------------
            // Configure image sizing and resolution
            // ------------------------------------------------------------

            // Enable automatic scaling (interpolation) to adapt the barcode to narrow receipt widths.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the target image dimensions in points (1 point = 1/72 inch).
            generator.Parameters.ImageWidth.Point = 200f;   // Width of the barcode image.
            generator.Parameters.ImageHeight.Point = 80f;   // Height of the barcode image.

            // Use a high resolution (dots per inch) for crisp printing on receipts.
            generator.Parameters.Resolution = 300f;

            // ------------------------------------------------------------
            // Configure human‑readable text appearance
            // ------------------------------------------------------------

            // Center the barcode's human‑readable text horizontally beneath the bars.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Set a readable font size for the human‑readable text (in points).
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

            // ------------------------------------------------------------
            // Configure padding around the barcode within the image
            // ------------------------------------------------------------

            // Apply a small uniform padding to keep the barcode away from the image edges.
            generator.Parameters.Barcode.Padding.Left.Point   = 2f;
            generator.Parameters.Barcode.Padding.Right.Point  = 2f;
            generator.Parameters.Barcode.Padding.Top.Point    = 2f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 2f;

            // ------------------------------------------------------------
            // Save the generated barcode image to the specified file
            // ------------------------------------------------------------

            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}