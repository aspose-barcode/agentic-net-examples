using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Mailmark barcode and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Mailmark barcode and writes it to the output folder.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "output");

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Build the full file path for the resulting JPEG image
        string outputPath = Path.Combine(outputFolder, "mailmark.jpg");

        // Configure the Mailmark codetext with the required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state Mailmark format
            VersionID = 1,                  // Version identifier
            Class = "0",                    // Null/Test class
            SupplychainID = 384224,         // Supply chain identifier
            ItemID = 16563762,              // Item identifier
            DestinationPostCodePlusDPS = "EF61AH8T " // Valid postcode plus DPS
        };

        // Generate the barcode using the configured Mailmark codetext
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Save the generated barcode as a JPEG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Mailmark barcode saved to: {outputPath}");
    }
}