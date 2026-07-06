// Title: Generate UPC‑A barcode with GS1 Code128 coupon and embed in HTML
// Description: Demonstrates creating a UPC‑A barcode that includes a GS1 Code128 coupon, converting it to PNG, and embedding the image directly into an HTML page using a data URI.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.UpcaGs1Code128Coupon. It illustrates typical scenarios such as creating combined symbologies for retail packaging, generating barcode images, and embedding them in web content. Developers often need to produce barcode graphics for e‑commerce, inventory, or promotional coupons, and this snippet provides a concise reference.
// Prompt: Produce a UPC‑A barcode with a GS1 Code128 coupon, then embed the image into an HTML page.
// Tags: upc-a, gs1-code128, coupon, barcode generation, image embedding, html, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a UPC‑A barcode with a GS1 Code128 coupon and embedding it into an HTML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, converts it to a Base64 PNG, and writes an HTML file containing the image.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for UPC‑A with a GS1 Code128 coupon.
        // Example codetext: "514141100906(8102)03"
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, "514141100906(8102)03"))
        {
            // Generate the barcode image as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format.
                using (var memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    byte[] imageBytes = memoryStream.ToArray();

                    // Convert the PNG bytes to a Base64 string for embedding.
                    string base64Image = Convert.ToBase64String(imageBytes);

                    // Build a simple HTML page that embeds the barcode image using a data URI.
                    string htmlContent = $"<html><head><meta charset=\"UTF-8\"><title>UPC‑A GS1‑Code128 Coupon</title></head>" +
                                         $"<body><h2>UPC‑A with GS1‑Code128 Coupon</h2>" +
                                         $"<img src=\"data:image/png;base64,{base64Image}\" alt=\"Barcode\"/>" +
                                         $"</body></html>";

                    // Write the HTML content to a file named 'barcode.html'.
                    File.WriteAllText("barcode.html", htmlContent);
                }
            }
        }
    }
}