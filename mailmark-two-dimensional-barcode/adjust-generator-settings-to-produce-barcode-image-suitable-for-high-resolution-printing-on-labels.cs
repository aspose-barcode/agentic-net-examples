using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeHighResolution
{
    /// <summary>
    /// Demonstrates generating a high‑resolution Code128 barcode and saving it as a PNG file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Creates a barcode with specific dimensions and resolution,
        /// then writes the image to disk.
        /// </summary>
        static void Main()
        {
            // Path where the generated barcode image will be saved.
            string outputPath = "highres_label.png";

            // Initialize a barcode generator for Code128 with the sample text "1234567890".
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set the image resolution to 300 DPI, suitable for high‑quality label printing.
                generator.Parameters.Resolution = 300f;

                // Turn off automatic sizing so we can manually define dimensions.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Configure the module (X) size and the bar height in points.
                generator.Parameters.Barcode.XDimension.Point = 2f;   // 2 points per module (narrow bar width)
                generator.Parameters.Barcode.BarHeight.Point = 50f; // 50 points tall (overall bar height)

                // Apply uniform padding of 5 points on all sides of the barcode.
                generator.Parameters.Barcode.Padding.Left.Point   = 5f;
                generator.Parameters.Barcode.Padding.Top.Point    = 5f;
                generator.Parameters.Barcode.Padding.Right.Point  = 5f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

                // Save the generated barcode image to the specified file.
                // The format (PNG) is inferred from the file extension.
                generator.Save(outputPath);
            }

            // Inform the user that the image has been saved.
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}