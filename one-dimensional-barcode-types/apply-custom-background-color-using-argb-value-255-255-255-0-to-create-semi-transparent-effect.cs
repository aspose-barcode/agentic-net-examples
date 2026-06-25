using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom background color
/// and saving it as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Creates a barcode generator, configures its appearance, saves the image,
    /// and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "Sample123"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply a semi‑transparent background color (ARGB: 255, 255, 255, 0)
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 255, 0);

            // Define the output file path for the generated PNG image
            string outputPath = "barcode.png";

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);

            // Inform the user where the barcode image has been saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}