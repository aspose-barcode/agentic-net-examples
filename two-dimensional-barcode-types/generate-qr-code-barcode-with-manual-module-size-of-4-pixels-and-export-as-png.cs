// Title: Generate QR Code with Custom Module Size and Save as PNG
// Description: This example creates a QR Code barcode with a manually set module size of 4 pixels and saves it as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode generation of QR Code symbology using the BarcodeGenerator class. It shows how to configure barcode parameters such as XDimension (module size) and export the result in PNG format. Developers working with barcode creation, custom sizing, and image output will find this pattern useful for integrating QR Code generation into .NET applications.
// Prompt: Generate a QR Code barcode with manual module size of 4 pixels and export as PNG.
// Tags: qr code, barcode generation, png, manual module size, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code with a custom module size and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code barcode, sets a 4‑pixel module size, and writes the image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "QrCode.png");

        // Initialize the barcode generator for QR Code symbology with the desired text.
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "Aspose"))
        {
            // Set the module (X) dimension to 4 pixels for manual sizing.
            gen.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG image to the specified path.
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the PNG file has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}