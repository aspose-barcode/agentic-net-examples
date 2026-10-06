// Title: Generate Fixed-Size QR Code Barcode
// Description: Demonstrates how to create a QR Code barcode with a fixed image size by disabling automatic sizing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to produce barcodes with custom dimensions. Developers often need to generate barcodes that fit specific layout constraints, such as fixed-width images for UI elements or printed materials. The snippet shows setting image width, height, and module size while turning off auto‑size, a common requirement in UI design and reporting scenarios.
// Prompt: Generate QR Code barcode and disable automatic size to enforce fixed dimensions.
// Tags: qr code, barcode generation, fixed size, autosizemode, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with a fixed image size using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output directory, generates the QR Code, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "FixedSizeQRCode.png");

        // Initialize the barcode generator for a QR Code with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Disable automatic sizing by selecting a fixed canvas size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = 300f;   // Set fixed image width
            generator.Parameters.ImageHeight.Pixels = 300f;  // Set fixed image height

            // Optionally adjust the module (dot) size within the QR Code
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}