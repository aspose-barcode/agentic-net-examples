// Title: Generate QR Code with automatic size selection and save as SVG
// Description: Demonstrates creating a QR Code barcode, letting the library choose the optimal size, and exporting it to an SVG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with QR symbology, configure AutoSizeMode, and save the result in vector format. Developers working with barcode creation often need to produce scalable graphics for web or print, and this snippet shows the typical workflow using EncodeTypes, AutoSizeMode, and BarCodeImageFormat classes.
// Prompt: Generate a QR Code barcode with automatic size selection and store as SVG file.
// Tags: qr code, auto size, svg, generation, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with automatic size selection and saving it as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code and writes it to an SVG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the SVG image
        string outputPath = "qr_code.svg";

        // Ensure the target directory exists; create it if necessary
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the QR Code generator with sample text (URL)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Enable automatic size selection using interpolation mode
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Optional: set the error correction level (default is LevelM)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Attempt to save the barcode as an SVG file
            // Note: In evaluation mode, only Code39 supports SVG/EMF; handle that limitation gracefully
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"QR Code saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // Detect evaluation license limitation and inform the user
                if (ex.Message != null && ex.Message.IndexOf("evaluation", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine("A full license is required to export QR Code as SVG. The barcode was generated, but SVG export is not available in evaluation mode.");
                }
                else
                {
                    // Re‑throw any unexpected exceptions
                    throw;
                }
            }
        }
    }
}