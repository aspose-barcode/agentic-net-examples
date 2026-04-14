using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample usage of the reusable barcode generator
            float rotation = 90f;          // rotation angle in degrees
            float padding = 10f;           // padding for all sides (points)
            float width = 300f;            // image width (points)
            float height = 150f;           // image height (points)
            string codeText = "1234567890";
            string outputFile = "custom_barcode.png";

            GenerateBarcode(rotation, padding, width, height, codeText, EncodeTypes.Code128, outputFile);
            Console.WriteLine($"Barcode saved to {outputFile}");
        }

        /// <summary>
        /// Generates a barcode image with custom rotation, padding, and size.
        /// </summary>
        /// <param name="rotationAngle">Rotation angle in degrees (0, 90, 180, 270).</param>
        /// <param name="padding">Uniform padding for all sides (points).</param>
        /// <param name="imageWidth">Image width (points).</param>
        /// <param name="imageHeight">Image height (points).</param>
        /// <param name="codeText">Text to encode.</param>
        /// <param name="encodeType">Symbology type.</param>
        /// <param name="outputPath">File path to save the image.</param>
        public static void GenerateBarcode(
            float rotationAngle,
            float padding,
            float imageWidth,
            float imageHeight,
            string codeText,
            BaseEncodeType encodeType,
            string outputPath)
        {
            // Validate rotation angle
            if (rotationAngle != 0f && rotationAngle != 90f && rotationAngle != 180f && rotationAngle != 270f)
                throw new ArgumentOutOfRangeException(nameof(rotationAngle), "Rotation angle must be 0, 90, 180, or 270 degrees.");

            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Set rotation
                generator.Parameters.RotationAngle = rotationAngle;

                // Set uniform padding
                generator.Parameters.Barcode.Padding.Left.Point = padding;
                generator.Parameters.Barcode.Padding.Top.Point = padding;
                generator.Parameters.Barcode.Padding.Right.Point = padding;
                generator.Parameters.Barcode.Padding.Bottom.Point = padding;

                // Set image size (used when AutoSizeMode is Interpolation or Nearest)
                generator.Parameters.ImageWidth.Point = imageWidth;
                generator.Parameters.ImageHeight.Point = imageHeight;

                // Save the barcode image
                generator.Save(outputPath);
            }
        }
    }
}