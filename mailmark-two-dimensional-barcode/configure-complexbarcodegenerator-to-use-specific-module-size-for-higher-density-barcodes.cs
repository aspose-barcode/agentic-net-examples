// Title: Generate a high‑density Swiss QR barcode using ComplexBarcodeGenerator
// Description: Demonstrates configuring the module size of a Swiss QR barcode for higher density and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use ComplexBarcodeGenerator with SwissQRCodetext, adjust XDimension for density, and customize colors. Developers working with Swiss QR codes, QR‑Bill standards, or any complex barcode formats can reference this pattern for creating high‑resolution images.
// Prompt: Configure ComplexBarcodeGenerator to use a specific module size for higher density barcodes.
// Tags: swissqr, complexbarcode, module-size, high-density, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a high‑density Swiss QR barcode using Aspose.BarCode's ComplexBarcodeGenerator.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a Swiss QR codetext, configures a small XDimension for higher density,
    /// sets visual parameters, and saves the barcode as a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "ComplexBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "SwissQR_HighDensity.png");

        // Construct Swiss QR codetext with required fields for a QR‑Bill
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create ComplexBarcodeGenerator using the codetext object
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set a small module size (XDimension) for higher barcode density
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Optional visual settings: black barcode on white background
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}