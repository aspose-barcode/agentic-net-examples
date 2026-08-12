// Title: Generate DotCode barcode as Base64 PNG string
// Description: Demonstrates how to create a DotCode barcode using Aspose.BarCode, encode it as a PNG image, and return the image data as a Base64 string for client‑side rendering.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce DotCode symbology images. Typical use cases include generating barcodes on the fly for web APIs, embedding them in HTML, or sending them to mobile clients. Developers often need to convert barcode images to Base64 to avoid file handling and enable direct rendering in browsers.
// Prompt: Expose an API that returns DotCode barcode as base64 string for client‑side rendering.
// Tags: dotcode, barcode, generation, base64, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace DotCodeBase64Example
{
    /// <summary>
    /// Provides a console entry point that generates a DotCode barcode and outputs its PNG representation as a Base64 string.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates a DotCode barcode image in PNG format and returns it as a Base64 string.
        /// </summary>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <param name="columns">Optional number of columns for the DotCode matrix; rows are auto‑calculated.</param>
        /// <returns>Base64‑encoded PNG image data.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="codeText"/> is null or empty.</exception>
        static string GenerateDotCodeBase64(string codeText, int columns = 20)
        {
            // Validate input.
            if (string.IsNullOrEmpty(codeText))
                throw new ArgumentException("Code text must not be null or empty.", nameof(codeText));

            // Initialize the barcode generator for DotCode symbology.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
            {
                // Configure only the column count; row count is determined automatically.
                generator.Parameters.Barcode.DotCode.Columns = columns;

                // Write the barcode image to a memory stream in PNG format.
                using (MemoryStream ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Convert the raw image bytes to a Base64 string for easy transport.
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// Application entry point. Generates a sample DotCode barcode and writes the Base64 PNG string to the console.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Sample text to encode; replace with dynamic input as needed.
            string sampleText = "Hello DotCode!";

            try
            {
                // Generate the Base64 representation of the barcode image.
                string base64Image = GenerateDotCodeBase64(sampleText);
                Console.WriteLine("Base64 PNG of DotCode barcode:");
                Console.WriteLine(base64Image);
            }
            catch (Exception ex)
            {
                // Output any errors that occur during generation.
                Console.WriteLine($"Error generating barcode: {ex.Message}");
            }
        }
    }
}