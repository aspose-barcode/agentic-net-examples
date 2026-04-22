using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeMemoryStreamExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text to encode
                generator.CodeText = "1234567890";

                // Generate the barcode image
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the image to a memory stream in PNG format
                    using (var memoryStream = new MemoryStream())
                    {
                        bitmap.Save(memoryStream, ImageFormat.Png);

                        // Retrieve the image bytes from the memory stream
                        byte[] imageBytes = memoryStream.ToArray();

                        // Output the size of the generated image
                        Console.WriteLine($"Generated barcode image size in memory: {imageBytes.Length} bytes");
                    }
                }
            }
        }
    }
}