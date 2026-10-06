// Title: Generate GS1 Composite Barcode and Return as Base64 String
// Description: Demonstrates creating a GS1 Composite barcode from combined linear and 2‑D components and encoding the PNG image to a Base64 string for API responses.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.GS1CompositeBar, configure linear and 2‑D component types, and produce image output. Typical use cases include web services that need to return barcode images as Base64 strings for client‑side rendering. Developers often work with EncodeTypes, GS1CompositeBar parameters, and image format classes to meet these requirements.
// Prompt: Expose an API endpoint that receives combined CodeText and returns a GS1 Composite barcode as base64 string.
// Tags: gs1 composite, barcode generation, png, base64, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 Composite barcode and outputs it as a Base64‑encoded PNG string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds the combined CodeText, creates the barcode, and writes the Base64 string to the console.
    /// </summary>
    static void Main()
    {
        // In a real web service this method would accept a combined CodeText via HTTP.
        // Here we demonstrate the core logic in a console app and output the Base64 string.

        // Linear component (GS1-128) containing a GTIN‑14.
        string linearComponent = "(01)01234567890128";

        // 2‑D component (PDF417) containing a lot number.
        string twoDComponent = "(21)A12345678";

        // Combine the two components using the '|' separator required by GS1 Composite.
        string combinedCodeText = $"{linearComponent}|{twoDComponent}";

        // Initialize the barcode generator for GS1 Composite symbology with the combined text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
        {
            // Set the linear (1‑D) component type to GS1‑Code128.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set the 2‑D component type; CC_C uses a full PDF417 barcode.
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

            // Example PDF417 column setting for the 2‑D component (adjust for desired size).
            generator.Parameters.Barcode.Pdf417.Columns = 30;

            // Allow non‑GS1 data in the 2‑D component (useful for custom application identifiers).
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Render the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the PNG bytes to a Base64 string for easy transport over HTTP.
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Output the Base64 string (in a real API this would be the response body).
                Console.WriteLine(base64);
            }
        }
    }
}