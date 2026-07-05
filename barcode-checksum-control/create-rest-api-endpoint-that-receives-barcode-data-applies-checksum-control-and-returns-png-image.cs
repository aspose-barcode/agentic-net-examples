// Title: Barcode generation with checksum and PNG output via simulated REST endpoint
// Description: Demonstrates how to receive barcode data, apply checksum control, and return a PNG image (as Base64) for a REST API scenario.
// Prompt: Create a REST API endpoint that receives barcode data, applies checksum control, and returns a PNG image.
// Tags: barcode, checksum, png, rest api, aspnet, aspose.barcode

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that simulates a REST API endpoint for barcode generation.
/// It receives barcode type and data, applies checksum control, and returns a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that mimics handling a REST request, generates the barcode, and outputs the image as Base64.
    /// </summary>
    static void Main()
    {
        // ---------- Simulated request payload ----------
        // Barcode symbology (e.g., Code128) and the data to encode.
        string symbologyName = "Code128"; // barcode type
        string codeText = "123ABC";       // data to encode

        // ---------- Resolve symbology name to EncodeTypes ----------
        // Use reflection to map the string name to the corresponding BaseEncodeType enum value.
        var field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // ---------- Create barcode generator with checksum enabled ----------
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Enable checksum generation and make it visible in the human‑readable text.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // ---------- Save barcode to a memory stream as PNG ----------
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // ---------- Output the image as a Base64 string ----------
                // This simulates the HTTP response body in a console environment.
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine("Barcode PNG (Base64):");
                Console.WriteLine(base64);
            }
        }

        // Note: In a real REST API you would return the PNG bytes with the appropriate
        // content‑type header. The console application prints the Base64 representation
        // to demonstrate the result within the snippet runner environment.
    }
}