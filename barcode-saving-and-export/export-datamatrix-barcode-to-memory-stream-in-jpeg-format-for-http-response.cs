using System;
using System.IO;
using Aspose.BarCode.Generation;

namespace DataMatrixToMemoryStream
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a memory stream to hold the JPEG image
            using (var memoryStream = new MemoryStream())
            {
                // Initialize the barcode generator for DataMatrix
                using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix))
                {
                    // Set the text to be encoded
                    generator.CodeText = "Hello, Aspose!";

                    // Save the barcode image to the memory stream in JPEG format
                    generator.Save(memoryStream, BarCodeImageFormat.Jpeg);
                }

                // Reset stream position to the beginning for further processing (e.g., HTTP response)
                memoryStream.Position = 0;

                // Example: output the size of the generated image
                Console.WriteLine($"Generated DataMatrix JPEG size: {memoryStream.Length} bytes");
                // In a real HTTP scenario, the memoryStream would be written to the response body.
            }
        }
    }
}