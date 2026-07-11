// Title: Barcode to Base64 conversion example
// Description: Demonstrates generating a Code128 barcode image and converting it to a Base64 string for embedding in HTML.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, set parameters, generate a bitmap, and obtain a Base64-encoded PNG. Developers often need to embed barcodes directly into web pages or emails without saving files, making this pattern common for HTML image sources.
// Prompt: Implement a function that converts a generated barcode image to a Base64 string for embedding in HTML.
// Tags: barcode symbology, generation, base64, html embedding, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeBase64Example
{
    /// <summary>
    /// Provides an example of generating a barcode and converting it to a Base64 string for HTML embedding.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Generates a Code128 barcode and writes its Base64 representation to the console.
        /// </summary>
        static void Main()
        {
            // Define the text to encode in the barcode.
            string codeText = "1234567890";

            // Initialize a BarcodeGenerator for Code128 symbology with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: adjust the module (X) dimension to control barcode size.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Convert the generated barcode image to a Base64 string.
                string base64 = ConvertBarcodeToBase64(generator);

                // Output the Base64 string prefixed with the data URI scheme for direct HTML embedding.
                Console.WriteLine("data:image/png;base64," + base64);
            }
        }

        /// <summary>
        /// Generates the barcode image from the provided generator and returns its Base64 representation.
        /// </summary>
        /// <param name="generator">Configured <see cref="BarcodeGenerator"/> instance.</param>
        /// <returns>Base64-encoded PNG image.</returns>
        static string ConvertBarcodeToBase64(BarcodeGenerator generator)
        {
            // Generate the barcode as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap into a memory stream using PNG format.
                using (var memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    // Convert the stream's byte array to a Base64 string.
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }
}