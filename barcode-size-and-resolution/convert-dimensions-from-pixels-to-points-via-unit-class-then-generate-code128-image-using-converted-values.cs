using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with specific image dimensions using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define desired image dimensions in pixels.
        float pixelWidth = 300f;
        float pixelHeight = 150f;

        // Initialize a barcode generator for Code128 with the sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the image width and height using pixel units.
            generator.Parameters.ImageWidth.Pixels = pixelWidth;
            generator.Parameters.ImageHeight.Pixels = pixelHeight;

            // Convert the pixel dimensions to points (1 point = 1/72 inch) via the Unit class.
            float widthInPoints = generator.Parameters.ImageWidth.Point;
            float heightInPoints = generator.Parameters.ImageHeight.Point;

            // Apply the converted point values back to the generator (ensures proper scaling).
            generator.Parameters.ImageWidth.Point = widthInPoints;
            generator.Parameters.ImageHeight.Point = heightInPoints;

            // Save the generated barcode as a PNG file.
            generator.Save("code128.png");
        }

        // Inform the user that the barcode has been created.
        Console.WriteLine("Barcode generated and saved as code128.png");
    }
}