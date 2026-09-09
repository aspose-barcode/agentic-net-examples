// Title: Generate a Mailmark 2D barcode with routing, service, and customer data
// Description: Demonstrates creating a Mailmark2DCodetext object, setting routing, service, and customer fields, and generating a PNG barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Mailmark 2D symbology. It showcases the use of Mailmark2DCodetext, ComplexBarcodeGenerator, and related parameters to build and render a Mailmark barcode, a common requirement for postal automation and tracking solutions. Developers often need to customize codetext fields and export barcode images for integration into mailing workflows.
// Prompt: Create a Mailmark2DCodetext instance and assign routing, service, and customer data values.
// Tags: mailmark, 2d barcode, complex barcode, generation, png, aspose.barcode, codetext

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a Mailmark 2D barcode with routing, service, and customer data, and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the Mailmark2DCodetext, generates the barcode image, and writes output paths to console.
    /// </summary>
    static void Main()
    {
        // Initialize Mailmark2DCodetext with routing, service, and customer information.
        var mailmark2D = new Mailmark2DCodetext
        {
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            RTSFlag = "0",
            SupplyChainID = 384224,
            ItemID = 16563762,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            CustomerContent = "CUST12",
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40,
            DataMatrixType = Mailmark2DType.Type_7
        };

        // Build the codetext string that will be encoded in the barcode.
        string constructed = mailmark2D.GetConstructedCodetext();
        Console.WriteLine("Constructed Codetext:");
        Console.WriteLine(constructed);

        // Prepare a temporary folder to store the generated barcode image.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Mailmark2D_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "Mailmark2D.png");

        // Generate the barcode using ComplexBarcodeGenerator and save it as PNG.
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Set the X-dimension (module size) for the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}