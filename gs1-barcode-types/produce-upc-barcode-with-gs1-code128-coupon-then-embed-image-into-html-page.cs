using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a UPC‑A with GS1‑Code128 coupon barcode,
/// saving it as an image, and embedding it in a simple HTML page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, converts it to Base64, creates an HTML file,
    /// and writes both the image and HTML to disk.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define output file names for the image and HTML page.
        string imagePath = "barcode.png";
        string htmlPath = "barcode.html";

        // UPC‑A part + GS1‑Code128 coupon part to encode.
        string codeText = "514141100906(8102)03";

        // Generate the barcode image using Aspose.BarCode.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, codeText))
        {
            // Save the generated barcode as a PNG file (default format).
            generator.Save(imagePath);
        }

        // Read the saved PNG file into a byte array.
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        // Convert the image bytes to a Base64 string for embedding in HTML.
        string base64Image = Convert.ToBase64String(imageBytes);

        // Construct a minimal HTML document that displays the barcode image.
        string htmlContent = $"<html><head><title>UPC‑A with GS1‑Code128 Coupon</title></head>" +
                             $"<body><h2>Barcode</h2>" +
                             $"<img src=\"data:image/png;base64,{base64Image}\" alt=\"Barcode\"/>" +
                             $"</body></html>";

        // Write the HTML content to the specified file.
        File.WriteAllText(htmlPath, htmlContent);

        // Output the locations of the generated files to the console.
        Console.WriteLine($"Barcode image saved to: {imagePath}");
        Console.WriteLine($"HTML page saved to: {htmlPath}");
    }
}