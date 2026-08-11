// Title: Generate a postal barcode image and return it as a byte array
// Description: Demonstrates creating a Postnet barcode from input data and retrieving the PNG image as a byte array, suitable for returning from a REST API.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.Postnet to produce barcode images. Developers often need to embed barcode creation in web services, returning image streams for client consumption. Key classes include BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which are commonly used for on‑the‑fly barcode rendering in ASP.NET Core or other API frameworks.
// Prompt: Build a REST API endpoint that receives postal barcode data and returns the image as a byte array.
// Tags: postnet, barcode generation, image output, png, byte array, aspose.barcode, aspnet core

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Postnet postal barcode and obtaining the image as a byte array.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Simulates receiving barcode data, generates the barcode image, and writes the size to console.
    /// </summary>
    static void Main()
    {
        // Simulate receiving postal barcode data (e.g., Postnet code)
        string postalData = "12345";

        // Generate barcode image as a byte array
        byte[] imageBytes = GeneratePostalBarcode(postalData);

        // Output the size of the generated image
        Console.WriteLine($"Generated barcode image size: {imageBytes.Length} bytes");
    }

    // Generates a postal barcode (Postnet) image and returns it as a byte array (PNG format)
    static byte[] GeneratePostalBarcode(string codeText)
    {
        // Validate input
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text must not be null or empty.", nameof(codeText));

        // Create a memory stream to hold the image data
        using (var memoryStream = new MemoryStream())
        {
            // Initialize the barcode generator for Postnet symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, codeText))
            {
                // Optional: adjust barcode parameters if needed
                // e.g., generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the barcode image to the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Return the image bytes from the memory stream
            return memoryStream.ToArray();
        }
    }
}