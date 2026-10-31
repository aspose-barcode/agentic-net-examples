// Title: Generate UPC‑A Barcode with GS1 Code128 Coupon and Embed in HTML
// Description: This example creates a UPC‑A barcode that includes a GS1 Code128 coupon, saves it as a PNG image, and then generates a simple HTML page that displays the barcode.
// Category-Description: Demonstrates Aspose.BarCode generation of composite symbologies (UPC‑A with GS1 Code128 coupon) using the BarcodeGenerator class. Typical use cases include retail product labeling with promotional coupons. Developers often need to configure barcode dimensions, coupon spacing, and embed the resulting image into web content; this example shows those steps.
// Prompt: Produce a UPC‑A barcode with a GS1 Code128 coupon, then embed the image into an HTML page.
// Tags: barcode, upc-a, gs1, code128, coupon, image, html, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a UPC‑A barcode with a GS1 Code128 coupon and embedding it into an HTML page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode image, creates an HTML file, and writes output paths to the console.
    /// </summary>
    static void Main()
    {
        // Define file paths for the barcode image and the resulting HTML page
        string imagePath = "upc_a_coupon.png";
        string htmlPath = "barcode.html";

        // Generate UPC‑A barcode with an embedded GS1 Code128 coupon
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, "123456789012(8102)03"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Allocate extra space (30 pixels) for the coupon supplement area
            generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 30;

            // Save the generated barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Build a minimal HTML document that references the generated barcode image
        string htmlContent = $"<!DOCTYPE html><html><head><meta charset=\"UTF-8\"><title>UPC-A with GS1 Code128 Coupon</title></head><body><h1>UPC-A with GS1 Code128 Coupon</h1><img src=\"{imagePath}\" alt=\"Barcode\"/></body></html>";

        // Write the HTML content to a file
        File.WriteAllText(htmlPath, htmlContent);

        // Output the absolute paths of the created files for user reference
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(imagePath)}");
        Console.WriteLine($"HTML page saved to: {Path.GetFullPath(htmlPath)}");
    }
}