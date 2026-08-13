// Title: Generate GS1 Composite barcode and output as Base64 PNG
// Description: This example creates a GS1 Composite barcode from a combined CodeText string and returns the image encoded as a Base64 PNG. It shows how to configure linear and 2D components using Aspose.BarCode.
// Category-Description: Demonstrates Aspose.BarCode generation of composite symbologies, focusing on GS1 Composite Bar. The example uses BarcodeGenerator, EncodeTypes, and TwoDComponentType to set up linear (GS1Code128) and 2D (CC-A) components, then saves the image as PNG. Useful for developers needing to embed barcode images in web responses or APIs.
// Prompt: Expose an API endpoint that receives combined CodeText and returns a GS1 Composite barcode as base64 string.
// Tags: barcode, gs1 composite, generation, base64, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a GS1 Composite barcode from a combined CodeText
/// and obtain the resulting PNG image as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application. Generates a barcode and writes the Base64 string to the console.
    /// </summary>
    static void Main()
    {
        // Sample combined CodeText for GS1 Composite barcode (linear|2D)
        string combinedCodeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Generate the barcode image and retrieve it as a Base64-encoded PNG
        string base64Barcode = GenerateGs1CompositeBarcodeBase64(combinedCodeText);

        // Output the Base64 string to the console
        Console.WriteLine("Base64 PNG of GS1 Composite barcode:");
        Console.WriteLine(base64Barcode);
    }

    /// <summary>
    /// Generates a GS1 Composite barcode image from the provided combined CodeText
    /// and returns the image as a Base64-encoded PNG string.
    /// </summary>
    /// <param name="combinedCodeText">Combined linear and 2D components separated by '|'.</param>
    /// <returns>Base64 string representing the PNG image.</returns>
    private static string GenerateGs1CompositeBarcodeBase64(string combinedCodeText)
    {
        // Validate input
        if (string.IsNullOrEmpty(combinedCodeText))
            throw new ArgumentException("CodeText cannot be null or empty.", nameof(combinedCodeText));

        // Initialize the barcode generator for GS1 Composite Bar with the combined CodeText
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
        {
            // Set the linear component to GS1 Code128
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set the 2D component to Composite Component (CC) type A
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional visual settings
            generator.Parameters.Barcode.XDimension.Pixels = 3f;      // Module size (pixel width of the smallest bar)
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;   // Height of the linear component
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;   // Aspect ratio for the 2D component (PDF417 based)

            // Render the barcode to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                byte[] imageBytes = memoryStream.ToArray();

                // Convert the PNG byte array to a Base64 string
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}