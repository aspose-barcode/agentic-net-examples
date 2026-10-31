// Title: Generate Swiss QR Code PNG as byte array
// Description: Demonstrates creating a Swiss QR Code barcode using Aspose.BarCode, encoding payment data, and returning the PNG image as a byte array.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Swiss QR Code (QR‑bill) creation. It showcases the use of SwissQRCodetext, ComplexBarcodeGenerator, and related parameter settings to produce a QR‑bill image. Developers working with payment QR codes, invoicing, or financial document automation commonly need to generate such barcodes in .NET applications.
// Prompt: Create a .NET method that accepts parameters and returns a byte array of the Swiss QR Code PNG.
// Tags: swissqr, qr-bill, barcode generation, png output, aspose.barcode, complexbarcode, byte array

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss QR Code (QR‑Bill) and saving it as a PNG byte array.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds sample data, calls <see cref="GenerateSwissQrCode"/>, and writes the PNG to a temporary file.
    /// </summary>
    static void Main()
    {
        // Sample data for Swiss QR Code
        string account = "CH4431999123000889012";
        decimal amount = 1000.25m;
        string creditorName = "Muster & Söhne";
        string creditorCountry = "CH";
        string creditorStreet = "Musterstrasse";
        string creditorHouseNo = "12b";
        string creditorPostal = "8200";
        string creditorTown = "Zürich";
        string debtorName = "Muster AG";
        string debtorCountry = "CH";
        string debtorStreet = "Musterstrasse";
        string debtorHouseNo = "1";
        string debtorPostal = "3030";
        string debtorTown = "Bern";

        // Generate the QR code image as a byte array
        byte[] pngBytes = GenerateSwissQrCode(
            account,
            amount,
            creditorName,
            creditorCountry,
            creditorStreet,
            creditorHouseNo,
            creditorPostal,
            creditorTown,
            debtorName,
            debtorCountry,
            debtorStreet,
            debtorHouseNo,
            debtorPostal,
            debtorTown);

        // Write the PNG bytes to a temporary file for verification
        string outputPath = Path.Combine(Path.GetTempPath(), "SwissQR.png");
        using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            file.Write(pngBytes, 0, pngBytes.Length);
        }

        Console.WriteLine($"Swiss QR Code PNG generated: {outputPath}");
        Console.WriteLine($"Byte array length: {pngBytes.Length}");
    }

    /// <summary>
    /// Generates a Swiss QR Code (QR‑Bill) PNG image and returns it as a byte array.
    /// </summary>
    /// <param name="account">IBAN account number.</param>
    /// <param name="amount">Payment amount.</param>
    /// <param name="creditorName">Creditor's name.</param>
    /// <param name="creditorCountryCode">Creditor's country code (ISO 3166‑1 alpha‑2).</param>
    /// <param name="creditorStreet">Creditor's street name.</param>
    /// <param name="creditorHouseNo">Creditor's house number.</param>
    /// <param name="creditorPostalCode">Creditor's postal code.</param>
    /// <param name="creditorTown">Creditor's town/city.</param>
    /// <param name="debtorName">Debtor's name.</param>
    /// <param name="debtorCountryCode">Debtor's country code (ISO 3166‑1 alpha‑2).</param>
    /// <param name="debtorStreet">Debtor's street name.</param>
    /// <param name="debtorHouseNo">Debtor's house number.</param>
    /// <param name="debtorPostalCode">Debtor's postal code.</param>
    /// <param name="debtorTown">Debtor's town/city.</param>
    /// <returns>Byte array containing the PNG representation of the Swiss QR Code.</returns>
    static byte[] GenerateSwissQrCode(
        string account,
        decimal amount,
        string creditorName,
        string creditorCountryCode,
        string creditorStreet,
        string creditorHouseNo,
        string creditorPostalCode,
        string creditorTown,
        string debtorName,
        string debtorCountryCode,
        string debtorStreet,
        string debtorHouseNo,
        string debtorPostalCode,
        string debtorTown)
    {
        // Build Swiss QR Code data structure
        SwissQRCodetext swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = account;
        swissQr.Bill.Amount = amount;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";

        // Populate creditor address
        swissQr.Bill.Creditor = new Address
        {
            Name = creditorName,
            Street = creditorStreet,
            HouseNo = creditorHouseNo,
            PostalCode = creditorPostalCode,
            Town = creditorTown,
            CountryCode = creditorCountryCode
        };

        // Populate debtor address
        swissQr.Bill.Debtor = new Address
        {
            Name = debtorName,
            Street = debtorStreet,
            HouseNo = debtorHouseNo,
            PostalCode = debtorPostalCode,
            Town = debtorTown,
            CountryCode = debtorCountryCode
        };

        // Generate barcode image into memory
        using (MemoryStream ms = new MemoryStream())
        {
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissQr))
            {
                // Optional visual settings
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

                // Save the generated QR code as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Return the PNG data as a byte array
            return ms.ToArray();
        }
    }
}