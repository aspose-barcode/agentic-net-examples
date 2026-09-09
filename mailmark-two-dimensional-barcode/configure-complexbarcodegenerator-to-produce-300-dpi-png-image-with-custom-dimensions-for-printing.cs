// Title: Generate MaxiCode Complex Barcode with Custom DPI and Dimensions
// Description: Demonstrates how to create a MaxiCode complex barcode, set a 300 dpi resolution, and define custom image width and height for printing.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with MaxiCodeCodetextMode3 and related classes to produce high‑resolution barcodes. Typical use cases include printing shipping labels, tickets, or any scenario where precise image size and resolution are required. Developers often need to control DPI, image dimensions, and module size when integrating barcodes into print workflows.
// Prompt: Configure ComplexBarcodeGenerator to produce a 300 dpi PNG image with custom dimensions for printing.
// Tags: maxicode, complexbarcode, barcode generation, resolution, png, aspose.barcode, image dimensions

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a MaxiCode complex barcode with custom DPI and image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Builds the MaxiCode codetext, configures the generator,
    /// and saves the resulting PNG image to a temporary location.
    /// </summary>
    static void Main()
    {
        // Prepare complex codetext for MaxiCode (structured second message)
        var secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add("123 Main St");
        secondMessage.Add("Anytown");
        secondMessage.Add("CA");
        secondMessage.Year = 23;

        // Assemble the full codetext for Mode 3 MaxiCode
        var codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "12345",
            CountryCode = 1,
            ServiceCategory = 0,
            SecondMessage = secondMessage
        };

        // Determine output file path in the system temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode.png");

        // Generate barcode with custom resolution and dimensions
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            // Set image resolution to 300 DPI
            generator.Parameters.Resolution = 300f;

            // Use nearest auto‑size mode to respect explicitly set dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Define custom image width and height (in pixels)
            generator.Parameters.ImageWidth.Pixels = 600f;
            generator.Parameters.ImageHeight.Pixels = 400f;

            // Set module (X‑dimension) size for the barcode
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}