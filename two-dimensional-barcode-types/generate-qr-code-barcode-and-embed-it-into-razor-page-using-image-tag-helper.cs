// Title: Generate QR Code and embed in Razor page using image tag helper
// Description: This example creates a QR Code barcode image and writes a simple Razor view that displays the image with the ASP.NET Core image tag helper.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for web applications. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce a QR Code, then shows how to reference the generated image in a Razor page via the asp-append-version image tag helper. Typical for developers needing dynamic barcode images in ASP.NET Core MVC or Razor Pages.
// Prompt: Generate QR Code barcode and embed it into a Razor page using image tag helper.
// Tags: qr code, barcode generation, asp.net core, razor, image tag helper, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode image and creating a Razor page that references it using the ASP.NET Core image tag helper.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR code, saves image, writes Razor view, and outputs file locations.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeQrDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define paths for the QR image and the Razor page
        string qrImagePath = Path.Combine(outputFolder, "qr.png");
        string razorPagePath = Path.Combine(outputFolder, "QrPage.cshtml");

        // Generate QR Code barcode and save it as a PNG image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the size of each QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Set error correction level to Medium
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            // Save the generated QR code to the specified file
            generator.Save(qrImagePath, BarCodeImageFormat.Png);
        }

        // Create Razor page content that uses the ASP.NET Core image tag helper to display the QR code
        string razorContent = @"@{
    Layout = null;
}
<img src=""~/qr.png"" asp-append-version=""true"" alt=""QR Code"" />";

        // Write the Razor view file to disk
        File.WriteAllText(razorPagePath, razorContent);

        // Output the locations of the generated files
        Console.WriteLine("QR code image saved to: " + qrImagePath);
        Console.WriteLine("Razor page saved to: " + razorPagePath);
    }
}