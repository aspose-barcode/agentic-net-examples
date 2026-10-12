// Title: Retrieve Barcode Pixel Dimensions Based on X-Dimension and Resolution
// Description: Demonstrates generating a barcode with a specific X‑dimension (in millimeters) and DPI resolution, then obtaining its actual pixel width and height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as X‑dimension and resolution using the BarcodeGenerator class. Developers often need to know the exact pixel size of a rendered barcode for layout, printing, or image processing tasks. The code shows reflection‑based symbology resolution, parameter setting, and bitmap extraction—common steps in barcode creation workflows.
/// Prompt: Implement method to retrieve actual pixel dimensions of generated barcode based on unit and resolution.
/// Tags: barcode, datamatrix, dimensions, resolution, pixel, aspose.barcode, generation

using System;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an example of calculating the pixel dimensions of a generated barcode
/// based on the specified X‑dimension (in millimeters) and image resolution (DPI).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Sets sample parameters, invokes the dimension
    /// calculation method, and writes the results to the console.
    /// </summary>
    static void Main()
    {
        // Sample parameters
        string symbology = "DataMatrix";
        string codeText = "ASPOSE";
        float xDimensionMillimeters = 1f;
        float resolutionDpi = 300f;

        // Retrieve pixel dimensions for the configured barcode
        var size = GetBarcodePixelDimensions(symbology, codeText, xDimensionMillimeters, resolutionDpi);

        // Output the results
        Console.WriteLine($"Barcode '{symbology}' with text '{codeText}':");
        Console.WriteLine($"Pixel Width = {size.width}, Pixel Height = {size.height}");
    }

    /// <summary>
    /// Generates a barcode using the specified symbology, text, X‑dimension, and resolution,
    /// then returns its width and height in pixels.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "DataMatrix").</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    /// <param name="xDimMillimeters">Desired X‑dimension in millimeters.</param>
    /// <param name="resolutionDpi">Image resolution in dots per inch.</param>
    /// <returns>A tuple containing the bitmap width and height in pixels.</returns>
    static (int width, int height) GetBarcodePixelDimensions(string symbologyName, string codeText, float xDimMillimeters, float resolutionDpi)
    {
        // Resolve symbology name to BaseEncodeType via reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return (0, 0);
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Initialize the barcode generator with the resolved type and text
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set the X‑dimension (module size) in millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = xDimMillimeters;

            // Set the image resolution (DPI)
            generator.Parameters.Resolution = resolutionDpi;

            // Generate the barcode image and retrieve its pixel dimensions
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                return (bitmap.Width, bitmap.Height);
            }
        }
    }
}