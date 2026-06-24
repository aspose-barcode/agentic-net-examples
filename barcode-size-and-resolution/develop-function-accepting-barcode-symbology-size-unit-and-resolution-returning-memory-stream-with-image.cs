using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode image using Aspose.BarCode and saving it to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image and returns it as a <see cref="MemoryStream"/>.
    /// </summary>
    /// <param name="symbology">The barcode type (e.g., <see cref="EncodeTypes.Code128"/>).</param>
    /// <param name="sizePoints">The width and height of the image in points.</param>
    /// <param name="resolution">The image resolution in DPI.</param>
    /// <returns>A memory stream containing the generated PNG barcode image.</returns>
    static MemoryStream GenerateBarcodeImage(BaseEncodeType symbology, float sizePoints, float resolution)
    {
        // Create a memory stream to hold the image data.
        var imageStream = new MemoryStream();

        // Use a BarcodeGenerator inside a using block to ensure proper disposal.
        using (var generator = new BarcodeGenerator(symbology, "Sample123"))
        {
            // Set image dimensions using the Point unit.
            generator.Parameters.ImageWidth.Point = sizePoints;
            generator.Parameters.ImageHeight.Point = sizePoints;

            // Set the desired resolution (DPI).
            generator.Parameters.Resolution = resolution;

            // Save the barcode to the memory stream in PNG format.
            generator.Save(imageStream, BarCodeImageFormat.Png);
        }

        // Reset the stream position so it can be read from the beginning.
        imageStream.Position = 0;
        return imageStream;
    }

    /// <summary>
    /// Entry point of the application. Generates a barcode and writes it to a file.
    /// </summary>
    static void Main()
    {
        // Example usage: generate a Code128 barcode with 200pt size and 300 DPI resolution.
        MemoryStream barcodeStream = GenerateBarcodeImage(EncodeTypes.Code128, 200f, 300f);

        // Write the size of the generated image to the console for verification.
        Console.WriteLine($"Generated barcode image size: {barcodeStream.Length} bytes");

        // Save the image to a file to verify the output (optional).
        using (var fileStream = new FileStream("barcode.png", FileMode.Create, FileAccess.Write))
        {
            barcodeStream.CopyTo(fileStream);
        }

        // Clean up the memory stream.
        barcodeStream.Dispose();
    }
}