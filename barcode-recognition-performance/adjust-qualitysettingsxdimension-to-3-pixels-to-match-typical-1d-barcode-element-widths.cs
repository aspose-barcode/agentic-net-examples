// Title: Adjust XDimension for 1D Barcode Generation and Recognition
// Description: Demonstrates setting XDimension for a Code128 barcode and configuring reader quality settings to use a minimal element width of 3 pixels.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a 1D barcode, and BarCodeReader with QualitySettings to fine‑tune detection parameters. Developers commonly need to control XDimension for accurate printing and reliable scanning, especially when matching industry‑standard element sizes.
// Prompt: Adjust QualitySettings.XDimension to 3 pixels to match typical 1D barcode element widths.
// Tags: barcode, code128, generation, recognition, xdimension, qualitysettings, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, adjusts XDimension settings,
/// reads the barcode back, and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, configures reader quality settings, outputs detection results,
    /// and removes temporary artifacts.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode with a custom XDimension
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Set XDimension to 2 pixels (typical for 1D barcodes)
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the barcode using adjusted QualitySettings
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Enable minimal XDimension mode and set the minimal element size to 3 pixels
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 3f;

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Detected Symbology: {result.CodeTypeName}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directories
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}