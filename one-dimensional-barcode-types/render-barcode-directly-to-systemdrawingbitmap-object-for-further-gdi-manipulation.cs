using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, performing a simple pixel manipulation,
/// and saving the result as a PNG file using Aspose.BarCode and Aspose.Drawing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, modifies a pixel, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Generate the barcode image as a Bitmap object
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Simple GDI+ manipulation: set the top-left pixel (0,0) to red
                bitmap.SetPixel(0, 0, Color.Red);

                // Determine the full path for the output PNG file in the current directory
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

                // Save the manipulated bitmap to the specified path using PNG format
                bitmap.Save(outputPath, ImageFormat.Png);

                // Inform the user where the file was saved
                Console.WriteLine($"Barcode image saved to: {outputPath}");
            }
        }
    }
}