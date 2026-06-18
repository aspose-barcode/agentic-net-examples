using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to calculate the optimal XDimension for a barcode
/// so that the generated image meets a desired pixel width, and then
/// creates and saves the barcode using that XDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Calculates the smallest XDimension (in points) that yields an image width
    /// greater than or equal to the desired pixel width for the given symbology.
    /// </summary>
    /// <param name="symbology">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="targetPixelWidth">The minimum width (in pixels) required for the generated image.</param>
    /// <returns>The optimal XDimension value (in points).</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="targetPixelWidth"/> is not positive.</exception>
    static float CalculateOptimalXDimension(BaseEncodeType symbology, string codeText, int targetPixelWidth)
    {
        // Validate input.
        if (targetPixelWidth <= 0)
            throw new ArgumentOutOfRangeException(nameof(targetPixelWidth), "Target width must be positive.");

        // Start with a small XDimension and increase until the generated image meets the width.
        float xDimension = 0.5f; // points
        const float maxXDimension = 10f; // safety upper bound
        const float step = 0.5f;        // increment step

        while (xDimension <= maxXDimension)
        {
            // Create a barcode generator for the current XDimension.
            using (var generator = new BarcodeGenerator(symbology, codeText))
            {
                // Disable automatic sizing so XDimension directly controls bar width.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Set a reasonable bar height (points) for 1D barcodes.
                generator.Parameters.Barcode.BarHeight.Point = 30f;

                // Keep the default resolution (96 dpi) unchanged.
                generator.Parameters.Resolution = 96f;

                // Apply the current XDimension.
                generator.Parameters.Barcode.XDimension.Point = xDimension;

                // Generate the barcode image and check its width.
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    int actualWidth = bitmap.Width; // pixels
                    if (actualWidth >= targetPixelWidth)
                        return xDimension; // Desired width reached.
                }
            }

            // Increase XDimension and try again.
            xDimension += step;
        }

        // If no XDimension satisfied the requirement, return the maximum tried value.
        return maxXDimension;
    }

    /// <summary>
    /// Entry point of the program. Calculates the optimal XDimension for a Code128 barcode,
    /// generates the barcode using that XDimension, and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Example: Code128 barcode, desired image width 300 pixels.
        BaseEncodeType symbology = EncodeTypes.Code128;
        string codeText = "1234567890";
        int desiredWidthPixels = 300;

        // Determine the optimal XDimension for the desired width.
        float optimalXDim = CalculateOptimalXDimension(symbology, codeText, desiredWidthPixels);
        Console.WriteLine($"Optimal XDimension (points): {optimalXDim}");

        // Generate the barcode using the calculated XDimension and save it.
        using (var generator = new BarcodeGenerator(symbology, codeText))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.BarHeight.Point = 30f;
            generator.Parameters.Resolution = 96f;
            generator.Parameters.Barcode.XDimension.Point = optimalXDim;

            string outputPath = "optimal_code128.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
    }
}