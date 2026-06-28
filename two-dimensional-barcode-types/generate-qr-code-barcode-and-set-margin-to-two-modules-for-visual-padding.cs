using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code image using the Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code with sample text,
    /// configures its dimensions and padding, saves it to a file, and writes
    /// the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the file path where the QR Code image will be saved.
        string outputPath = "qr.png";

        // Initialize a BarcodeGenerator for QR encoding with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the size of a single QR module (XDimension) to 2 points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Calculate padding equal to two modules on each side.
            float padding = 2f * generator.Parameters.Barcode.XDimension.Point; // two modules

            // Apply the calculated padding to all four sides of the barcode.
            generator.Parameters.Barcode.Padding.Left.Point   = padding;
            generator.Parameters.Barcode.Padding.Top.Point    = padding;
            generator.Parameters.Barcode.Padding.Right.Point  = padding;
            generator.Parameters.Barcode.Padding.Bottom.Point = padding;

            // Save the generated QR Code image to the specified file.
            generator.Save(outputPath);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to {outputPath}");
    }
}