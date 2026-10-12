// Title: Generate MaxiCode barcode with custom quiet zone
// Description: Demonstrates creating a MaxiCode barcode and configuring a custom quiet zone (padding) to satisfy specific scanning requirements.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.MaxiCode. Developers often need to produce MaxiCode symbols for logistics and shipping labels, where precise quiet zone dimensions are critical for reliable scanner performance. The snippet illustrates setting module size, adjusting padding on all sides, and saving the result as a PNG image—common tasks when integrating barcode creation into .NET applications.
/// Prompt: Generate a MaxiCode barcode with a custom quiet zone size to meet specific scanning requirements.
/// Tags: maxicode, barcode, quiet zone, padding, generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a MaxiCode barcode with a custom quiet zone and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, applies custom padding, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCode.png");

        // Initialize the barcode generator for MaxiCode with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Set the module (X) dimension to control the size of individual barcode elements.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Apply a custom quiet zone (padding) of 20 points on all four sides.
            generator.Parameters.Barcode.Padding.Left.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 20f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}