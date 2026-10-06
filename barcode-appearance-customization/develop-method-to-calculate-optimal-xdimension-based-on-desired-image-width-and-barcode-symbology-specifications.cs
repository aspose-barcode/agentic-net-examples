// Title: Calculate optimal XDimension for a barcode to achieve a specific image width
// Description: Demonstrates how to compute the XDimension value that scales a barcode image to a desired pixel width using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control barcode size via the XDimension property. It shows usage of BarcodeGenerator, AutoSizeMode, and image generation to fine‑tune dimensions, a common need when integrating barcodes into fixed‑size layouts or print media. Developers often need to calculate optimal dimensions for various symbologies to meet layout constraints.
// Prompt: Develop a method to calculate optimal XDimension based on desired image width and barcode symbology specifications.
// Tags: barcode symbology, xdimension calculation, aspose.barcode, image size, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates calculating and applying an optimal XDimension for a barcode to match a target image width.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode with XDimension adjusted to achieve the desired width.
    /// </summary>
    static void Main()
    {
        // Sample inputs: symbology, data to encode, and target width in pixels.
        string symbologyName = "Code128";
        string codeText = "1234567890";
        int desiredWidthPixels = 500;

        try
        {
            // Resolve the symbology name to an EncodeType enum value.
            BaseEncodeType encodeType = GetEncodeType(symbologyName);

            // Compute the optimal XDimension that will produce the desired image width.
            float optimalX = CalculateOptimalXDimension(encodeType, codeText, desiredWidthPixels);
            Console.WriteLine($"Optimal XDimension (pixels): {optimalX}");

            // Generate the barcode using the calculated XDimension.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Disable automatic sizing; we will control size via XDimension.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.XDimension.Pixels = optimalX;

                // Save the generated barcode image to a PNG file.
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_optimal.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves the BaseEncodeType corresponding to a given symbology name.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "Code128").</param>
    /// <returns>The matching BaseEncodeType value.</returns>
    static BaseEncodeType GetEncodeType(string symbologyName)
    {
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}");
        return (BaseEncodeType)field.GetValue(null);
    }

    /// <summary>
    /// Calculates the XDimension (in pixels) required to generate a barcode image with the specified width.
    /// </summary>
    /// <param name="encodeType">The barcode symbology type.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="desiredWidthPixels">The target image width in pixels.</param>
    /// <returns>The optimal XDimension value.</returns>
    static float CalculateOptimalXDimension(BaseEncodeType encodeType, string codeText, int desiredWidthPixels)
    {
        // Generate a temporary barcode to measure its current width.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                int currentWidth = bitmap.Width;
                if (currentWidth == 0)
                    throw new InvalidOperationException("Generated barcode width is zero.");

                // Scale the current XDimension proportionally to reach the desired width.
                float currentX = generator.Parameters.Barcode.XDimension.Pixels;
                float newX = currentX * desiredWidthPixels / currentWidth;
                return newX;
            }
        }
    }
}