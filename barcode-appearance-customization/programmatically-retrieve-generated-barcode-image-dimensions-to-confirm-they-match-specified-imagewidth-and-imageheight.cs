using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with specific image dimensions
/// using Aspose.BarCode and verifies the resulting image size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it to a file, and checks the image dimensions.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Desired image dimensions in points (1 point = 1/72 inch).
        float targetWidth = 300f;
        float targetHeight = 150f;

        // Create a BarcodeGenerator for Code128 with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set AutoSizeMode to Interpolation so that ImageWidth/ImageHeight are respected.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Assign the target width and height (in points) to the generator parameters.
            generator.Parameters.ImageWidth.Point = targetWidth;
            generator.Parameters.ImageHeight.Point = targetHeight;

            // Save the generated barcode image to the specified file.
            generator.Save(outputPath);
        }

        // Verify that the barcode image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Load the saved image to inspect its actual dimensions.
        using (var bitmap = new Bitmap(outputPath))
        {
            int actualWidth = bitmap.Width;
            int actualHeight = bitmap.Height;

            // Output the target and actual dimensions for comparison.
            Console.WriteLine($"Target Width: {(int)targetWidth}, Actual Width: {actualWidth}");
            Console.WriteLine($"Target Height: {(int)targetHeight}, Actual Height: {actualHeight}");

            // Determine whether the actual dimensions match the specified targets.
            bool widthMatch = actualWidth == (int)targetWidth;
            bool heightMatch = actualHeight == (int)targetHeight;

            // Report the result of the dimension comparison.
            if (widthMatch && heightMatch)
            {
                Console.WriteLine("Image dimensions match the specified ImageWidth and ImageHeight.");
            }
            else
            {
                Console.WriteLine("Image dimensions do NOT match the specified ImageWidth and ImageHeight.");
            }
        }
    }
}