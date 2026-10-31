// Title: Generate GS1 Composite barcode and return it as a Base64 string
// Description: Demonstrates how to create a GS1 Composite (linear + 2D) barcode using Aspose.BarCode, encode it to PNG, and convert the image to a Base64 string for API responses.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on composite symbologies such as GS1 Composite. It showcases the BarcodeGenerator class, EncodeTypes, and component configuration (linear and 2D) to produce a printable image. Developers building web APIs or services often need to generate barcodes on‑the‑fly and return them in a transport‑friendly format like Base64.
// Prompt: Expose an API endpoint that receives combined CodeText and returns a GS1 Composite barcode as base64 string.
// Tags: barcode, gs1, composite, base64, generation, aspose.barcode, encode-types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console example that generates a GS1 Composite barcode and outputs its Base64 representation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode and writes the Base64 string to the console.
    /// </summary>
    static void Main()
    {
        // Sample combined CodeText: linear component data | 2D component data
        string combinedCodeText = "(01)01234567890128|HelloWorld";

        // Generate the barcode image as a Base64 string
        string base64 = GenerateGs1CompositeBase64(combinedCodeText);

        // Output the result
        Console.WriteLine("Base64 GS1 Composite barcode:");
        Console.WriteLine(base64);
    }

    /// <summary>
    /// Generates a GS1 Composite barcode from a combined CodeText string and returns the PNG image as a Base64 string.
    /// </summary>
    /// <param name="combinedCodeText">A string containing linear and 2D component data separated by '|'.</param>
    /// <returns>Base64‑encoded PNG image of the generated barcode.</returns>
    static string GenerateGs1CompositeBase64(string combinedCodeText)
    {
        // Validate input format
        if (string.IsNullOrEmpty(combinedCodeText) || !combinedCodeText.Contains("|"))
            throw new ArgumentException("Combined CodeText must contain a '|' separator.");

        // Initialize the generator with GS1 Composite symbology and the combined text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
        {
            // Set linear component type to GS1 Code128
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set 2D component type to PDF417 (CC-C)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

            // Configure PDF417 columns for the 2D component
            generator.Parameters.Barcode.Pdf417.Columns = 30;

            // Allow non‑GS1 data in the 2D component if needed
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Render the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the PNG bytes to a Base64 string
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}