// Title: Generate UPC‑A Barcode with GS1 Code128 Coupon and Embed in HTML
// Description: Demonstrates creating a UPC‑A barcode that includes a GS1 Code128 coupon, saving it as PNG, and generating a simple HTML page that displays the barcode image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.UpcaGs1Code128Coupon to produce combined symbologies. Typical use cases include retail packaging where a UPC‑A product code is paired with a GS1‑128 coupon for promotions. Developers often need to customize dimensions, supplement spacing, and embed the resulting image into web pages or reports.
// Prompt: Produce a UPC‑A barcode with a GS1 Code128 coupon, then embed the image into an HTML page.
// Tags: barcode, upc-a, gs1-code128, coupon, image, html, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a UPC‑A barcode with a GS1 Code128 coupon,
/// saves it as a PNG image, and creates an HTML page that displays the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define and create the output directory for generated files.
        string outputDir = Path.Combine(Environment.CurrentDirectory, "BarcodeOutput");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the barcode image file.
        string imagePath = Path.Combine(outputDir, "upc_a_gs1_code128.png");

        // Generate the barcode using the UPC‑A with GS1 Code128 coupon symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, "123456789012(8110)ASPOSE"))
        {
            // Set the X‑dimension (module width) to 2 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Define the space (in pixels) between the UPC‑A and the coupon supplement.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 30;

            // Save the generated barcode as a PNG image.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Create a simple HTML page that references the generated barcode image.
        string htmlContent = $"<html><body><h1>UPC‑A with GS1 Code128 Coupon</h1><img src=\"{Path.GetFileName(imagePath)}\" alt=\"Barcode\"/></body></html>";
        string htmlPath = Path.Combine(outputDir, "barcode.html");
        File.WriteAllText(htmlPath, htmlContent);

        // Output the locations of the generated files to the console.
        Console.WriteLine($"Barcode image saved to: {imagePath}");
        Console.WriteLine($"HTML page saved to: {htmlPath}");
    }
}