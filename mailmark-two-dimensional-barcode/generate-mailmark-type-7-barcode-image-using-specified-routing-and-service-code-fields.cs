// Title: Generate Mailmark Type 7 Barcode Image
// Description: Creates a Mailmark Type 7 2‑D barcode image with specified routing and service code fields and saves it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It demonstrates how to use the Mailmark2DCodetext class together with ComplexBarcodeGenerator to produce Mailmark Type 7 barcodes, a common requirement for postal routing and service code encoding. Developers working with postal symbologies often need to configure fields such as UPU country ID, supply‑chain ID, and customer content, then render the barcode to an image format.
// Prompt: Generate a Mailmark type 7 barcode image using specified routing and service code fields.
// Tags: mailmark, type7, barcode generation, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a Mailmark Type 7 barcode and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the Mailmark2DCodetext, configures the generator,
    /// and writes the resulting barcode image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "MailmarkType7.png");

        // Initialize Mailmark 2D codetext with required fields for Type 7.
        Mailmark2DCodetext mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 384224,
            ItemID = 16563762,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            CustomerContent = "ABC123", // max 6 characters for Type 7
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40,
            DataMatrixType = Mailmark2DType.Type_7
        };

        // Create the barcode generator with the Mailmark codetext.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Set the X-dimension (module size) to 4 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Mailmark Type 7 barcode saved to: {outputPath}");
    }
}