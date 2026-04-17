using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

namespace MaxiCodeExample
{
    class Program
    {
        static void Main()
        {
            byte[] imageBytes = GetMaxiCodeBytes();
            Console.WriteLine($"Generated MaxiCode image byte array length: {imageBytes.Length}");
        }

        // Generates a MaxiCode barcode (Mode 4) and returns the image as a byte array.
        static byte[] GetMaxiCodeBytes()
        {
            // Prepare the codetext for a standard MaxiCode (Mode 4).
            var maxiCodeCodetext = new MaxiCodeStandardCodetext
            {
                Mode = MaxiCodeMode.Mode4,
                Message = "Test message"
            };

            // Create a memory stream to hold the generated image.
            using (var memoryStream = new MemoryStream())
            {
                // Generate the barcode using ComplexBarcodeGenerator.
                using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
                {
                    // Save the barcode image to the memory stream in PNG format.
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                }

                // Return the image bytes.
                return memoryStream.ToArray();
            }
        }
    }
}