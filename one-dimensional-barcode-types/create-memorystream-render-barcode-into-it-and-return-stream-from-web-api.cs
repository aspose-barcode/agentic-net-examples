using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeApiSimulation
{
    class Program
    {
        static void Main()
        {
            // Simulate handling a request and obtaining the barcode stream
            using (MemoryStream barcodeStream = GetBarcodeStream())
            {
                // Demonstrate that the stream contains data
                Console.WriteLine($"Generated barcode stream length: {barcodeStream.Length} bytes");
            }
        }

        // Simulated web API method that creates a barcode and returns it as a MemoryStream
        static MemoryStream GetBarcodeStream()
        {
            // Create a memory stream to hold the barcode image
            var memoryStream = new MemoryStream();

            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Optional styling
                generator.Parameters.Barcode.BarColor = Color.Blue;
                generator.Parameters.BackColor = Color.White;

                // Save the barcode image into the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position so it can be read from the beginning
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}