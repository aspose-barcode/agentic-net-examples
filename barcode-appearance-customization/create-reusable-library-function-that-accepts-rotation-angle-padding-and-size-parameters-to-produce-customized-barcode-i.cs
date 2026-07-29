// Title: Generate Custom Rotated Barcode with Padding and Size
// Description: Demonstrates creating a barcode image with a specified rotation angle, uniform padding, and custom dimensions, then saving it as PNG.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to use the BarcodeGenerator class together with its Parameters property to control rotation, padding, and image size. Developers often need to produce barcodes that fit specific layout constraints, such as rotated labels or fixed-size graphics, and this snippet shows the typical API usage for those scenarios.
// Prompt: Create a reusable library function that accepts rotation angle, padding, and size parameters to produce customized barcode images.
// Tags: barcode symbology, image generation, rotation, padding, size, aspose.barcode, png, c#

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeDemo
{
    /// <summary>
    /// Demonstrates generating a barcode with custom rotation, padding, and image size.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Sets up parameters and creates a barcode image.
        /// </summary>
        static void Main()
        {
            // Define sample parameters for the barcode generation
            string outputPath = "custom_barcode.png";
            BaseEncodeType encodeType = EncodeTypes.Code128;
            string codeText = "1234567890";
            float rotationAngle = 45f;   // degrees
            float padding = 10f;         // points
            float imageWidth = 300f;     // points
            float imageHeight = 150f;    // points

            // Generate the barcode with the specified customizations
            CreateBarcode(outputPath, encodeType, codeText, rotationAngle, padding, imageWidth, imageHeight);

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }

        /// <summary>
        /// Generates a barcode image with custom rotation, uniform padding, and image size.
        /// </summary>
        /// <param name="outputPath">File path to save the barcode image.</param>
        /// <param name="encodeType">Symbology type (e.g., EncodeTypes.Code128).</param>
        /// <param name="codeText">Text to encode.</param>
        /// <param name="rotationAngle">Rotation angle in degrees.</param>
        /// <param name="padding">Uniform padding applied to all sides (points).</param>
        /// <param name="imageWidth">Desired image width (points).</param>
        /// <param name="imageHeight">Desired image height (points).</param>
        static void CreateBarcode(string outputPath, BaseEncodeType encodeType, string codeText,
                                  float rotationAngle, float padding,
                                  float imageWidth, float imageHeight)
        {
            // Validate arguments to ensure required values are provided
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));
            if (codeText == null)
                throw new ArgumentNullException(nameof(codeText));

            // Initialize the barcode generator with the chosen symbology and text
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply rotation to the barcode image
                generator.Parameters.RotationAngle = rotationAngle;

                // Set uniform padding on all sides
                generator.Parameters.Barcode.Padding.Left.Point = padding;
                generator.Parameters.Barcode.Padding.Top.Point = padding;
                generator.Parameters.Barcode.Padding.Right.Point = padding;
                generator.Parameters.Barcode.Padding.Bottom.Point = padding;

                // Define the output image dimensions
                generator.Parameters.ImageWidth.Point = imageWidth;
                generator.Parameters.ImageHeight.Point = imageHeight;

                // Save the generated barcode as a PNG file
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }
}