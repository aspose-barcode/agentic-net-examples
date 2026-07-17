// Title: DataMatrix barcode generation for ASP.NET MVC view
// Description: Generates a DataMatrix barcode image on the server and provides an HTML <img> tag that can be embedded in an MVC view.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating server‑side creation of barcodes using the BarcodeGenerator class. Typical use cases include rendering barcodes in web applications, exporting them as images, and embedding them in HTML. Developers often need to configure barcode parameters, choose output formats, and integrate the resulting image into MVC or Razor views.
// Prompt: Integrate DataMatrix generation into ASP.NET MVC view using server‑side rendering and an image tag.
// Tags: datamatrix, generation, png, aspnet mvc, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates server‑side generation of a DataMatrix barcode and outputs an HTML <img> tag for ASP.NET MVC integration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode image, saves it, and writes an <img> tag to the console.
    /// </summary>
    static void Main()
    {
        // Define output file name and the text to encode
        string outputFile = "datamatrix.png";
        string codeText = "Hello Aspose DataMatrix!";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputFile));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate DataMatrix barcode using Aspose.BarCode
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Configure a square DataMatrix version (20x20 modules)
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;
            generator.Parameters.Barcode.DataMatrix.AspectRatio = 1f;

            // Set auto‑size mode to interpolation and define image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 200f;

            // Save the generated barcode as a PNG file
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Output an HTML <img> tag that can be placed in an MVC view
        Console.WriteLine("<img src=\"{0}\" alt=\"DataMatrix Barcode\" />", outputFile);
    }
}