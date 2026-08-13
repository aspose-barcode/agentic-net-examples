// Title: Generate DataMatrix barcode and embed in ASP.NET MVC view
// Description: Demonstrates server‑side generation of a DataMatrix barcode image using Aspose.BarCode and how to embed it in an MVC view with an <img> tag.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to create barcode images (DataMatrix) on the server, configure visual properties, and save them in common formats like PNG. It uses key classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers frequently employ when integrating barcodes into web applications, especially ASP.NET MVC projects where the image is served via a virtual path.
// Prompt: Integrate DataMatrix generation into ASP.NET MVC view using server‑side rendering and an image tag.
// Tags: datamatrix, barcode, generation, aspnet mvc, image, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Entry point for the DataMatrix barcode generation demo.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DataMatrix barcode, saves it as a PNG file, and outputs an HTML <img> tag for use in an ASP.NET MVC view.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder to store the generated barcode image
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Output file path (relative URL for MVC view)
        string fileName = "datamatrix.png";
        string outputPath = Path.Combine(outputFolder, fileName);

        // Sample DataMatrix code text
        string codeText = "Sample123";

        // Generate the DataMatrix barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set DataMatrix specific parameters
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
            generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.Auto;
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Optional visual settings
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Resolution = 300f; // 300 DPI for sharper image

            // Save as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Build an HTML <img> tag that can be used in an ASP.NET MVC view
        // Assume the image will be served from a virtual "/images/" folder mapped to the output folder
        string imageUrl = "/images/" + fileName;
        string imgTag = $"<img src=\"{imageUrl}\" alt=\"DataMatrix Barcode\" />";

        // Output the HTML snippet to the console
        Console.WriteLine("Generated DataMatrix barcode at:");
        Console.WriteLine(outputPath);
        Console.WriteLine();
        Console.WriteLine("Use the following <img> tag in your MVC view:");
        Console.WriteLine(imgTag);
    }
}