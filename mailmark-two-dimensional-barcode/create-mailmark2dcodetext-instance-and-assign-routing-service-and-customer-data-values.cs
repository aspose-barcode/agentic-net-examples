// Title: Generate Mailmark 2D Barcode with Custom Routing, Service, and Customer Data
// Description: Demonstrates how to create a Mailmark2DCodetext instance, assign routing, service, and customer data values, and generate a PNG barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of Mailmark2DCodetext, ComplexBarcodeGenerator, and related parameter classes to produce Mailmark 2D barcodes, a common requirement for postal automation and tracking solutions. Developers often need to customize codetext fields such as routing information, service flags, and customer content before rendering the barcode image.
/// Prompt: Create a Mailmark2DCodetext instance and assign routing, service, and customer data values.
/// Tags: mailmark, 2d barcode, aspose.barcode, complexbarcode, generation, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that builds a Mailmark 2D codetext, displays it, and saves the generated barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares output folder, constructs the Mailmark2DCodetext,
    /// generates the barcode image, and writes status messages to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "Mailmark2DExample");
        Directory.CreateDirectory(outputDir);

        // Create Mailmark2DCodetext and assign routing, service, and customer data
        Mailmark2DCodetext mailmark2D = new Mailmark2DCodetext();
        mailmark2D.UPUCountryID = "JGB ";
        mailmark2D.InformationTypeID = "0";
        mailmark2D.VersionID = "1";
        mailmark2D.Class = "1";
        mailmark2D.RTSFlag = "0";
        mailmark2D.SupplyChainID = 384224;
        mailmark2D.ItemID = 16563762;
        mailmark2D.DestinationPostCodeAndDPS = "EF61AH8T ";
        mailmark2D.CustomerContent = "CUST1"; // max length 6
        mailmark2D.CustomerContentEncodeMode = DataMatrixEncodeMode.C40;
        mailmark2D.DataMatrixType = Mailmark2DType.Type_7;

        // Retrieve and display the constructed codetext string
        string constructed = mailmark2D.GetConstructedCodetext();
        Console.WriteLine("Constructed Codetext:");
        Console.WriteLine(constructed);

        // Generate the barcode image and save it as PNG
        string imagePath = Path.Combine(outputDir, "Mailmark2D.png");
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Set the X-dimension (module size) to 4 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode saved to: {imagePath}");
    }
}