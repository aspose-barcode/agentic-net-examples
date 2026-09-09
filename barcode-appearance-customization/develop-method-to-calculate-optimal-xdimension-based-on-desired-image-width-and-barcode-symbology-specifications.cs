// Title: Calculate optimal XDimension for barcode generation
// Description: Demonstrates how to compute the XDimension value that yields a barcode image of a desired pixel width using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to work with EncodeTypes, AutoSizeMode, and XDimension settings. Developers often need to adjust barcode dimensions to fit specific layout constraints, such as fixed image widths for UI elements or printed labels. The code illustrates retrieving symbology types via reflection, measuring generated image size, and scaling XDimension accordingly.
// Prompt: Develop a method to calculate optimal XDimension based on desired image width and barcode symbology specifications.
// Tags: barcode symbology, calculation, xdimension, aspose.barcode, image generation, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that calculates the optimal XDimension for a barcode to match a target image width.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Calculates optimal XDimension, generates barcode, and saves PNG file.
    /// </summary>
    static void Main()
    {
        // Define barcode parameters
        string symbology = "Code128";
        string codeText = "1234567890";
        int desiredWidthPixels = 300;

        // Compute the optimal XDimension based on the desired width
        float optimalXDim = CalculateOptimalXDimension(symbology, codeText, desiredWidthPixels);
        Console.WriteLine($"Optimal XDimension (pixels): {optimalXDim}");

        // Prepare output file path
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_optimal.png");

        // Resolve the EncodeTypes field for the requested symbology
        var field = typeof(EncodeTypes).GetField(symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbology}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Generate the barcode with the calculated XDimension and save it as PNG
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.XDimension.Pixels = optimalXDim;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to {outputPath}");
    }

    /// <summary>
    /// Calculates the XDimension (in pixels) that will produce a barcode image whose width matches the desired pixel width.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="desiredWidthPixels">Target image width in pixels.</param>
    /// <returns>Optimal XDimension value in pixels.</returns>
    static float CalculateOptimalXDimension(string symbologyName, string codeText, int desiredWidthPixels)
    {
        // Retrieve the EncodeTypes field corresponding to the symbology name
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}");

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
        const float initialXDim = 1f; // Start with a base XDimension of 1 pixel

        // Generate a temporary barcode image to measure its width
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.XDimension.Pixels = initialXDim;

            using (var bitmap = generator.GenerateBarCodeImage())
            {
                int currentWidth = bitmap.Width;
                if (currentWidth == 0)
                    throw new InvalidOperationException("Generated image width is zero.");

                // Scale the initial XDimension proportionally to reach the desired width
                float scale = (float)desiredWidthPixels / currentWidth;
                return initialXDim * scale;
            }
        }
    }
}