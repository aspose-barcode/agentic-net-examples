using System;
using Aspose.BarCode.Generation;

namespace BarcodeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample usage of the reusable barcode generation function
            string outputPath = "custom_barcode.png";
            string codeText = "1234567890";

            // Rotation angle in degrees, padding in points, size in points
            float rotationAngle = 45f;
            float padding = 10f;
            float imageWidth = 300f;
            float imageHeight = 150f;

            GenerateCustomBarcode(codeText, rotationAngle, padding, imageWidth, imageHeight, outputPath);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }

        /// <summary>
        /// Generates a barcode image with custom rotation, padding, and size.
        /// </summary>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <param name="rotationAngle">Rotation angle in degrees.</param>
        /// <param name="padding">Uniform padding (points) applied to all sides.</param>
        /// <param name="width">Image width (points).</param>
        /// <param name="height">Image height (points).</param>
        /// <param name="outputFile">File path to save the generated image.</param>
        public static void GenerateCustomBarcode(string codeText, float rotationAngle, float padding, float width, float height, string outputFile)
        {
            // Use Code128 as an example symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text to encode
                generator.CodeText = codeText;

                // Apply rotation
                generator.Parameters.RotationAngle = rotationAngle;

                // Apply uniform padding to all sides
                generator.Parameters.Barcode.Padding.Left.Point = padding;
                generator.Parameters.Barcode.Padding.Top.Point = padding;
                generator.Parameters.Barcode.Padding.Right.Point = padding;
                generator.Parameters.Barcode.Padding.Bottom.Point = padding;

                // Set image dimensions
                generator.Parameters.ImageWidth.Point = width;
                generator.Parameters.ImageHeight.Point = height;

                // Save the barcode image as PNG
                generator.Save(outputFile);
            }
        }
    }
}