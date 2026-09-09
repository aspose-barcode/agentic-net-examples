// Title: Generate Mailmark 4‑State Barcode and Save as PNG
// Description: Demonstrates how to create a Mailmark 4‑state barcode using Aspose.BarCode's ComplexBarcodeGenerator and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of MailmarkCodetext and ComplexBarcodeGenerator classes to produce Mailmark 4‑state symbols, a common requirement for postal and logistics applications. Developers working with advanced barcode symbologies can refer to this pattern for creating, configuring, and exporting complex barcodes in various image formats.
// Prompt: Generate a Mailmark 4‑state barcode with ComplexBarcodeGenerator and save as PNG.
// Tags: mailmark,4-state,barcode,generation,complexbarcodegenerator,png,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Mailmark 4‑state barcode and writes it to a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, saves it, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image.
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full file path for the PNG image.
        string outputPath = Path.Combine(outputDir, "Mailmark4State.png");

        // Build the Mailmark 4‑state codetext with required fields.
        MailmarkCodetext mailmarkCode = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode using ComplexBarcodeGenerator and configure visual parameters.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmarkCode))
        {
            // Set the X‑dimension (module width) to 4 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Mailmark 4‑state barcode saved to: {outputPath}");
    }
}