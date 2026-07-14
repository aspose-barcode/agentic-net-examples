// Title: Generate Swiss QR Code PNG as byte array
// Description: Demonstrates creating a Swiss QR Code barcode and returning the PNG image as a byte array.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Code. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and BarCodeImageFormat to produce PNG output, a common requirement for payment QR codes in financial applications.
// Prompt: Create a .NET method that accepts parameters and returns a byte array of the Swiss QR Code PNG.
// Tags: swiss qr code, barcode generation, png output, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Swiss QR Code barcode and returns the PNG image as a byte array.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Swiss QR Code PNG and returns it as a byte array.
    /// </summary>
    /// <param name="creditorName">Name of the creditor (mandatory).</param>
    /// <param name="creditorCountryCode">ISO country code of the creditor (e.g., "CH").</param>
    /// <param name="account">IBAN account number (must be a valid Swiss IBAN).</param>
    /// <param name="amount">Invoice amount.</param>
    /// <returns>Byte array containing the PNG image of the generated Swiss QR Code.</returns>
    public static byte[] GenerateSwissQrCode(string creditorName, string creditorCountryCode, string account, decimal amount)
    {
        // Prepare the Swiss QR Code data structure.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = creditorName;
        swissQr.Bill.Creditor.CountryCode = creditorCountryCode;
        swissQr.Bill.Account = account;
        swissQr.Bill.Amount = amount;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Generate the barcode image into a memory stream.
        using (var memoryStream = new MemoryStream())
        {
            // ComplexBarcodeGenerator creates the QR code based on the provided data.
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                // Save the generated barcode as PNG into the memory stream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Return the PNG bytes from the memory stream.
            return memoryStream.ToArray();
        }
    }

    /// <summary>
    /// Entry point that calls <see cref="GenerateSwissQrCode"/> with sample data and writes the result length to console.
    /// </summary>
    static void Main()
    {
        // Sample data for demonstration purposes.
        var pngBytes = GenerateSwissQrCode(
            creditorName: "John Doe",
            creditorCountryCode: "CH",
            account: "CH9300762011623852957",
            amount: 199.95m);

        // Output the size of the generated PNG byte array.
        Console.WriteLine($"Generated Swiss QR Code PNG byte array length: {pngBytes.Length}");
    }
}