// Title: Center-aligned Code128 barcode with auto-scaling for receipt printing
// Description: Demonstrates how to generate a narrow receipt‑friendly Code128 barcode, centering the human‑readable text and enabling automatic scaling to fit a specific image width.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating use of BarcodeGenerator, EncodeTypes, and image formatting classes. Typical use cases include creating barcodes for point‑of‑sale receipts, tickets, or labels where space is limited. Developers often need to control text alignment, scaling mode, and output dimensions to produce clear, printable barcodes.
// Prompt: Align barcode text to center, enable automatic scaling, and generate image suitable for narrow receipt printing.
// Tags: code128, alignment, autoscaling, png, barcodegenerator, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode image that is centered, auto‑scaled, and sized for narrow receipt printers.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures alignment and scaling, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the barcode content and output file name
        const string codeText = "1234567890";
        const string outputPath = "receipt_barcode.png";

        // Ensure the output directory exists (creates it if missing)
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Center the human‑readable text beneath the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Enable automatic scaling (interpolation) to fit the target width
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the desired image width for a typical receipt printer (≈2.78 in)
            generator.Parameters.ImageWidth.Point = 200f;
            // Height is auto‑calculated based on content and scaling mode

            // Use a small X‑dimension so the barcode fits within the narrow width
            generator.Parameters.Barcode.XDimension.Point = 1f;

            // Define barcode and background colors (black on white)
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
    }
}