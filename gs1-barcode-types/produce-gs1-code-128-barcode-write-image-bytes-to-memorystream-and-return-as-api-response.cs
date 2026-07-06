// Title: Generate GS1 Code 128 barcode and return as API response
// Description: Demonstrates creating a GS1 Code 128 barcode, writing the PNG image to a MemoryStream, and returning the image bytes (as Base64) suitable for an API response.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.GS1Code128, configure barcode parameters, and output the image in PNG format. Developers often need to generate GS1-compliant barcodes for product identification and return them via web APIs; this snippet shows the typical workflow using Aspose.BarCode classes such as BarcodeGenerator, BarCodeImageFormat, and related parameter settings.
// Prompt: Produce a GS1 Code 128 barcode, write image bytes to a MemoryStream, and return as an API response.
// Tags: gs1, code128, barcode, generation, png, memorystream, api, response

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode,
/// writes the PNG image to a <see cref="MemoryStream"/>,
/// and outputs the image bytes as a Base64 string (simulating an API response).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the result to the console.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Code 128 data (AI (01) for GTIN)
        const string gs1Code128Text = "(01)12345678901231";

        // Create a memory stream to hold the generated barcode image
        using (var imageStream = new MemoryStream())
        {
            // Initialize the barcode generator for GS1 Code 128 with the sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1Code128Text))
            {
                // Ensure the checksum is always shown (default for GS1 Code 128)
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Save the barcode image as PNG into the memory stream
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning for reading
            imageStream.Position = 0;

            // In a real API you would return the stream directly.
            // Here we demonstrate by converting the image bytes to a Base64 string.
            byte[] imageBytes = imageStream.ToArray();
            string base64Image = Convert.ToBase64String(imageBytes);

            // Output the Base64 string (simulating an API response body)
            Console.WriteLine(base64Image);
        }
    }
}