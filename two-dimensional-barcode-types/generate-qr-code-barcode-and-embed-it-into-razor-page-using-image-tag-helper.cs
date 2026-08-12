// Title: Generate QR Code and embed in Razor page using image tag helper
// Description: This example creates a QR Code barcode, saves it as a PNG image, and generates a Razor view that displays the image with ASP.NET Core's image tag helper.
// Category-Description: Demonstrates Aspose.BarCode barcode generation (specifically QR Code) and how to integrate the resulting image into a web application using Razor syntax. The example utilizes BarcodeGenerator, EncodeTypes, QRErrorLevel, and BarCodeImageFormat classes to produce a PNG file, then writes a .cshtml file that references the image via the ASP.NET Core image tag helper. Ideal for developers needing to add dynamic barcodes to MVC or Razor Pages projects.
// Prompt: Generate QR Code barcode and embed it into a Razor page using image tag helper.
// Tags: qr code, barcode generation, aspnet core, razor, image tag helper, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR Code generation with Aspose.BarCode and creates a Razor page that displays the barcode using the ASP.NET Core image tag helper.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR Code image, writes a Razor view referencing the image, and outputs file locations.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated QR code image
        string qrImagePath = Path.Combine(tempFolder, "qr.png");

        // Generate a QR Code barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a medium error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the barcode image directly to file
            generator.Save(qrImagePath, BarCodeImageFormat.Png);
        }

        // Create a simple Razor page that uses the ASP.NET Core image tag helper
        // Note: In a real ASP.NET Core project the image would be placed under wwwroot and the tag helper would resolve the URL.
        // Here we just generate the .cshtml file to illustrate the required markup.
        string razorPagePath = Path.Combine(tempFolder, "QrPage.cshtml");
        string razorContent = @"@page
<img src=""~/images/qr.png"" asp-append-version=""true"" />";

        File.WriteAllText(razorPagePath, razorContent);

        // Output the locations of the generated files
        Console.WriteLine("QR code image saved to: " + qrImagePath);
        Console.WriteLine("Razor page saved to: " + razorPagePath);
        Console.WriteLine("Demo completed successfully.");
    }
}