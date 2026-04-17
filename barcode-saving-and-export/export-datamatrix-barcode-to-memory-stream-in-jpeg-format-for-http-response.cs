using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample data to encode in the DataMatrix barcode
        const string codeText = "HelloWorld";

        // Create a BarcodeGenerator for DataMatrix with the specified code text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set a higher resolution for better image quality (optional)
            generator.Parameters.Resolution = 300; // DPI

            // Save the generated barcode to a memory stream in JPEG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Jpeg);

                // Reset the stream position to the beginning for any further reading
                memoryStream.Position = 0;

                // Simulate an HTTP response by outputting the size of the generated image
                Console.WriteLine($"DataMatrix JPEG image generated, size: {memoryStream.Length} bytes");
            }
        }
    }
}