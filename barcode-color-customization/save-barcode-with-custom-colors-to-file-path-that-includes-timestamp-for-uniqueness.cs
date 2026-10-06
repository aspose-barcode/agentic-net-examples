// Title: Save barcode with custom colors to a timestamped PNG file
// Description: This example generates a Code128 barcode, applies custom foreground (blue) and background (yellow) colors, and saves the image as a PNG file with a unique name that includes a timestamp.
// Category-Description: Demonstrates Aspose.BarCode generation features such as setting barcode symbology, customizing colors, and exporting images. The key API classes used are BarcodeGenerator, EncodeTypes, and BarCodeImageFormat. Typical scenarios include branding, UI integration, and batch processing where distinct file names are required. Developers often need to customize appearance and ensure file uniqueness in automated workflows.
// Prompt: Save a barcode with custom colors to a file path that includes a timestamp for uniqueness.
// Tags: barcode, code128, custom-colors, png, timestamp, aspose.barcode, image-generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Entry point for the barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with custom colors and saves it to a uniquely named PNG file.
    /// </summary>
    static void Main()
    {
        // Define the text to encode and the barcode symbology
        string codeText = "12345678";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Build a unique file name using the current timestamp
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
        string fileName = $"barcode_{timestamp}.png";
        string outputPath = Path.Combine(Environment.CurrentDirectory, fileName);

        // Create the barcode generator with the specified symbology and content
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Apply custom colors: blue bars on a yellow background
            generator.Parameters.Barcode.BarColor = Color.Blue;   // foreground (bars) color
            generator.Parameters.BackColor = Color.Yellow;       // background color

            // Save the generated barcode image to the file system as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}