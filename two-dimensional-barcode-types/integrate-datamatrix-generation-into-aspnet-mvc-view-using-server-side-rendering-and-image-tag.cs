// Title: Generate DataMatrix barcode and embed as HTML img tag for ASP.NET MVC
// Description: Demonstrates server‑side creation of a DataMatrix barcode image using Aspose.BarCode and shows how to embed it in an MVC view with an <img> tag.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to use BarcodeGenerator with EncodeTypes.DataMatrix, configure DataMatrix parameters, and save the result as PNG. Typical use cases include generating barcodes on the fly for web applications, reports, or documents. Developers often need to render barcodes server‑side and reference them in HTML views, which this snippet demonstrates.
// Prompt: Integrate DataMatrix generation into ASP.NET MVC view using server‑side rendering and an image tag.
// Tags: datamatrix, barcode, generation, aspnet mvc, image tag, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a DataMatrix barcode image and outputs an HTML <img> tag
/// suitable for inclusion in an ASP.NET MVC view.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a DataMatrix barcode, saves it as PNG, and prints an <img> tag.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the DataMatrix barcode
        string codeText = "Hello Aspose";

        // Determine a temporary file path for the generated PNG image
        string outputFile = Path.Combine(Path.GetTempPath(), "datamatrix.png");

        // Create a BarcodeGenerator for DataMatrix with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Optional: configure DataMatrix-specific parameters (size, error correction)
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
            generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;

            // Save the generated barcode image to the temporary file in PNG format
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Build an HTML <img> tag that references the generated image file name
        string imgTag = $"<img src=\"{Path.GetFileName(outputFile)}\" alt=\"DataMatrix Barcode\" />";

        // Output information to the console (useful for debugging or console apps)
        Console.WriteLine("Generated DataMatrix barcode saved to:");
        Console.WriteLine(outputFile);
        Console.WriteLine("HTML img tag for MVC view:");
        Console.WriteLine(imgTag);
    }
}