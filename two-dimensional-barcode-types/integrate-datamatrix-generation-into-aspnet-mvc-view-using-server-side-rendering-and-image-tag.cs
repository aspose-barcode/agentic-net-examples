// Title: Server‑Side DataMatrix Barcode Generation for ASP.NET MVC
// Description: Demonstrates generating a DataMatrix barcode image using Aspose.BarCode and outputting an HTML <img> tag for inclusion in an MVC view.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to create barcode images on the server using the BarcodeGenerator class. Typical use cases include embedding barcodes in web pages, reports, or documents. Developers often need to configure symbology settings, resolution, and output formats before rendering the image for client‑side display.
// Prompt: Integrate DataMatrix generation into ASP.NET MVC view using server‑side rendering and an image tag.
// Tags: datamatrix, barcode, generation, png, aspnet-mvc, server-side-rendering

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a DataMatrix barcode image and writes an HTML <img> tag that can be embedded in an ASP.NET MVC view.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the temporary file path where the barcode image will be saved.
        string outputPath = Path.Combine(Path.GetTempPath(), "datamatrix.png");

        // Remove any existing file with the same name to avoid conflicts.
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Create a BarcodeGenerator for the DataMatrix symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, string.Empty))
        {
            // Set the encoded text (GS1 example) using UTF‑8 encoding.
            generator.SetCodeText("(01)12345678901231(21)ASPOSE", Encoding.UTF8);

            // Configure DataMatrix‑specific parameters.
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Optional: define the image resolution (dots per inch).
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Emit an HTML <img> tag referencing the generated image; suitable for inclusion in an MVC view.
        Console.WriteLine($"<img src=\"{outputPath}\" alt=\"DataMatrix Barcode\" />");
    }
}