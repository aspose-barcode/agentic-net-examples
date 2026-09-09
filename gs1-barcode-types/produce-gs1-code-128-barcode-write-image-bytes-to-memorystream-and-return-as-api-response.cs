// Title: Generate GS1 Code 128 Barcode and Return as Base64 String
// Description: This example creates a GS1 Code 128 barcode, saves it as PNG into a memory stream, and outputs the image as a Base64 string suitable for API responses.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for GS1 Code 128 symbology. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to configure barcode dimensions, render to a MemoryStream, and retrieve raw image bytes. Developers building web APIs or services that need to embed barcode images in JSON or XML responses commonly use this pattern.
// Prompt: Produce a GS1 Code 128 barcode, write image bytes to a MemoryStream, and return as an API response.
// Tags: gs1,code128,barcode,generation,memory stream,base64,api response,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode,
/// writes the PNG image to a memory stream, and prints the Base64 representation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and outputs its Base64 string.
    /// </summary>
    static void Main()
    {
        // Define the GS1 Code 128 data (including Application Identifier (01) for GTIN).
        string codeText = "(01)12345678901231";

        // Create a memory stream to hold the generated barcode image.
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Initialize the barcode generator with GS1 Code 128 symbology and the data.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
            {
                // Set the X-dimension (module width) to 2 pixels for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Save the barcode as a PNG image into the memory stream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Retrieve the image bytes from the memory stream.
            byte[] imageBytes = memoryStream.ToArray();

            // Convert the image bytes to a Base64 string for easy transmission in API responses.
            string base64 = Convert.ToBase64String(imageBytes);

            // Output the Base64 string to the console (or return it from an API endpoint).
            Console.WriteLine(base64);
        }
    }
}