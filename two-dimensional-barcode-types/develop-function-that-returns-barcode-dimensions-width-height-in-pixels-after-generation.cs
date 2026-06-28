using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a barcode image and retrieve its pixel dimensions using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image for the specified encode type and text, then returns its width and height in pixels.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use (e.g., Code128).</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A tuple containing the image width and height.</returns>
    static (int width, int height) GetBarcodeDimensions(BaseEncodeType encodeType, string codeText)
    {
        // Create a BarcodeGenerator with the requested type and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Disable automatic resizing so the saved image reflects the true pixel size.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the image from the memory stream to access its dimensions.
                using (var bitmap = (Bitmap)Image.FromStream(ms))
                {
                    // Return the width and height of the bitmap.
                    return (bitmap.Width, bitmap.Height);
                }
            }
        }
    }

    /// <summary>
    /// Entry point of the program. Generates a sample barcode and prints its dimensions.
    /// </summary>
    static void Main()
    {
        // Generate dimensions for a Code128 barcode containing "123ABC".
        var dimensions = GetBarcodeDimensions(EncodeTypes.Code128, "123ABC");

        // Output the width and height to the console.
        Console.WriteLine($"Barcode Width: {dimensions.width} px");
        Console.WriteLine($"Barcode Height: {dimensions.height} px");
    }
}