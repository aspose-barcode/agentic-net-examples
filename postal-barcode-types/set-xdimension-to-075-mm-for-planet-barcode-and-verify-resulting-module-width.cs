using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting the XDimension of a Planet barcode and saving it as an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Desired XDimension: 0.75 mm → convert to points (1 point = 0.352777 mm)
        float xDimPoints = 0.75f / 0.352777f; // approx 2.125 points

        // Create a Planet barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "1234567890"))
        {
            // Set the XDimension (module width) in points
            generator.Parameters.Barcode.XDimension.Point = xDimPoints;

            // Build the full path for the output PNG file in the temporary folder
            string outputPath = Path.Combine(Path.GetTempPath(), "planet.png");

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);

            // Retrieve the actual XDimension value after generation for verification
            float actualXDim = generator.Parameters.Barcode.XDimension.Point;

            // Output the intended and actual XDimension values to the console
            Console.WriteLine($"Set XDimension (points): {xDimPoints:F3}");
            Console.WriteLine($"Actual XDimension after generation (points): {actualXDim:F3}");

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}