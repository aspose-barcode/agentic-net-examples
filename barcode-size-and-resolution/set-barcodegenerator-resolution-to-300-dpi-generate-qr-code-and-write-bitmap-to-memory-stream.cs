using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code using Aspose.BarCode and writing it to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code and outputs the stream size.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Configure the image resolution (dots per inch).
            generator.Parameters.Resolution = 300f;

            // Create a memory stream to hold the generated PNG image.
            using (var memoryStream = new MemoryStream())
            {
                // Save the QR code image into the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Output the size of the generated image data.
                Console.WriteLine($"QR code generated. Stream length: {memoryStream.Length} bytes.");
            } // memoryStream disposed here
        } // generator disposed here
    }
}