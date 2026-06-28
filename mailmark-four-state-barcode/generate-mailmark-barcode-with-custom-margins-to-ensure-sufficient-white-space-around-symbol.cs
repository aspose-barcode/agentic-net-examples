using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demo program that generates a Mailmark barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Mailmark barcode image and saves it to the current directory.
    /// </summary>
    static void Main()
    {
        // Create a MailmarkCodetext instance with the required fields.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,               // 4-state format
            VersionID = 1,
            Class = "0",              // string property
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing spaces are part of the format
        };

        // Build the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "mailmark.png");

        // Use ComplexBarcodeGenerator to create the barcode with custom padding.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set padding on all sides (in points) to add white space around the barcode.
            generator.Parameters.Barcode.Padding.Left.Point = 30f;
            generator.Parameters.Barcode.Padding.Top.Point = 30f;
            generator.Parameters.Barcode.Padding.Right.Point = 30f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 30f;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Mailmark barcode saved to: {outputPath}");
    }
}