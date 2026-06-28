using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a rotated Mailmark barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Mailmark barcode, rotates it, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare a Mailmark codetext (required for ComplexBarcodeGenerator)
        var mailmark = new MailmarkCodetext
        {
            Format = 4,               // 4 – unspecified/default
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Define output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "mailmark_rotated.png");

        // Create the barcode generator with the Mailmark codetext
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set rotation angle (in degrees) – 45° clockwise
            generator.Parameters.RotationAngle = 45f;

            // Save the generated barcode image as PNG to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Rotated barcode saved to: {outputPath}");
    }
}