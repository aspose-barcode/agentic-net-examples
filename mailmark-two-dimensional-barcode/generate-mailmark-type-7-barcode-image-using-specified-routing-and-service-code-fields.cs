// Title: Generate Mailmark Type 7 Barcode Image
// Description: Creates a Mailmark Type 7 barcode with routing and service code fields and saves it as a PNG file.
// Category-Description: This example demonstrates how to use Aspose.BarCode's ComplexBarcodeGenerator to produce Mailmark 2D barcodes. It showcases the Mailmark2DCodetext class for configuring routing and service code data, and the BarCodeImageFormat enum for output. Developers building postal or logistics solutions often need to generate Mailmark barcodes for tracking and routing, making this pattern a common requirement.
// Prompt: Generate a Mailmark type 7 barcode image using specified routing and service code fields.
// Tags: mailmark, type7, barcode generation, png, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a Mailmark Type 7 barcode and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the Mailmark 2D codetext, generates the barcode, and writes the image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "MailmarkType7.png");

        // Configure the Mailmark 2D codetext with routing and service code fields.
        Mailmark2DCodetext mailmark2D = new Mailmark2DCodetext
        {
            // Routing fields (example values)
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            // Service code fields (example values)
            SupplyChainID = 384224,
            ItemID = 16563762,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            // Specify that this is a Mailmark Type 7 barcode.
            DataMatrixType = Mailmark2DType.Type_7,
            CustomerContent = "CUSTOM"
        };

        // Generate the barcode image using the ComplexBarcodeGenerator.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Set the X-dimension (module size) to 4 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Mailmark Type 7 barcode saved to: {outputPath}");
    }
}