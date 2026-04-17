using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Generate a DotCode barcode and obtain the image as a MemoryStream
        using (MemoryStream barcodeStream = GenerateDotCode("Sample123"))
        {
            // Example usage: display the size of the generated image stream
            Console.WriteLine($"Generated barcode image size: {barcodeStream.Length} bytes");
            // The stream can be passed to other image processing components here
        }
    }

    /// <summary>
    /// Creates a DotCode barcode image and returns it as a MemoryStream.
    /// </summary>
    /// <param name="codeText">The text to encode in the DotCode barcode.</param>
    /// <returns>A MemoryStream containing the barcode image in PNG format.</returns>
    public static MemoryStream GenerateDotCode(string codeText)
    {
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text must be a non-empty string.", nameof(codeText));

        // Prepare a memory stream to hold the barcode image
        var stream = new MemoryStream();

        // Create the barcode generator for DotCode symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Optional: customize DotCode parameters if needed
            // generator.Parameters.Barcode.DotCode.Columns = 5;
            // generator.Parameters.Barcode.DotCode.Rows = 5;

            // Save the generated barcode image to the memory stream in PNG format
            generator.Save(stream, BarCodeImageFormat.Png);
        }

        // Reset the stream position so it can be read from the beginning
        stream.Position = 0;
        return stream;
    }
}