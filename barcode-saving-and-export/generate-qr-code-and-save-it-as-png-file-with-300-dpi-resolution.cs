using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code using Aspose.BarCode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr_code.png";

        // Initialize a BarcodeGenerator for QR code symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the text that will be encoded into the QR code.
            generator.CodeText = "Hello Aspose";

            // Set the image resolution to 300 DPI for higher quality output.
            generator.Parameters.Resolution = 300f;

            // Save the generated QR code as a PNG file at the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the QR code has been saved.
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}