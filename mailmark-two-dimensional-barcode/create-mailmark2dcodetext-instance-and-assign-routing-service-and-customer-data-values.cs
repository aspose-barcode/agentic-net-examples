// Title: Generate Mailmark 2D Barcode with Routing, Service, and Customer Data
// Description: Demonstrates how to create a Mailmark2DCodetext object, assign routing, service, and customer data values, and generate a Mailmark 2D barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of Mailmark2DCodetext, ComplexBarcodeGenerator, and related settings to produce Mailmark 2D symbols, which are commonly used for postal automation and tracking. Developers working with postal barcodes, data matrix encoding, or custom barcode content will find this pattern useful for creating and exporting barcode images.
// Prompt: Create a Mailmark2DCodetext instance and assign routing, service, and customer data values.
// Tags: mailmark2d, barcode, generation, complexbarcode, datamatrix, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that builds a Mailmark 2D barcode by populating routing, service,
/// and customer fields, then saves the resulting image to a temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Constructs the Mailmark2DCodetext, configures the generator,
    /// and writes the barcode image to disk.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Initialize Mailmark2D codetext with required routing, service, and customer information.
        var mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            RTSFlag = "0",
            SupplyChainID = 384224,
            ItemID = 16563762,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            ReturnToSenderPostCode = " QWE2 ",
            CustomerContent = "CUST",
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40,
            DataMatrixType = Mailmark2DType.Type_7
        };

        // Define the output file path in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "Mailmark2D.png");

        // Create the barcode generator, apply visual settings, and save the image.
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Set the X‑dimension (module size) to 4 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was written.
        Console.WriteLine($"Mailmark 2D barcode generated at: {outputPath}");
    }
}