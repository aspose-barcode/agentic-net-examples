using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Demonstrates generating a DataMatrix barcode with a caption using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Generates a DataMatrix barcode, adds a caption, and saves the image.
        /// </summary>
        static void Main()
        {
            // Define the output file path for the generated barcode image.
            string outputPath = "DataMatrixWithCaption.png";

            // Initialize a BarcodeGenerator for a DataMatrix barcode with the specified code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
            {
                // Set the caption text that appears above the barcode.
                generator.Parameters.CaptionAbove.Text = "DataMatrix Sample";

                // Make sure the caption is rendered (visible) in the output image.
                generator.Parameters.CaptionAbove.Visible = true;

                // Align the caption horizontally to the center of the image.
                generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

                // Add a top padding of 10 pixels to create space between the caption and the barcode.
                generator.Parameters.CaptionAbove.Padding.Top.Pixels = 10f;

                // Optional: specify a particular DataMatrix version.
                // This line is commented out because the default version is sufficient for most cases.
                // generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;

                // Save the generated barcode (with caption) to the specified file path.
                generator.Save(outputPath);
            }

            // Inform the user that the barcode image has been saved successfully.
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}