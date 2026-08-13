// Title: Generate MaxiCode barcode with custom quiet zone
// Description: Demonstrates creating a MaxiCode barcode and customizing its quiet zone (padding) to meet specific scanning requirements.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and related parameter settings to control barcode appearance, including quiet zone and module size. Developers working with shipping, logistics, or inventory systems often need to generate MaxiCode symbols with precise layout constraints for reliable scanning.
/// Prompt: Generate a MaxiCode barcode with a custom quiet zone size to meet specific scanning requirements.
// Tags: maxicode, barcode, quiet zone, padding, generation, aspose.barcode, png, complexbarcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a MaxiCode barcode with a custom quiet zone (padding) and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, applies custom padding, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the output file path
        string outputPath = "maxicode.png";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Prepare MaxiCode codetext (Mode 3) with a standard second message
        var maxiCodeData = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",   // 6‑character alphanumeric postal code
            CountryCode = 56,       // Country code (e.g., USA = 56)
            ServiceCategory = 999   // Example service category
        };

        // Create the standard second message
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode"
        };
        maxiCodeData.SecondMessage = secondMessage;

        // Generate the barcode with custom quiet zone (padding)
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            // Set individual padding values (quiet zone) in points
            generator.Parameters.Barcode.Padding.Left.Point = 15f;
            generator.Parameters.Barcode.Padding.Top.Point = 15f;
            generator.Parameters.Barcode.Padding.Right.Point = 15f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 15f;

            // Optionally adjust the module size (X dimension) in points
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"MaxiCode barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}