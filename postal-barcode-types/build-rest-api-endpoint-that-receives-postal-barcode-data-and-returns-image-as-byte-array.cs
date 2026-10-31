// Title: Generate Australian Post Barcode and Return as Byte Array
// Description: Creates an Australian Post postal barcode from provided data and returns the PNG image as a byte array.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class to produce barcode images. Typical use cases include creating barcode graphics for REST API responses, embedding barcodes in documents, or storing them in databases. Developers often need to configure symbology‑specific parameters and retrieve the image as a byte array for further processing or transmission.
// Prompt: Build a REST API endpoint that receives postal barcode data and returns the image as a byte array.
// Tags: australianpost, barcode, generation, png, bytearray, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating an Australian Post barcode and obtaining the image as a byte array.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that simulates receiving postal barcode data, generates the barcode image,
    /// and outputs the size of the resulting byte array.
    /// </summary>
    static void Main()
    {
        // Simulated request payload: postal barcode data (Australian Post)
        // FCC=11, DPID=00000000, no customer info
        string postalCodeText = "1100000000";

        // Generate barcode image and obtain byte array
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, postalCodeText))
        {
            // Set encoding table (customer information interpreting type) - required for Australian Post
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the memory stream to a byte array for transmission or storage
                barcodeBytes = ms.ToArray();
            }
        }

        // Simulated response: output byte array length
        Console.WriteLine($"Generated barcode byte array length: {barcodeBytes.Length}");
    }
}