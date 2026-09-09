// Title: Enable or disable anti-aliasing for barcode images
// Description: Demonstrates how to configure the Aspose.BarCode generator to turn anti‑aliasing on or off, producing sharper or more pixel‑perfect barcode PNG files.
// Category-Description: This example belongs to the Aspose.BarCode image rendering category, illustrating the use of the BarcodeGenerator class and its Parameters property to control rendering options such as anti‑aliasing. Developers creating barcodes for print or screen can adjust this setting to improve visual quality, especially when scaling images. The snippet shows typical usage for Code128 symbology and PNG output, a common scenario in inventory and labeling applications.
// Prompt: Provide configuration to enable or disable anti‑aliasing on generated barcode images for sharper output.
// Tags: barcode, anti-aliasing, rendering, code128, png, aspnet, aspose.barcode, image-output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates Code128 barcodes with anti‑aliasing enabled and disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, generates two PNG barcodes (one with anti‑aliasing on, one off),
    /// and writes the file paths to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary directory for the output images
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeAntiAlias_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Text to encode in the barcode
        string codeText = "Sample123";

        // ------------------------------------------------------------
        // Generate barcode with anti‑aliasing enabled
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Turn on anti‑aliasing for smoother edges
            generator.Parameters.UseAntiAlias = true;

            // Define the output file path
            string enabledPath = Path.Combine(outputDir, "barcode_aa_enabled.png");

            // Save the barcode as a PNG image
            generator.Save(enabledPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Saved with anti-aliasing enabled: {enabledPath}");
        }

        // ------------------------------------------------------------
        // Generate barcode with anti‑aliasing disabled
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Turn off anti‑aliasing for a crisp, pixel‑perfect image
            generator.Parameters.UseAntiAlias = false;

            // Define the output file path
            string disabledPath = Path.Combine(outputDir, "barcode_aa_disabled.png");

            // Save the barcode as a PNG image
            generator.Save(disabledPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Saved with anti-aliasing disabled: {disabledPath}");
        }
    }
}