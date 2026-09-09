// Title: Generate Australia Post Postal Barcode and Return Image Bytes
// Description: Demonstrates how to create an Australia Post (postal) barcode using Aspose.BarCode, encode it, and obtain the PNG image as a byte array.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the BarcodeGenerator class with EncodeTypes.AustraliaPost. It illustrates typical use cases such as creating postal barcodes for shipping labels, setting encoding tables, and customizing visual parameters. Developers working with barcode creation for logistics and mailing systems can reference this pattern for generating image data programmatically.
// Prompt: Build a REST API endpoint that receives postal barcode data and returns the image as a byte array.
// Tags: barcode, australia post, postal, generation, png, byte array, aspose.barcode, encoding, logistics

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating an Australia Post postal barcode and retrieving the image as a byte array.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console example. Simulates receiving barcode data, generates the barcode image, and outputs the byte array length.
    /// </summary>
    static void Main()
    {
        // Simulated request payload: postal barcode data (Australia Post format)
        string postalData = "1100000000"; // FCC 11 + 8‑digit DPID, no customer info

        try
        {
            // Generate barcode image bytes from the provided data
            byte[] imageBytes = GeneratePostalBarcodeImageBytes(postalData);
            Console.WriteLine($"Generated barcode image byte array length: {imageBytes.Length}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a PNG image of an Australia Post barcode and returns it as a byte array.
    /// </summary>
    /// <param name="codeText">The raw postal data to encode (e.g., FCC and DPID).</param>
    /// <returns>Byte array containing the PNG image data.</returns>
    static byte[] GeneratePostalBarcodeImageBytes(string codeText)
    {
        // Validate input
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text must not be null or empty.", nameof(codeText));

        // Initialize the barcode generator with the Australia Post symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set the encoding table for customer information interpreting type
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
            // Optional visual setting: define X-dimension in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the image data as a byte array
                return ms.ToArray();
            }
        }
    }
}