// Title: Generate GS1 Composite Barcode and Return Base64
// Description: Demonstrates creating a GS1 Composite barcode from combined CodeText and encoding the image as a Base64 string, suitable for API responses.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.GS1CompositeBar, configure linear and 2D components, and output the result in PNG format. Developers working on barcode creation services often need to generate composite symbologies, adjust dimensions, and return image data as Base64 for web APIs. The snippet showcases key classes such as BarcodeGenerator, EncodeTypes, TwoDComponentType, and BarCodeImageFormat.
// Prompt: Expose an API endpoint that receives combined CodeText and returns a GS1 Composite barcode as base64 string.
// Tags: barcode symbology, generation, png, base64, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 Composite barcode from combined CodeText
/// and outputs the barcode image as a Base64‑encoded PNG string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Simulates receiving combined CodeText,
    /// creates the barcode, and writes the Base64 string to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument can be the combined CodeText.</param>
    static void Main(string[] args)
    {
        // Determine the combined CodeText: use first argument if provided, otherwise use a default sample.
        string combinedCodeText = args.Length > 0
            ? args[0]
            : "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Initialize the barcode generator for GS1 Composite symbology with the combined CodeText.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
        {
            // Set the linear component to GS1‑Code128 and the 2D component to CC‑A.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional: adjust additional visual settings.
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;          // Aspect ratio of the 2D component.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;          // X‑dimension for both components.
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;        // Height of the linear component.

            // Render the barcode into a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the PNG byte array to a Base64 string for transmission.
                string base64 = Convert.ToBase64String(imageBytes);

                // Output the Base64 string (in a real API this would be the HTTP response body).
                Console.WriteLine(base64);
            }
        }
    }
}