// Title: Generate Swiss QR Code with Maximum Error Correction
// Description: Demonstrates creating a Swiss QR bill barcode using Aspose.BarCode with the highest QR error correction level to improve readability on low‑quality prints.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and QR error correction settings. Developers working with payment QR codes, such as Swiss QR bills, often need to adjust error correction levels to ensure reliable scanning under suboptimal printing conditions. The snippet illustrates typical steps: preparing codetext, configuring generator parameters, and saving the image.
// Prompt: Set ComplexBarcodeGenerator ErrorCorrectionLevel to maximum to boost Reed‑Solomon redundancy for low‑quality prints.
// Tags: barcode, complex barcode, swiss qr, error correction, qr, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Swiss QR bill barcode with the highest QR error correction level.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Prepares Swiss QR codetext, configures the generator, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare Swiss QR codetext with creditor and payment details
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create a ComplexBarcodeGenerator using the prepared codetext
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set the QR error correction level to the maximum (Level H) for better redundancy
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optionally adjust the module size (X dimension) for visual clarity
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Define the output file path and save the barcode as a PNG image
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR_MaxError.png");
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the image was saved
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
    }
}