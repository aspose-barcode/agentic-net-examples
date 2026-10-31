// Title: Generate Mailmark 4‑state barcode using ComplexBarcodeGenerator
// Description: Demonstrates creating a Mailmark 4‑state barcode with Aspose.BarCode's ComplexBarcodeGenerator and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating how to use the MailmarkCodetext class with ComplexBarcodeGenerator to produce Mailmark 4‑state symbology. Typical use cases include encoding postal information for mail sorting and tracking. Developers often need to configure barcode parameters such as X‑dimension and output format, then save the image using BarCodeImageFormat.
// Prompt: Generate a Mailmark 4‑state barcode with ComplexBarcodeGenerator and save as PNG.
// Tags: mailmark, 4-state, complexbarcode, generation, png, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Mailmark 4‑state barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures parameters, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Mailmark4State.png");

        // Define the Mailmark codetext with required fields for a 4‑state barcode
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Initialize the complex barcode generator using the Mailmark codetext
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set the X‑dimension (module width) of the barcode in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Mailmark 4-state barcode saved to {outputPath}");
    }
}