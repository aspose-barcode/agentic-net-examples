// Title: Calculate optimal XDimension for a barcode to achieve a target image width
// Description: Demonstrates how to compute the XDimension (module width) that yields a barcode image of a specific pixel width, using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to fine‑tune barcode dimensions with the BarcodeGenerator, EncodeTypes, and AutoSizeMode classes. Developers often need to match barcode size to UI constraints or print layouts, and this pattern shows binary‑search based optimization for XDimension.
// Prompt: Develop a method to calculate optimal XDimension based on desired image width and barcode symbology specifications.
// Tags: barcode symbology, xdimension, image width, aspose.barcode, generation

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that calculates the optimal XDimension for a barcode to meet a desired image width
/// and then generates the barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Demonstrates the calculation and generation workflow.
    /// </summary>
    static void Main()
    {
        // Sample inputs: symbology type, data to encode, and target image width in pixels
        string symbology = "Code128";
        string codeText = "1234567890";
        int desiredWidthPixels = 300;

        try
        {
            // Compute the XDimension that will produce an image close to the desired width
            float optimalXDim = CalculateOptimalXDimension(symbology, codeText, desiredWidthPixels);
            Console.WriteLine($"Optimal XDimension (points) for {symbology} with width {desiredWidthPixels}px: {optimalXDim}");

            // Resolve the EncodeTypes field for the chosen symbology via reflection
            var field = typeof(EncodeTypes).GetField(symbology);
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Generate the final barcode using the calculated XDimension
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.XDimension.Point = optimalXDim;

                // Save the barcode image as PNG in the current directory
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during calculation or generation
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Calculates the XDimension (module width) that yields a barcode image width closest to the desired pixel width.
    /// Uses a binary search over a reasonable XDimension range.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="desiredWidthPixels">Target image width in pixels.</param>
    /// <returns>Optimal XDimension value in points.</returns>
    static float CalculateOptimalXDimension(string symbologyName, string codeText, int desiredWidthPixels)
    {
        // Resolve symbology to BaseEncodeType via reflection
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}");

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Binary search boundaries for XDimension (points)
        float low = 0.5f;
        float high = 10f;
        float best = low;
        const float tolerance = 1f; // Acceptable pixel deviation

        // Perform a fixed number of iterations to converge on an optimal value
        for (int iteration = 0; iteration < 20; iteration++)
        {
            float mid = (low + high) / 2f;

            // Generate a temporary barcode image with the current XDimension
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.XDimension.Point = mid;

                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    int width = bitmap.Width;

                    // If the generated width is within tolerance, return immediately
                    if (Math.Abs(width - desiredWidthPixels) <= tolerance)
                        return mid;

                    // Adjust search range based on whether the image is too narrow or too wide
                    if (width < desiredWidthPixels)
                    {
                        low = mid;
                        best = mid; // Track the best candidate that is not exceeding the target
                    }
                    else
                    {
                        high = mid;
                    }
                }
            }
        }

        // Return the best XDimension found after completing the search
        return best;
    }
}