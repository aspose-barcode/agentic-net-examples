// Title: Barcode XDimension Optimizer
// Description: Demonstrates calculating the optimal XDimension for a barcode to match a target image width.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and AutoSizeMode to control module size. Developers often need to fit barcodes into predefined layouts, requiring precise dimension calculations. The snippet shows measuring generated image size and adjusting XDimension accordingly.
// Prompt: Develop a method to calculate optimal XDimension based on desired image width and barcode symbology specifications.
// Tags: barcode symbology, xdimension calculation, image width, aspose.barcode, generation

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeXDimensionCalculator
{
    /// <summary>
    /// Provides an example of calculating the optimal XDimension (module size) for a barcode
    /// so that the generated image matches a desired width.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Sets up sample data, invokes the calculation,
        /// and outputs the resulting XDimension.
        /// </summary>
        static void Main()
        {
            // Sample inputs
            string symbologyName = "Code128";
            string codeText = "1234567890";
            float desiredWidth = 300f; // Desired image width in pixels

            try
            {
                // Calculate the optimal XDimension based on inputs
                float optimalX = CalculateOptimalXDimension(symbologyName, codeText, desiredWidth);
                Console.WriteLine($"Optimal XDimension for {symbologyName} with width {desiredWidth} px: {optimalX} pt");
            }
            catch (Exception ex)
            {
                // Output any errors that occur during calculation
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Calculates the optimal XDimension (module size) so that the generated barcode image
        /// matches the desired width as closely as possible.
        /// </summary>
        /// <param name="symbologyName">Name of the barcode symbology (e.g., "Code128").</param>
        /// <param name="codeText">Text to encode.</param>
        /// <param name="desiredWidth">Desired image width in pixels.</param>
        /// <returns>Calculated XDimension in points.</returns>
        static float CalculateOptimalXDimension(string symbologyName, string codeText, float desiredWidth)
        {
            // Validate symbology name
            if (string.IsNullOrWhiteSpace(symbologyName))
                throw new ArgumentException("Symbology name must be provided.", nameof(symbologyName));

            // Validate desired width
            if (desiredWidth <= 0f)
                throw new ArgumentOutOfRangeException(nameof(desiredWidth), "Desired width must be greater than zero.");

            // Resolve the symbology name to a BaseEncodeType using reflection.
            FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
                throw new ArgumentException($"Unknown symbology: {symbologyName}", nameof(symbologyName));

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create a barcode generator with a default XDimension.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Disable automatic sizing to work with explicit XDimension.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Set an initial XDimension (points). This value will be adjusted.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Generate the barcode image to measure its current width.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    int actualWidth = bitmap.Width;
                    if (actualWidth == 0)
                        throw new InvalidOperationException("Generated barcode has zero width.");

                    // Compute scaling factor needed to reach the desired width.
                    float scale = desiredWidth / actualWidth;

                    // Calculate the optimal XDimension based on the scale.
                    float optimalX = generator.Parameters.Barcode.XDimension.Point * scale;

                    // Apply the optimal XDimension back to the generator.
                    generator.Parameters.Barcode.XDimension.Point = optimalX;

                    // Optional: regenerate and save the barcode to verify the size.
                    // using (var output = new FileStream("optimal_barcode.png", FileMode.Create, FileAccess.Write))
                    // {
                    //     generator.Save(output, BarCodeImageFormat.Png);
                    // }

                    return optimalX;
                }
            }
        }
    }
}