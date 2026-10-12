// Title: Generate MaxiCode Barcode Image in ASP.NET MVC
// Description: Demonstrates creating a MaxiCode barcode using Aspose.BarCode and saving it as a PNG file. In a real MVC controller the barcode text would be supplied via query string parameters.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.MaxiCode to produce high‑density 2‑D barcodes. Typical use cases include shipping labels, parcel tracking, and inventory management where MaxiCode is required. Developers often need to customize module size, aspect ratio, and output format, then return the image from a web endpoint.
// Prompt: Create an ASP.NET MVC action that returns a generated MaxiCode barcode image based on query string parameters.
// Tags: maxicode, barcode generation, aspnet mvc, image output, png, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode image using Aspose.BarCode.
/// In a real ASP.NET MVC application the barcode text would be obtained from the request query string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that simulates receiving a barcode text value, generates the MaxiCode image,
    /// and writes the output file path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument is used as the barcode text if provided.</param>
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Simulate obtaining the barcode text from query string parameters.
        // ------------------------------------------------------------
        string codeText = "Sample MaxiCode";
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            codeText = args[0];
        }

        // ------------------------------------------------------------
        // Prepare a temporary folder to store the generated image.
        // ------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeMaxiCodeDemo");
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "maxicode.png");

        // ------------------------------------------------------------
        // Create and configure the MaxiCode barcode generator.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Set the module (dot) size in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 15f;

            // Optional: adjust the aspect ratio; the default value (1.0) is usually sufficient.
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1.0f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"MaxiCode barcode generated at: {outputPath}");
    }
}