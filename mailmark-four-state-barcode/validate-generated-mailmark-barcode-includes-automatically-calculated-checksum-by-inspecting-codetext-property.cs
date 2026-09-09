// Title: Validate Mailmark barcode checksum via Codetext property
// Description: This example generates a Mailmark barcode, retrieves its constructed codetext (which includes the automatically calculated checksum), and saves the barcode image.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation for the Mailmark symbology. It uses MailmarkCodetext to define barcode data and ComplexBarcodeGenerator to render the image. Typical scenarios include postal automation and logistics where Mailmark barcodes are required, and developers often need to verify checksum calculation by inspecting the Codetext property.
// Prompt: Validate generated Mailmark barcode includes automatically calculated checksum by inspecting Codetext property.
// Tags: mailmark, barcode, checksum, generation, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Mailmark barcode, displays its constructed codetext
/// (including the automatically calculated checksum), and saves the barcode image to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and outputs relevant information.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the Mailmark data using MailmarkCodetext
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state Mailmark
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // exact required value with trailing space
        };

        // Retrieve the constructed codetext, which includes the automatically calculated checksum
        string constructedCodeText = mailmark.GetConstructedCodetext();
        Console.WriteLine("Constructed Mailmark Codetext (includes checksum):");
        Console.WriteLine(constructedCodeText);

        // Generate the barcode image and save it as PNG
        string imagePath = Path.Combine(tempFolder, "mailmark.png");
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Mailmark barcode image saved to: {imagePath}");
    }
}