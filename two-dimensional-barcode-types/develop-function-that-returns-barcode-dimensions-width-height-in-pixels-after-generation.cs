// Title: Get Barcode Dimensions in Pixels
// Description: Demonstrates how to generate a barcode with Aspose.BarCode and retrieve its pixel width and height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, AutoSizeMode, and image handling to obtain barcode dimensions. Developers working with barcode creation often need to know the exact pixel size for layout or UI purposes; this snippet shows the typical workflow using EncodeTypes, BarCodeImageFormat, and Aspose.Drawing to extract image dimensions.
// Prompt: Develop a function that returns barcode dimensions (width, height) in pixels after generation.
// Tags: barcode, dimensions, generation, aspose.barcode, encode types, png, image, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides functionality to generate a barcode and retrieve its pixel dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Returns the width and height (in pixels) of a generated barcode.
    /// </summary>
    /// <param name="symbologyName">Name of the EncodeTypes field, e.g., "Code128", "QR".</param>
    /// <param name="codeText">Text to encode.</param>
    /// <returns>Tuple containing width and height in pixels.</returns>
    static (int width, int height) GetBarcodeDimensions(string symbologyName, string codeText)
    {
        // Resolve symbology name to BaseEncodeType via reflection.
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}");

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create generator with desired settings.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Let the generator determine size automatically using interpolation.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream for reading.

                // Load the image to obtain pixel dimensions.
                using (var image = Image.FromStream(ms))
                {
                    return (image.Width, image.Height);
                }
            }
        }
    }

    /// <summary>
    /// Demonstrates usage of GetBarcodeDimensions.
    /// </summary>
    static void Main()
    {
        // Sample barcode generation parameters.
        string symbology = "Code128";
        string text = "12345";

        try
        {
            // Retrieve dimensions of the generated barcode.
            var (width, height) = GetBarcodeDimensions(symbology, text);
            Console.WriteLine($"Barcode '{symbology}' with text '{text}' dimensions: {width}px (width) x {height}px (height)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}