// Title: Calculate optimal XDimension for a barcode to match a desired image width
// Description: Demonstrates how to compute the XDimension (module size) that makes a generated barcode fit a specific pixel width, using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to fine‑tune barcode dimensions. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and image measurement via Aspose.Drawing. Developers often need to adjust XDimension to meet layout constraints, ensuring barcodes render at exact sizes for printing or UI integration.
// Prompt: Develop a method to calculate optimal XDimension based on desired image width and barcode symbology specifications.
// Tags: barcode, symbology, xdimension, image width, aspose.barcode, generation, calculation

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example of calculating the optimal XDimension for a barcode
/// so that the generated image matches a target width in pixels.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Sets up sample parameters,
    /// invokes the calculation method, and displays the result.
    /// </summary>
    static void Main()
    {
        // Sample parameters for the barcode generation
        string symbology = "Code128";
        string codeText = "1234567890";
        float desiredWidthPixels = 300f;

        try
        {
            // Calculate the XDimension that will produce the desired image width
            float optimalXDim = CalculateOptimalXDimension(symbology, codeText, desiredWidthPixels);
            Console.WriteLine($"Optimal XDimension (points) for {symbology} with width {desiredWidthPixels}px: {optimalXDim}");
        }
        catch (Exception ex)
        {
            // Output any errors encountered during calculation
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Calculates the XDimension (module size in points) that makes the generated barcode
    /// fit the desired image width (in pixels) as closely as possible.
    /// </summary>
    /// <param name="symbologyName">Name of the symbology (e.g., "Code128")</param>
    /// <param name="codeText">Text to encode</param>
    /// <param name="desiredWidthPixels">Target image width in pixels</param>
    /// <returns>Calculated XDimension in points</returns>
    static float CalculateOptimalXDimension(string symbologyName, string codeText, float desiredWidthPixels)
    {
        // Validate the desired width argument
        if (desiredWidthPixels <= 0)
            throw new ArgumentOutOfRangeException(nameof(desiredWidthPixels), "Desired width must be positive.");

        // Resolve the symbology name to the corresponding EncodeTypes value using reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}", nameof(symbologyName));

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Use a reasonable default XDimension (points) for the initial measurement
        const float defaultXDimension = 2f; // points

        // Generate a barcode with the default XDimension to measure its actual pixel width
        int measuredWidth;
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.Barcode.XDimension.Point = defaultXDimension;

            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                using (var bitmap = new Bitmap(ms))
                {
                    measuredWidth = bitmap.Width;
                }
            }
        }

        // Ensure the generated barcode has a non‑zero width
        if (measuredWidth == 0)
            throw new InvalidOperationException("Generated barcode has zero width.");

        // Compute the scaling factor needed to reach the desired width
        float scaleFactor = desiredWidthPixels / measuredWidth;
        float optimalXDimension = defaultXDimension * scaleFactor;

        // Verify that the calculated XDimension is a positive value
        if (optimalXDimension <= 0)
            throw new InvalidOperationException("Calculated XDimension is non-positive.");

        // Optional verification step (commented out) could regenerate the barcode with the new XDimension
        // using (var verifyGen = new BarcodeGenerator(encodeType, codeText))
        // {
        //     verifyGen.Parameters.Barcode.XDimension.Point = optimalXDimension;
        //     // Additional verification logic could be placed here.
        // }

        return optimalXDimension;
    }
}