using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode and saving it to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code and writes it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.png");

        // Initialize the barcode generator for QR codes.
        // The using statement ensures the generator is disposed after use.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to be encoded in the QR code.
            generator.CodeText = "https://example.com";

            // Save the generated QR code image to the specified path.
            // If the file already exists, it will be overwritten.
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}