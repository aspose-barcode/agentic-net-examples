using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a Mailmark barcode using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Mailmark barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Prepare Mailmark data (valid sample)
        var mailmark = new MailmarkCodetext
        {
            // 4‑state format identifier
            Format = 4,
            // Version identifier
            VersionID = 1,
            // Test class identifier
            Class = "0",
            // Supply chain identifier
            SupplychainID = 384224,
            // Item identifier
            ItemID = 16563762,
            // Destination postcode plus DP suffix (trailing space is intentional)
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Create a generator for the Mailmark barcode using the prepared data
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Reduce the module size (XDimension) for a tighter barcode appearance
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Enable auto‑size mode to automatically fit the barcode within the target dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Define the target image dimensions (in points)
            generator.Parameters.ImageWidth.Point = 200f;   // Desired image width
            generator.Parameters.ImageHeight.Point = 100f;  // Desired image height

            // Save the generated barcode as a PNG file
            string outputPath = "mailmark.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Mailmark barcode saved to {outputPath}");
        }
    }
}