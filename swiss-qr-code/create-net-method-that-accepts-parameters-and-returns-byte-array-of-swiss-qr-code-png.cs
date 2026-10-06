// Title: Generate Swiss QR Code as PNG Byte Array
// Description: Demonstrates how to create a Swiss QR Bill barcode using Aspose.BarCode and return the image as a PNG byte array.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Bills. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related parameter settings to produce payment QR codes. Developers commonly need to generate QR codes for invoicing, banking, or mobile payments, and this pattern illustrates the typical workflow for creating, configuring, and exporting the barcode image.
// Prompt: Create a .NET method that accepts parameters and returns a byte array of the Swiss QR Code PNG.
// Tags: swiss qr code, barcode generation, png output, aspose.barcode, complexbarcodegenerator, swissqr, payment barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides functionality to generate a Swiss QR Code barcode and save it as a PNG byte array.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Swiss QR Bill barcode image in PNG format and returns it as a byte array.
    /// </summary>
    /// <param name="account">The IBAN account number.</param>
    /// <param name="amount">The payment amount.</param>
    /// <param name="creditorName">Name of the creditor.</param>
    /// <param name="creditorCountryCode">ISO country code of the creditor.</param>
    /// <param name="debtorName">Name of the debtor.</param>
    /// <param name="debtorCountryCode">ISO country code of the debtor.</param>
    /// <param name="reference">Payment reference string.</param>
    /// <param name="currency">Currency code (default is CHF).</param>
    /// <returns>Byte array containing the PNG image of the generated Swiss QR Code.</returns>
    static byte[] GenerateSwissQrCode(string account, decimal amount, string creditorName, string creditorCountryCode, string debtorName, string debtorCountryCode, string reference, string currency = "CHF")
    {
        // Initialize Swiss QR Code text object and set bill details
        var swiss = new SwissQRCodetext();
        swiss.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swiss.Bill.Account = account;
        swiss.Bill.Amount = amount;
        swiss.Bill.Currency = currency;
        swiss.Bill.Reference = reference;
        swiss.Bill.Creditor = new Address
        {
            Name = creditorName,
            CountryCode = creditorCountryCode
        };
        swiss.Bill.Debtor = new Address
        {
            Name = debtorName,
            CountryCode = debtorCountryCode
        };

        // Create a barcode generator for the Swiss QR Code
        using (var generator = new ComplexBarcodeGenerator(swiss))
        {
            // Configure visual parameters: module size and QR encoding settings
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Render the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the generated image as a byte array
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Entry point of the program. Generates a Swiss QR Code PNG and writes it to a temporary file.
    /// </summary>
    static void Main()
    {
        // Sample data for the Swiss QR Bill
        string account = "CH4431999123000889012";
        decimal amount = 1000.25m;
        string creditorName = "Muster & Söhne";
        string creditorCountry = "CH";
        string debtorName = "Muster AG";
        string debtorCountry = "CH";
        string reference = "210000000003139471430009017";

        // Generate the PNG byte array
        byte[] pngBytes = GenerateSwissQrCode(account, amount, creditorName, creditorCountry, debtorName, debtorCountry, reference);

        // Save the PNG to a temporary location for verification
        string outputPath = Path.Combine(Path.GetTempPath(), "SwissQR.png");
        File.WriteAllBytes(outputPath, pngBytes);

        // Inform the user about the output location and size
        Console.WriteLine($"Swiss QR Code PNG saved to: {outputPath}");
        Console.WriteLine($"Byte array length: {pngBytes.Length}");
    }
}