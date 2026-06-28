using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a 4‑state Mailmark barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Mailmark barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "mailmark.png");

        // Create and populate a MailmarkCodetext instance with required data for a 4‑state barcode.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                                 // Use the 4‑state format.
            VersionID = 1,                              // Version identifier.
            Class = "0",                                // Test class identifier.
            SupplychainID = 384224,                     // Supply chain identifier.
            ItemID = 16563762,                          // Item identifier.
            DestinationPostCodePlusDPS = "EF61AH8T "    // Destination postcode plus DP (valid format).
        };

        // Initialize the ComplexBarcodeGenerator with the prepared Mailmark codetext.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Configure the barcode's X-dimension (module width) to 0.5 mm.
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Render and save the barcode image as a PNG file at the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Mailmark barcode saved to: {outputPath}");
    }
}