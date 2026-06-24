using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Demonstrates generating a Code128 barcode image using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Generates a barcode, saves it to a file,
        /// and outputs the image dimensions to the console.
        /// </summary>
        static void Main()
        {
            // Define the output file path for the generated barcode image.
            string outputPath = "barcode.png";

            // Create a BarcodeGenerator instance configured for Code128 encoding.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text to be encoded in the barcode.
                generator.CodeText = "1234567890";

                // Enable automatic height adjustment using interpolation mode.
                // No explicit BarHeight is set; the generator determines height automatically.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Specify the desired image width (40 millimeters). Height will be auto‑sized.
                generator.Parameters.ImageWidth.Millimeters = 40f;

                // Save the generated barcode image to the specified file path.
                generator.Save(outputPath);
            }

            // Check whether the barcode image file was successfully created.
            if (File.Exists(outputPath))
            {
                // Load the image to retrieve its dimensions.
                using (var image = Image.FromFile(outputPath))
                {
                    int width = image.Width;   // Image width in pixels
                    int height = image.Height; // Image height in pixels

                    // Output the dimensions to the console.
                    Console.WriteLine($"Generated barcode size: {width}x{height} pixels");
                }
            }
            else
            {
                // Inform the user that the image was not created.
                Console.WriteLine("Barcode image was not created.");
            }
        }
    }
}