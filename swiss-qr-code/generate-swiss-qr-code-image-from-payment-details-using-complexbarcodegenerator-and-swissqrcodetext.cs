// Title: Generate Swiss QR Code for payment using Aspose.BarCode ComplexBarcodeGenerator
// Description: Demonstrates how to create a Swiss QR Code image containing payment information such as account, amount, and creditor details, and save it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating the use of ComplexBarcodeGenerator with SwissQRCodetext to produce Swiss QR Bill codes. It shows how to configure QR code parameters, set payment data, and export the barcode image. Developers working with financial QR codes, invoicing, or payment processing can use these APIs to integrate Swiss QR Bill generation into .NET applications.
// Prompt: Generate a Swiss QR Code image from payment details using ComplexBarcodeGenerator and SwissQRCodetext.
// Tags: swiss qr code, payment, barcode generation, complexbarcode, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Swiss QR Code image for a payment bill using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates payment data, generates the QR code, and saves it as PNG.
    /// </summary>
    static void Main(string[] args)
    {
        // Define output folder and file path
        string outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDirectory);
        string outputPath = Path.Combine(outputDirectory, "SwissQRBill.png");

        // Create Swiss QR Code text and populate payment details
        var swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH4431999123000889012";
        swissQRCode.Bill.Amount = 1000.25m;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Reference = "210000000003139471430009017";

        // Set creditor address
        swissQRCode.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };

        // Set debtor address
        swissQRCode.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        try
        {
            // Initialize generator with the prepared Swiss QR Code text
            using (var generator = new ComplexBarcodeGenerator(swissQRCode))
            {
                // Configure QR code appearance and encoding
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Swiss QR Code image saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation
            Console.WriteLine($"Error generating Swiss QR Code: {ex.Message}");
        }
    }
}