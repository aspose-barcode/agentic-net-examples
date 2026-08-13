// Title: Generate Swiss QR Code PNG as Byte Array
// Description: Demonstrates how to create a Swiss QR Code barcode and obtain its PNG representation as a byte array.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and SwissQRCodetext to produce Swiss QR Code (QR‑Bill) barcodes, a common requirement for Swiss payment processing. Developers often need to generate QR‑Bill images in PNG format for embedding in invoices or digital documents.
// Prompt: Create a .NET method that accepts parameters and returns a byte array of the Swiss QR Code PNG.
// Tags: swiss qr code, generation, png, complexbarcodegenerator, swissqrcodetext

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides functionality to generate a Swiss QR Code (QR‑Bill) and save it as a PNG byte array.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Swiss QR Code PNG and returns it as a byte array.
    /// </summary>
    /// <param name="creditorName">Name of the creditor.</param>
    /// <param name="creditorCountryCode">Two‑letter ISO country code of the creditor (e.g., "CH").</param>
    /// <param name="account">IBAN of the creditor's account.</param>
    /// <param name="amount">Payment amount in CHF.</param>
    /// <param name="reference">Optional payment reference (e.g., QR‑Reference or RF‑Reference).</param>
    /// <param name="billInformation">Optional additional bill information.</param>
    /// <returns>Byte array containing the PNG image of the generated Swiss QR Code.</returns>
    static byte[] CreateSwissQrCode(
        string creditorName,
        string creditorCountryCode,
        string account,
        decimal amount,
        string reference = null,
        string billInformation = null)
    {
        // Prepare Swiss QR code data structure.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = creditorName;
        swissQr.Bill.Creditor.CountryCode = creditorCountryCode;
        swissQr.Bill.Account = account;
        swissQr.Bill.Amount = amount;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Currency = "CHF";

        // Optional fields: reference and additional bill information.
        if (!string.IsNullOrEmpty(reference))
        {
            swissQr.Bill.Reference = reference;
        }

        if (!string.IsNullOrEmpty(billInformation))
        {
            swissQr.Bill.BillInformation = billInformation;
        }

        // Generate the barcode and write it to a memory stream.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                return ms.ToArray(); // Return the PNG bytes.
            }
        }
    }

    /// <summary>
    /// Entry point of the example. Generates a sample Swiss QR Code PNG and saves it to disk.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Sample data for the Swiss QR Code.
        string name = "John Doe";
        string country = "CH";
        string account = "CH9300762011623852957";
        decimal amount = 199.95m;
        string reference = "RF18539007547034";
        string billInfo = "Invoice 12345";

        // Generate the PNG bytes using the helper method.
        byte[] pngBytes = CreateSwissQrCode(name, country, account, amount, reference, billInfo);

        // Save the PNG to a file for verification.
        File.WriteAllBytes("SwissQR.png", pngBytes);
        Console.WriteLine($"Swiss QR Code generated ({pngBytes.Length} bytes) and saved as SwissQR.png");
    }
}