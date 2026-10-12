// Title: Generate QR Code and embed in Razor page using image tag helper
// Description: This example creates a QR Code barcode image and writes a simple Razor view that displays the image using the ASP.NET Core image tag helper.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for web applications. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce a QR Code, then embeds the resulting PNG in a Razor page. Developers building ASP.NET Core sites often need to generate barcodes on‑the‑fly and render them in views; this snippet provides a clear, reusable pattern for that scenario.
// Prompt: Generate QR Code barcode and embed it into a Razor page using image tag helper.
// Tags: qr code, barcode generation, aspnet core, razor, image tag helper, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a QR Code barcode image and embed it into a Razor page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, saves it as PNG, and creates a Razor view that references the image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeQrDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the QR image and the Razor page
        string qrImagePath = Path.Combine(tempFolder, "qr.png");
        string razorPagePath = Path.Combine(tempFolder, "qrPage.cshtml");

        // Generate QR Code barcode and save it as a PNG image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the size of each module (pixel) in the QR code
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(qrImagePath, BarCodeImageFormat.Png);
        }

        // Build Razor page content that displays the QR image using the image tag helper
        string razorContent = @"@{
    Layout = null;
}
<img src=""qr.png"" alt=""QR Code"" />";

        // Write the Razor page to the temporary folder
        File.WriteAllText(razorPagePath, razorContent);

        // Output the locations of the generated files
        Console.WriteLine("QR code image saved to: " + qrImagePath);
        Console.WriteLine("Razor page saved to: " + razorPagePath);
    }
}