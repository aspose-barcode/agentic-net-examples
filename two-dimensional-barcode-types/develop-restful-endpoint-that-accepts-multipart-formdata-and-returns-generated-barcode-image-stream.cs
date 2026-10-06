// Title: Generate Barcode Image and Return as Base64 Stream
// Description: Demonstrates creating a barcode with Aspose.BarCode, encoding it as PNG, and outputting the image as a Base64 string, mimicking a RESTful service response.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcode images on the fly. Typical scenarios include web APIs that generate barcodes for inventory, shipping, or ticketing systems, where developers need to return the image as a byte stream or Base64 payload. The snippet shows common configuration steps such as setting dimensions and colors, useful for developers building REST endpoints that serve barcode graphics.
// Prompt: Develop a RESTful endpoint that accepts multipart/form-data and returns generated barcode image stream.
// Tags: barcode, symbology, generation, png, base64, aspose.barcode, rest, multipart, image

using System;
using System.IO;
using System.Reflection;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode and outputs the image as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that simulates receiving barcode parameters from a multipart/form-data request,
    /// generates the barcode, and writes the PNG image as a Base64 string to the console.
    /// </summary>
    static void Main()
    {
        // Simulated input that would come from a multipart/form-data request
        string symbologyName = "Code128";
        string codeText = "12345678";

        // Resolve the symbology name to the corresponding BaseEncodeType using reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create a BarcodeGenerator with the resolved encode type and the provided code text
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Optional: customize barcode appearance
            generator.Parameters.Barcode.XDimension.Pixels = 4f; // Set module width
            generator.Parameters.Barcode.BarColor = Color.Black; // Set bar color
            generator.Parameters.BackColor = Color.White; // Set background color

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the PNG byte array to a Base64 string for easy transport
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine("Generated barcode image (Base64 PNG):");
                Console.WriteLine(base64);
            }
        }

        // In a real RESTful service, the imageBytes would be written directly to the HTTP response stream.
    }
}