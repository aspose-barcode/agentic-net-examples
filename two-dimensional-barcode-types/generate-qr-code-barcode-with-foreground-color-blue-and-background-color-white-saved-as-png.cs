// Title: Generate QR Code with custom colors and save as PNG
// Description: Demonstrates creating a QR Code barcode, setting its foreground to blue and background to white, and saving the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with QR Code symbology. It shows configuring visual properties such as BarColor and BackColor, and exporting the result in PNG format. Developers working with barcode creation for web, mobile, or print can use these techniques to customize appearance and output format.
// Prompt: Generate a QR Code barcode with foreground color blue and background color white, saved as PNG.
// Tags: qr code, barcode generation, color customization, png output, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode with custom colors and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a QR Code, applies color settings, and writes the image to disk.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file name.
        string outputFile = "qr_blue_white.png";

        // Initialize the barcode generator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set the foreground (barcode) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to white.
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG image.
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"QR code saved to: {Path.GetFullPath(outputFile)}");
    }
}