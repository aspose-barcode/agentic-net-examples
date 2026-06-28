using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code with custom foreground and background colors
/// using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code image with green bars on a black background and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Define the output file name and path for the generated QR code image.
        string outputPath = "qr_green_on_black.png";

        // Initialize a BarcodeGenerator for QR encoding with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Configure the barcode appearance:
            // Set the color of the QR code modules (bars) to green.
            generator.Parameters.Barcode.BarColor = Color.Green;

            // Set the background color of the image to black.
            generator.Parameters.BackColor = Color.Black;

            // Save the generated QR code as a PNG file to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user that the QR code has been saved successfully.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}