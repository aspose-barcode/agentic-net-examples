using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a Mailmark barcode using Aspose.BarCode and outputs it as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Creates a Mailmark codetext, generates the barcode, and writes the image bytes as Base64 to the console.
    /// </summary>
    static void Main()
    {
        // Initialize Mailmark codetext with sample data required for a valid barcode
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state format identifier
            VersionID = 1,                  // Version identifier
            Class = "0",                    // Class as a string (required by the API)
            SupplychainID = 384224,         // Supply chain identifier
            ItemID = 16563762,              // Item identifier
            DestinationPostCodePlusDPS = "EF61AH8T " // 9‑character postcode plus DP suffix
        };

        // Use ComplexBarcodeGenerator to create the barcode image in memory
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // MemoryStream holds the generated PNG image data
            using (var ms = new MemoryStream())
            {
                // Save the barcode to the stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the stream contents to a byte array
                byte[] barcodeBytes = ms.ToArray();

                // Output the image bytes as a Base64 string (simulating an API response)
                Console.WriteLine(Convert.ToBase64String(barcodeBytes));
            }
        }
    }
}