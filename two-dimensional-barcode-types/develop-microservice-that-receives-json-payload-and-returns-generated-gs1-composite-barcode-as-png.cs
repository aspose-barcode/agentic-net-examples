// Title: Generate GS1 Composite barcode and return PNG as Base64
// Description: Demonstrates creating a GS1 Composite barcode from a JSON payload and outputting the PNG image as a Base64 string.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on composite symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and related parameter classes to configure linear and 2D components. Developers building microservices that need to produce barcode images programmatically will find this pattern useful.
// Prompt: Develop a microservice that receives JSON payload and returns generated GS1 Composite barcode as PNG.
// Tags: gs1 composite, barcode generation, png, aspose.barcode, aspose.drawing, json

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 Composite barcode from a JSON payload
/// and outputs the resulting PNG image as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Deserializes a sample JSON payload,
    /// creates the barcode, saves it to disk, and prints the Base64 representation.
    /// </summary>
    static void Main()
    {
        // Sample JSON payload containing linear and 2D parts of the GS1 Composite barcode
        string json = "{\"linear\":\"(01)03212345678906\",\"twod\":\"(21)A1B2C3D4E5F6G7H8\"}";

        // Deserialize the JSON payload into a strongly‑typed object
        Payload? payload = JsonSerializer.Deserialize<Payload>(json);
        if (payload == null || string.IsNullOrEmpty(payload.Linear) || string.IsNullOrEmpty(payload.TwoD))
        {
            Console.WriteLine("Invalid payload");
            return;
        }

        // Combine linear and 2D parts using the '|' separator required for GS1 Composite barcodes
        string codetext = $"{payload.Linear}|{payload.TwoD}";

        // Initialize the barcode generator for GS1 Composite symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Set linear component type (GS1 Code128)
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set 2D component type (CC-A, a MicroPDF417 variant)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Configure aspect ratio for the 2D component (PDF417 settings)
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;

            // Set X‑Dimension for both linear and 2D components
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Set height of the linear component
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the barcode image as a PNG file
            string outputPath = "gs1composite.png";
            generator.Save(outputPath);

            // Generate the barcode image in memory and output its Base64 representation
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, Aspose.Drawing.Imaging.ImageFormat.Png);
                    string base64 = Convert.ToBase64String(ms.ToArray());
                    Console.WriteLine(base64);
                }
            }
        }
    }

    // Simple class representing the expected JSON structure
    private class Payload
    {
        public string Linear { get; set; }
        public string TwoD { get; set; }
    }
}