// Title: Generate Barcode PNG with Checksum (REST API Simulation)
// Description: Demonstrates creating a barcode image with checksum enabled and returning it as a PNG byte stream, mimicking a REST API endpoint.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and related parameter classes to produce checksum‑controlled barcodes. Typical use cases include server‑side barcode creation for web services, e‑commerce platforms, and inventory systems where clients request barcode images via HTTP. Developers often need to select symbologies dynamically, enable checksum validation, and deliver the result in common image formats such as PNG.
// Prompt: Create a REST API endpoint that receives barcode data, applies checksum control, and returns a PNG image.
// Tags: barcode, checksum, png, aspose.barcode, aspose.drawing, rest, api, generation

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that simulates a REST API endpoint for generating a barcode image with checksum control.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds a barcode based on simulated request data, enables checksum, and writes the PNG to disk.
    /// </summary>
    static void Main()
    {
        // ---------- Simulated request payload ----------
        // Symbology requested by the client (e.g., "Code128")
        string symbologyName = "Code128";

        // Data to encode into the barcode
        string codeText = "123456789012";

        // ---------- Resolve symbology name to EncodeTypes ----------
        // Use reflection to map the string name to the corresponding BaseEncodeType value
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // ---------- Create barcode generator with checksum enabled ----------
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Enable checksum generation and ensure it appears in the human‑readable text
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Optional: set foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // ---------- Generate PNG image into a memory stream ----------
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // ---------- Simulate API response by writing PNG bytes to a file ----------
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");
                File.WriteAllBytes(outputPath, ms.ToArray());

                Console.WriteLine($"Barcode image saved to: {outputPath}");
            }
        }
    }
}