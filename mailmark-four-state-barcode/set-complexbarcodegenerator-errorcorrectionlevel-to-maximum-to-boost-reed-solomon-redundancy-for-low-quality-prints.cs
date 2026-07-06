// Title: Generate Swiss QR Bill barcode with maximum Reed‑Solomon error correction
// Description: Demonstrates how to create a Swiss QR Bill barcode and set the QR error correction level to the highest (Level H) to improve readability on low‑quality prints.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on QR code creation for financial documents. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and QR error‑correction settings, which are common tasks for developers implementing payment QR codes, invoices, or other high‑density data barcodes.
// Prompt: Set ComplexBarcodeGenerator ErrorCorrectionLevel to maximum to boost Reed‑Solomon redundancy for low‑quality prints.
// Tags: swiss qr, barcode generation, error correction, qr, complexbarcode, aspnet, aspnetcore, aspnet5, aspose.barcode

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the Swiss QR Bill barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Swiss QR Bill barcode with maximum QR error correction (Level H) and saves it as an image.
    /// </summary>
    static void Main()
    {
        // Prepare Swiss QR codetext with mandatory fields
        var swissQr = new SwissQRCodetext();

        // Set creditor details
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";

        // Set account number (IBAN)
        swissQr.Bill.Account = "CH9300762011623852957";

        // Set payment amount
        swissQr.Bill.Amount = 199.95m;

        // Use QR Bill standard version 2.0
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create ComplexBarcodeGenerator and configure maximum QR error correction (Level H)
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set Reed‑Solomon error correction to the highest level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode image to a PNG file
            generator.Save("SwissQR.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("Swiss QR barcode generated with maximum error correction.");
    }
}