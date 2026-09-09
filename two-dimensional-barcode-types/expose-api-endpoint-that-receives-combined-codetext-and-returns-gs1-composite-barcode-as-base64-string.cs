// Title: Generate GS1 Composite barcode and return Base64 PNG
// Description: Demonstrates creating a GS1 Composite barcode from a combined linear and 2D code text string and encoding the resulting PNG image as a Base64 string.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on GS1 Composite symbology. It uses BarcodeGenerator with EncodeTypes.GS1CompositeBar, configures linear and 2D component types, and shows how to customize component settings such as PDF417 columns. Developers building barcode services or APIs often need to generate composite barcodes and deliver them in web‑friendly formats like Base64 strings.
// Prompt: Expose an API endpoint that receives combined CodeText and returns a GS1 Composite barcode as base64 string.
// Tags: gs1 composite, barcode generation, base64, png, aspose.barcode, encode types, api endpoint

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a console example that generates a GS1 Composite barcode and outputs it as a Base64‑encoded PNG string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds sample combined code text, generates the barcode, and writes the Base64 string to the console.
    /// </summary>
    static void Main()
    {
        // Sample combined CodeText: linear part | 2D part
        string linearPart = "(01)12345678901231";
        string twoDPart = "(01)00123456789012";
        string combinedCodeText = $"{linearPart}|{twoDPart}";

        // Generate the barcode image and obtain its Base64 representation
        string base64Image = GenerateGs1CompositeBase64(combinedCodeText);
        Console.WriteLine("Base64 PNG of GS1 Composite barcode:");
        Console.WriteLine(base64Image);
    }

    /// <summary>
    /// Generates a GS1 Composite barcode from the provided combined code text and returns the PNG image as a Base64 string.
    /// </summary>
    /// <param name="combinedCodeText">The combined linear and 2D component data, separated by a pipe character.</param>
    /// <returns>Base64‑encoded PNG image of the generated barcode.</returns>
    static string GenerateGs1CompositeBase64(string combinedCodeText)
    {
        // Initialize the generator with GS1 Composite symbology and the combined code text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
        {
            // Configure the linear component to use GS1‑Code128 encoding
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Configure the 2D component to use CC‑C (Composite Component) type
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

            // Allow non‑GS1 data in the 2D component (optional, set to false to enforce GS1 only)
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Example: set PDF417 column count for the CC‑C component
            generator.Parameters.Barcode.Pdf417.Columns = 30;

            // Render the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the image bytes to a Base64 string
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}