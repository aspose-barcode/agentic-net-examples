// Title: Generate QR Code and embed it into a Razor page using Aspose.BarCode
// Description: Demonstrates creating a QR Code image with Aspose.BarCode and writing a Razor view that displays the image via the ASP.NET Core image tag helper.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of the BarcodeGenerator class together with EncodeTypes.QR to produce QR Code images, a common requirement for web applications that need to display scannable data. Typical scenarios include embedding QR codes in Razor pages, emails, or PDFs, where developers often rely on the BarCodeImageFormat and QR error‑level settings.
// Prompt: Generate QR Code barcode and embed it into a Razor page using image tag helper.
// Tags: qr code, barcode generation, aspnet core, razor, image tag helper, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code image and writes a Razor page referencing the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, saves it as PNG, and creates a Razor view that displays the image using the ASP.NET Core image tag helper.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare the output directory where the image and Razor file will be saved.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // 2. Define the full file path for the QR Code image.
        // --------------------------------------------------------------------
        string qrImagePath = Path.Combine(outputDir, "qr.png");

        // --------------------------------------------------------------------
        // 3. Generate the QR Code barcode using Aspose.BarCode.
        //    - EncodeTypes.QR specifies the QR symbology.
        //    - "Hello, Aspose QR!" is the data encoded in the QR Code.
        //    - XDimension.Pixels controls the module size.
        //    - QRErrorLevel.LevelM sets a medium error correction level.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, Aspose QR!"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            generator.Save(qrImagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // 4. Create Razor page content that references the generated QR image.
        //    The asp-append-version attribute enables cache busting in ASP.NET Core.
        // --------------------------------------------------------------------
        string razorContent = @"@page
<img src=""~/images/qr.png"" asp-append-version=""true"" />";

        // --------------------------------------------------------------------
        // 5. Write the Razor page to the output directory.
        // --------------------------------------------------------------------
        string razorPath = Path.Combine(outputDir, "qrPage.cshtml");
        File.WriteAllText(razorPath, razorContent);

        // --------------------------------------------------------------------
        // 6. Inform the user where the files have been saved.
        // --------------------------------------------------------------------
        Console.WriteLine($"QR code image saved to: {qrImagePath}");
        Console.WriteLine($"Razor page saved to: {razorPath}");
    }
}