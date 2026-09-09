// Title: Generate DotCode barcode and return as MemoryStream
// Description: Demonstrates creating a DotCode barcode using Aspose.BarCode, returning it as a MemoryStream for further processing, and saving it to a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.DotCode. Typical use cases include generating machine-readable DotCode symbols for inventory, tracking, or authentication, and then processing the image in memory for further manipulation or storage. Developers often need to obtain the barcode as a stream to integrate with other services or APIs without writing to disk first.
// Prompt: Create method that returns DotCode barcode as MemoryStream for further image processing.
// Tags: dotcode, barcode generation, memorystream, png, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace DotCodeExample
{
    /// <summary>
    /// Demonstrates generating a DotCode barcode and handling it as a MemoryStream.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that generates a DotCode barcode, displays stream info, and saves it to a PNG file.
        /// </summary>
        static void Main()
        {
            // Text to encode in the DotCode barcode
            string codeText = "Aspose";

            // Generate the barcode and obtain it as a MemoryStream
            MemoryStream barcodeStream = GenerateDotCodeBarcode(codeText);
            Console.WriteLine($"Generated DotCode barcode stream length: {barcodeStream.Length}");

            // Define the output file path for the PNG image
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "DotCodeSample.png");

            // Write the MemoryStream contents to a file
            using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                barcodeStream.CopyTo(file);
            }

            Console.WriteLine($"Barcode saved to {outputPath}");
        }

        /// <summary>
        /// Generates a DotCode barcode for the specified text and returns it as a MemoryStream in PNG format.
        /// </summary>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <returns>A MemoryStream containing the generated PNG barcode image.</returns>
        static MemoryStream GenerateDotCodeBarcode(string codeText)
        {
            // Create a memory stream to hold the barcode image
            MemoryStream stream = new MemoryStream();

            // Initialize the barcode generator with DotCode symbology and the provided text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
            {
                // Set the X-dimension (module size) in pixels
                generator.Parameters.Barcode.XDimension.Pixels = 10;

                // Save the generated barcode to the memory stream as a PNG image
                generator.Save(stream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning for subsequent reading
            stream.Position = 0;
            return stream;
        }
    }
}