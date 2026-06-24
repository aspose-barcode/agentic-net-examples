using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a rotated Mailmark barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Mailmark barcode, rotates it 90 degrees, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated barcode image.
        string outputPath = "mailmark_rotated.png";

        // Create a MailmarkCodetext instance and populate its properties.
        // This example uses a 4‑state format with specific identifiers.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state format identifier
            VersionID = 1,                  // Version of the Mailmark specification
            Class = "0",                    // Class as a string (required by the API)
            SupplychainID = 384224,         // Supply chain identifier
            ItemID = 16563762,              // Item identifier
            DestinationPostCodePlusDPS = "EF61AH8T " // Nine‑character postcode + DP suffix
        };

        // Initialize the ComplexBarcodeGenerator with the prepared Mailmark codetext.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set the rotation angle to 90 degrees to rotate the barcode.
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode image in PNG format to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the full path of the saved barcode image to the console.
        Console.WriteLine($"Mailmark barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}