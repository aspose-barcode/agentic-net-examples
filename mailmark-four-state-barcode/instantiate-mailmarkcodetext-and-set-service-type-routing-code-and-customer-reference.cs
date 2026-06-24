using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Mailmark barcode using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a MailmarkCodetext, configures the generator,
    /// and saves the resulting barcode image to a file.
    /// </summary>
    static void Main()
    {
        // Instantiate MailmarkCodetext and set required fields
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4;                 // 4‑state format
        mailmark.VersionID = 1;              // version
        mailmark.Class = "0";                // service type
        mailmark.SupplychainID = 384224;     // routing code
        mailmark.ItemID = 16563762;          // customer reference
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T "; // valid postcode + DPS

        // Generate the Mailmark barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set optional resolution (dots per inch)
            generator.Parameters.Resolution = 300f;

            // Determine output file path in the current directory
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "mailmark.png");

            // Save barcode to a memory stream, then write to file
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // reset stream position before reading
                File.WriteAllBytes(outputPath, ms.ToArray());
            }

            // Inform the user where the file was saved
            Console.WriteLine($"Mailmark barcode saved to {outputPath}");
        }
    }
}