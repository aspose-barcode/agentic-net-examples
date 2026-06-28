using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a Swiss QR Code using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Generates a Swiss QR Code, converts it to PNG, and prints the Base64 representation.
    /// </summary>
    static void Main()
    {
        // NOTE: Full ASP.NET MVC integration cannot be demonstrated in this console application.
        // The core logic for generating a Swiss QR Code is shown below.
        // In an MVC controller, you would return the image bytes as a FileResult.

        // Create and populate the Swiss QR codetext.
        var swissQr = new SwissQRCodetext();
        // Set creditor details.
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        // Set account number (IBAN).
        swissQr.Bill.Account = "CH9300762011623852957";
        // Set payment amount.
        swissQr.Bill.Amount = 199.95m;
        // Set QR bill version.
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Generate the barcode image into a memory stream.
        using (var ms = new MemoryStream())
        {
            // Initialize the complex barcode generator with the Swiss QR data.
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                // Save the generated barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Retrieve the image bytes from the memory stream.
            byte[] imageBytes = ms.ToArray();

            // Convert the image bytes to a Base64 string for demonstration purposes.
            string base64 = Convert.ToBase64String(imageBytes);
            Console.WriteLine("Swiss QR Code (Base64 PNG):");
            Console.WriteLine(base64);
        }
    }
}