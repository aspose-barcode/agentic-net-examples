// Title: Render Barcode to File Stream Using Aspose.BarCode
// Description: Demonstrates how to generate a Code128 barcode and save it directly to a file stream in PNG format.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcode images. Typical use cases include generating barcodes on the fly for reports, labels, or web applications, where developers need to write the image to a stream for further processing or storage.
// Prompt: Render barcode directly to a file stream using Save method, then close the stream to release resources.
// Tags: code128, barcode generation, file stream, png, aspose.barcode, save method

using System;
using System.IO;
using Aspose.BarCode.Generation;

namespace BarcodeStreamExample
{
    /// <summary>
    /// Provides an entry point that generates a Code128 barcode and writes it to a PNG file via a stream.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates a barcode image and saves it directly to a file stream.
        /// </summary>
        static void Main()
        {
            // Define the output file path for the generated barcode image.
            string outputPath = "barcode.png";

            // Initialize a BarcodeGenerator with Code128 symbology and sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
            {
                // Create a FileStream for writing the barcode image to the specified file.
                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // Save the barcode directly to the stream in PNG format.
                    generator.Save(fileStream, BarCodeImageFormat.Png);
                } // The FileStream is disposed and closed here.
            } // The BarcodeGenerator is disposed here.

            // Output the full path of the saved barcode image for verification.
            Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
        }
    }
}