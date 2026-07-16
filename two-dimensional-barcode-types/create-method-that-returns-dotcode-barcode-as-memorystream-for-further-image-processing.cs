// Title: Generate DotCode barcode as MemoryStream
// Description: Demonstrates creating a DotCode barcode image and returning it as a MemoryStream for downstream image processing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.DotCode. Typical use cases include generating barcodes for printing, embedding in documents, or further image manipulation. Developers often need to obtain barcode images as streams to integrate with other APIs or services.
// Prompt: Create method that returns DotCode barcode as MemoryStream for further image processing.
// Tags: dotcode, barcode, generation, memorystream, png, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace DotCodeBarcodeExample
{
    /// <summary>
    /// Provides an example of generating a DotCode barcode and returning it as a <see cref="MemoryStream"/>.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Generates a DotCode barcode and writes the resulting image size to the console.
        /// </summary>
        static void Main()
        {
            // Sample text to encode in the DotCode barcode.
            const string sampleText = "Sample DotCode Text";

            // Generate the barcode and obtain it as a memory stream.
            using (MemoryStream barcodeStream = GenerateDotCodeBarcode(sampleText))
            {
                // The stream now contains the PNG image of the DotCode barcode.
                // For demonstration, output the size of the generated image.
                Console.WriteLine($"Generated barcode image size: {barcodeStream.Length} bytes");
            }
        }

        /// <summary>
        /// Generates a DotCode barcode image and returns it as a <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="codeText">The text to encode in the DotCode barcode.</param>
        /// <returns>A <see cref="MemoryStream"/> containing the PNG image of the barcode.</returns>
        static MemoryStream GenerateDotCodeBarcode(string codeText)
        {
            // Create a memory stream that will hold the barcode image.
            var memoryStream = new MemoryStream();

            // Use a BarcodeGenerator to create the DotCode barcode.
            // The constructor takes the symbology type and the code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
            {
                // Optional: configure DotCode specific parameters if needed.
                // For example, set the ECI encoding to UTF-8.
                generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.UTF8;

                // Save the generated barcode directly to the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning so it can be read by the caller.
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}