// Title: Generate a Mailmark 4-state barcode using Aspose.BarCode
// Description: Demonstrates how to create a Mailmark barcode by configuring service type, routing code, and customer reference, then saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Mailmark (4‑state) symbology. It shows how to use the MailmarkCodetext class together with ComplexBarcodeGenerator to set key fields such as service type, routing code, and customer reference, which are common requirements for postal and logistics applications. Developers can adapt this pattern for generating other complex barcodes like QR, DataMatrix, or custom symbologies.
// Prompt: Instantiate a MailmarkCodetext and set service type, routing code, and customer reference.
// Tags: mailmark, barcode, complexbarcode, generation, png, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creation of a Mailmark 4‑state barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds the MailmarkCodetext, generates the barcode, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Instantiate MailmarkCodetext and configure its properties
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4;                     // Set Mailmark format (4‑state)
        mailmark.VersionID = 1;                  // Set version identifier
        mailmark.Class = "0";                    // Service type
        mailmark.SupplychainID = 384224;         // Routing code
        mailmark.ItemID = 16563762;              // Customer reference
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T "; // Destination postcode plus DPS

        // Create a generator using the configured MailmarkCodetext
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Adjust barcode appearance: set X dimension in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Determine output file path
            string outputPath = Path.Combine(Environment.CurrentDirectory, "Mailmark4State.png");

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
    }
}