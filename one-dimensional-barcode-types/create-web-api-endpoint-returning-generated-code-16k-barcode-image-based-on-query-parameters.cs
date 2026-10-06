// Title: Generate Code 16K barcode image and save to file
// Description: Demonstrates creating a Code 16K barcode using Aspose.BarCode, configuring dimensions and quiet zones, and saving the PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.Code16K. It shows setting XDimension, aspect ratio, and quiet zone coefficients—common tasks when customizing barcode appearance for printing or display. Developers often need such samples to quickly integrate barcode creation into web APIs or desktop apps.
// Prompt: Create web API endpoint returning generated Code 16K barcode image based on query parameters.
// Tags: code16k, barcode, generation, png, aspose.barcode, xdimension, aspectratio, quietzone

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code 16K barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Simulates request parameters, generates the barcode, and writes the PNG file.
    /// </summary>
    static void Main()
    {
        // Simulated request parameters that would normally come from a web API query string
        string codeText = "Aspose.Barcode";
        int aspectRatio = 10;          // Recommended value > 8 for proper height
        int quietZoneLeft = 10;        // Minimum quiet zone on the left side (>= 10)
        int quietZoneRight = 10;       // Minimum quiet zone on the right side (>= 1)
        float xDimensionPixels = 2f;   // Module size in pixels

        try
        {
            // Generate the barcode image as a byte array
            byte[] imageBytes = GenerateCode16K(codeText, aspectRatio, quietZoneLeft, quietZoneRight, xDimensionPixels);

            // Determine output path in the current working directory
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Code16K.png");

            // Write the PNG image to disk
            File.WriteAllBytes(outputPath, imageBytes);

            Console.WriteLine($"Code 16K barcode generated and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during generation or file I/O
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a Code 16K barcode image with specified parameters and returns it as a PNG byte array.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="aspectRatio">Height aspect ratio (recommended > 8).</param>
    /// <param name="quietLeftCoef">Quiet zone coefficient for the left side (minimum 10).</param>
    /// <param name="quietRightCoef">Quiet zone coefficient for the right side (minimum 1).</param>
    /// <param name="xDimensionPixels">Module size in pixels (must be positive).</param>
    /// <returns>Byte array containing the PNG image of the generated barcode.</returns>
    static byte[] GenerateCode16K(string codeText, int aspectRatio, int quietLeftCoef, int quietRightCoef, float xDimensionPixels)
    {
        // Validate input arguments
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text must be provided.", nameof(codeText));
        if (aspectRatio <= 0)
            throw new ArgumentOutOfRangeException(nameof(aspectRatio), "Aspect ratio must be positive.");
        if (quietLeftCoef < 10)
            throw new ArgumentOutOfRangeException(nameof(quietLeftCoef), "QuietZoneLeftCoef must be >= 10.");
        if (quietRightCoef < 1)
            throw new ArgumentOutOfRangeException(nameof(quietRightCoef), "QuietZoneRightCoef must be >= 1.");
        if (xDimensionPixels <= 0f)
            throw new ArgumentOutOfRangeException(nameof(xDimensionPixels), "XDimension must be positive.");

        byte[] result;

        // Initialize the barcode generator with Code16K symbology and the provided text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set the module size (pixel dimension of a single barcode element)
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;

            // Configure the aspect ratio (controls barcode height)
            generator.Parameters.Barcode.Code16K.AspectRatio = aspectRatio;

            // Apply quiet zone coefficients to control margins on each side
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = quietLeftCoef;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = quietRightCoef;

            // Save the generated barcode to a memory stream in PNG format
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                result = ms.ToArray(); // Convert stream to byte array
            }
        }

        return result;
    }
}