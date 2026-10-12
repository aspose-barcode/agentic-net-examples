// Title: Generate UPC-A barcode with GS1 Code128 coupon and embed in HTML
// Description: Demonstrates creating a UPC‑A barcode that includes a GS1 Code128 coupon, saving it as a PNG, and embedding the image into a simple HTML page.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.UpcaGs1Code128Coupon. It illustrates typical tasks such as setting barcode dimensions, saving to image formats, and integrating the result into web content—common needs for developers building e‑commerce, inventory, or promotional applications.
// Prompt: Produce a UPC‑A barcode with a GS1 Code128 coupon, then embed the image into an HTML page.
// Tags: upc-a, gs1, code128, coupon, barcode generation, image output, html embedding, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a UPC‑A barcode with a GS1 Code128 coupon,
/// saves it as a PNG image, and generates an HTML page that displays the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Determine the current working directory to store output files.
        string outputDir = Directory.GetCurrentDirectory();

        // Build full file paths for the barcode image and the HTML page.
        string imagePath = Path.Combine(outputDir, "barcode.png");
        string htmlPath = Path.Combine(outputDir, "barcode.html");

        // Create a barcode generator for UPC‑A with a GS1 Code128 coupon.
        // The data string includes the UPC‑A digits and the coupon application identifier (10) followed by the coupon text.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, "123456789012(10)ABC"))
        {
            // Set the X‑dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG image.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Prepare a minimal HTML document that references the saved barcode image.
        string htmlContent = "<!DOCTYPE html>\n<html>\n<head>\n<title>Barcode</title>\n</head>\n<body>\n<h1>UPC-A with GS1 Code128 Coupon</h1>\n<img src=\"barcode.png\" alt=\"Barcode\" />\n</body>\n</html>";

        // Write the HTML content to a file in the output directory.
        File.WriteAllText(htmlPath, htmlContent);

        // Inform the user where the files have been saved.
        Console.WriteLine("Barcode image saved to: " + imagePath);
        Console.WriteLine("HTML page saved to: " + htmlPath);
    }
}