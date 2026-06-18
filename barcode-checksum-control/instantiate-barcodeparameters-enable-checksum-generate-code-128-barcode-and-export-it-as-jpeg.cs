using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode and saves it as a JPEG file.
    /// </summary>
    static void Main()
    {
        // Define the output file name and path for the generated barcode image.
        string outputPath = "code128.jpg";

        // Initialize a BarcodeGenerator for the Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Assign the data to be encoded in the barcode.
            generator.CodeText = "1234567890";

            // Activate checksum calculation for the barcode (required for some scanners).
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Persist the generated barcode to a JPEG file at the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}