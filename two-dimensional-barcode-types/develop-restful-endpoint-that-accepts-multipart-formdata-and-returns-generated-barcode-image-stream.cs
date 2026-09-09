// Title: Generate barcode image from symbology name and code text
// Description: Demonstrates how to create a barcode image using Aspose.BarCode based on a supplied symbology and text, then output the image as a Base64 string (simulating a REST response).
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode images. Typical scenarios include web APIs that generate barcodes on‑the‑fly for labels, tickets, or inventory systems. Developers often need to map user‑provided symbology names to EncodeTypes and return the image in a stream or encoded format.
// Prompt: Develop a RESTful endpoint that accepts multipart/form-data and returns generated barcode image stream.
// Tags: barcode, symbology, generation, png, aspose.barcode, aspose.drawing, rest, multipart, image

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode image based on input parameters,
/// simulating the core logic of a RESTful endpoint.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode using Aspose.BarCode and writes the image as a Base64 string.
    /// </summary>
    static void Main()
    {
        // Simulated input that would come from a multipart/form-data request
        string symbologyName = "Code128";
        string codeText = "Sample123";

        // Resolve symbology name to BaseEncodeType using reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Generate barcode with specified parameters
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set visual appearance
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Resolution = 300f;

            // Save barcode to a memory stream in PNG format
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Convert the image stream to Base64 (simulating HTTP response body)
                string base64 = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine(base64);
            }
        }
    }
}