// Title: Decode Swiss QR Code using ComplexCodetextReader
// Description: Demonstrates generating a Swiss QR Code barcode, saving it to an image, and decoding it back into a SwissQRCodetext object for data extraction.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to create a Swiss QR Code, BarCodeReader to scan the image, and ComplexCodetextReader to parse the raw QR text into a strongly‑typed SwissQRCodetext object. Developers working with payment QR codes, QR‑based invoices, or any Swiss QR standard will find these APIs essential for creating and reading QR data in .NET applications.
// Prompt: Use ComplexCodetextReader.TryDecodeSwissQR to parse raw text into a SwissQRCodetext object for extraction.
// Tags: swissqr, barcode, generation, recognition, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Sample program that creates a Swiss QR Code, saves it as an image, reads it back,
/// and extracts the embedded payment information using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "SwissQR.png");

        // ---------------------------------------------------------------
        // 2. Build the Swiss QR Code data model (payment information).
        // ---------------------------------------------------------------
        var swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH4431999123000889012";
        swissQRCode.Bill.Amount = 1000.25m;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Reference = "210000000003139471430009017";
        swissQRCode.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };
        swissQRCode.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // ---------------------------------------------------------------
        // 3. Generate the barcode image using ComplexBarcodeGenerator.
        // ---------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            // Set visual parameters: module size and QR encoding mode.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated image to the temporary path.
            generator.Save(imagePath);
        }

        // ---------------------------------------------------------------
        // 4. Read and decode the barcode from the saved image.
        // ---------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Try to parse the raw QR text into a strongly‑typed SwissQRCodetext object.
                SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Failed to decode Swiss QR Code.");
                    continue;
                }

                // Output the extracted payment details.
                Console.WriteLine($"Version: {decoded.Bill.Version}");
                Console.WriteLine($"Account: {decoded.Bill.Account}");
                Console.WriteLine($"Amount: {decoded.Bill.Amount}");
                Console.WriteLine($"Currency: {decoded.Bill.Currency}");
                Console.WriteLine($"Reference: {decoded.Bill.Reference}");
                Console.WriteLine($"Creditor: {decoded.Bill.Creditor.Name}");
                Console.WriteLine($"Debtor: {decoded.Bill.Debtor.Name}");
            }
        }

        // ---------------------------------------------------------------
        // 5. Clean up temporary files and directories.
        // ---------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit.
        }
    }
}