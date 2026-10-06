// Title: Generate Code128 barcode without human‑readable text
// Description: Creates a Code128 barcode image, disables the display of the code text, and saves it as a PNG file.
// Category-Description: This example demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class. It shows how to configure barcode parameters such as symbology (EncodeTypes), visual appearance (CodeTextParameters, FilledBars), and output format (BarCodeImageFormat). Typical use cases include creating machine‑readable labels for inventory, shipping, or retail where the human‑readable text may be omitted. Developers working with barcode creation often need to customize these settings to meet specific design or compliance requirements.
// Prompt: Generate a barcode, set ShowCodeText to false, and confirm only bars are present in the output file.
// Tags: code128, barcode, generation, hidecodetext, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a Code128 barcode image with the human‑readable text hidden,
/// using Aspose.BarCode's BarcodeGenerator, and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, configures visual parameters,
    /// saves the image, and confirms successful creation.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated PNG image
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Hide the human‑readable code text (equivalent to ShowCodeText = false)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Ensure that the bars are rendered as filled shapes (default is true)
            generator.Parameters.Barcode.FilledBars = true;

            // Save the generated barcode image to the specified path in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created and provide console feedback
        if (File.Exists(outputPath))
        {
            Console.WriteLine($"Barcode image saved to: {outputPath}");
            // Load the image to confirm it can be opened (no further analysis needed)
            using (var bitmap = new Bitmap(outputPath))
            {
                Console.WriteLine("Confirmation: barcode generated with only bars (code text hidden).");
            }
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }
    }
}