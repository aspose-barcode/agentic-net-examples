// Title: Generate a 300 dpi MaxiCode PNG with custom dimensions
// Description: This example creates a MaxiCode barcode (mode 3) and saves it as a 300 dpi PNG image with a 600×600 pixel size, suitable for high‑resolution printing.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation using the ComplexBarcodeGenerator class. The example builds a MaxiCode codetext (mode 3), configures image resolution and dimensions, and outputs a PNG file. Developers working with advanced barcode symbologies such as MaxiCode, QR, or DataMatrix often need to control DPI and image size for printing or embedding in documents; this snippet shows the typical API usage pattern for those scenarios.
/// Prompt: Configure ComplexBarcodeGenerator to produce a 300 dpi PNG image with custom dimensions for printing.
/// Tags: maxicode, barcode generation, png, 300dpi, complexbarcode, aspose.barcode, image dimensions

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a MaxiCode barcode with custom DPI and image size,
/// then saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the MaxiCode codetext, configures the generator,
    /// and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode_300dpi.png");

        // Create the secondary (unstructured) message part of the MaxiCode.
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode"
        };

        // Build the MaxiCode codetext for mode 3, including postal code, country code,
        // service category, and the secondary message.
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = secondMessage
        };

        // Optional: display the constructed codetext string for verification.
        Console.WriteLine("Constructed codetext: " + maxiCodeCodetext.GetConstructedCodetext());

        // Initialize the complex barcode generator with the prepared codetext.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set the image resolution to 300 DPI for high‑quality printing.
            generator.Parameters.Resolution = 300f;

            // Specify custom image dimensions (600 × 600 pixels).
            generator.Parameters.ImageWidth.Pixels = 600f;
            generator.Parameters.ImageHeight.Pixels = 600f;

            // Use AutoSizeMode.Nearest so the generator respects the custom dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}