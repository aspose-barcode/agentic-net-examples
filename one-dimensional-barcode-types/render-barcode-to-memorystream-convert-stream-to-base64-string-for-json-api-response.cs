// Title: Render barcode to Base64 string for JSON response
// Description: Demonstrates generating a Code128 barcode, saving it to a MemoryStream, and converting the image to a Base64 string suitable for inclusion in a JSON API payload.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator and BarCodeImageFormat to create barcode images in memory. Typical use cases include returning barcode images from web services without writing files to disk. Developers often need to encode the image as Base64 for JSON transport, leveraging MemoryStream and Convert.ToBase64String.
// Prompt: Render barcode to a MemoryStream, convert the stream to a Base64 string for JSON API response.
// Tags: barcode, code128, base64, json, memorystream, aspnet, aspose.barcode, image-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode, encodes it as Base64, and writes a JSON-formatted string to the console.
    /// </summary>
    static void Main()
    {
        // Define the barcode data and symbology
        string codeText = "12345678";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Create a memory stream to hold the generated image
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Initialize the barcode generator with the chosen type and data
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save the barcode image to the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading
            memoryStream.Position = 0;

            // Convert the stream contents to a byte array
            byte[] imageBytes = memoryStream.ToArray();

            // Encode the byte array as a Base64 string
            string base64String = Convert.ToBase64String(imageBytes);

            // Output a JSON-like response containing the Base64-encoded barcode
            Console.WriteLine("{\"barcodeBase64\":\"" + base64String + "\"}");
        }
    }
}