// Title: Generate Mailmark Type 7 Barcode Image
// Description: Demonstrates how to create a Mailmark Type 7 2‑D barcode using Aspose.BarCode and save it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating the use of Mailmark2DCodetext and ComplexBarcodeGenerator classes. Developers commonly generate Mailmark barcodes for postal routing, embedding routing and service codes, and need to configure codetext fields and image parameters. The snippet shows typical steps: preparing output folder, configuring codetext, setting dimensions, and saving the image.
// Prompt: Generate a Mailmark type 7 barcode image using specified routing and service code fields.
// Tags: mailmark, type7, barcode, generation, png, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Mailmark Type 7 barcode and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the Mailmark codetext, generates the barcode, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare the output directory and file name
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "MailmarkType7.png");

        // --------------------------------------------------------------------
        // Define the Mailmark 2D codetext with required routing and service fields
        // --------------------------------------------------------------------
        Mailmark2DCodetext mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 1234,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            ReturnToSenderPostCode = " QWE2 ",
            CustomerContent = "CUSTOM",
            DataMatrixType = Mailmark2DType.Type_7
        };

        // --------------------------------------------------------------------
        // Generate the barcode image using ComplexBarcodeGenerator
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Set the X-dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Mailmark Type 7 barcode saved to: {outputPath}");
    }
}