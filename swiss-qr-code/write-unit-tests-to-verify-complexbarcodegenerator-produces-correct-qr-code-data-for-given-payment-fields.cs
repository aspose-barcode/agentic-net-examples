// Title: Generate and Verify Swiss QR Bill Barcode
// Description: Demonstrates creating a Swiss QR bill QR code with ComplexBarcodeGenerator, then reading it back to confirm the encoded payment fields match the original data.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator for QR code creation, BarCodeReader for decoding, and related classes such as SwissQRCodetext, Address, and QRErrorLevel. Typical scenarios include generating payment QR codes (e.g., Swiss QR bills) and validating them in automated tests or processing pipelines.
// Prompt: Write unit tests to verify ComplexBarcodeGenerator produces correct QR code data for given payment fields.
// Tags: qr code, swiss qr bill, complex barcode, generation, recognition, aspose.barcode, payment, unit testing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Swiss QR bill barcode, reading it back, and verifying the encoded data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates, reads, and validates a Swiss QR bill QR code.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare expected payment data using the Swiss QR bill model
        // ------------------------------------------------------------
        var expected = new SwissQRCodetext();
        expected.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        expected.Bill.Account = "CH9300762011623852957";
        expected.Bill.Amount = 199.95m;
        expected.Bill.Currency = "CHF";
        expected.Bill.Reference = "210000000003139471430009017";
        expected.Bill.Creditor = new Address
        {
            Name = "John Doe",
            Street = "Main Street",
            HouseNo = "1",
            PostalCode = "8000",
            Town = "Zurich",
            CountryCode = "CH"
        };
        expected.Bill.Debtor = new Address
        {
            Name = "Acme Corp",
            Street = "Business Ave",
            HouseNo = "99",
            PostalCode = "3000",
            Town = "Bern",
            CountryCode = "CH"
        };

        // ------------------------------------------------------------
        // Generate the QR code into a memory stream
        // ------------------------------------------------------------
        using (var ms = new MemoryStream())
        {
            using (var generator = new ComplexBarcodeGenerator(expected))
            {
                // Configure barcode appearance and error correction level
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated barcode as PNG into the stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset stream position for reading
            ms.Position = 0;

            // ------------------------------------------------------------
            // Read and decode the QR code from the memory stream
            // ------------------------------------------------------------
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                var results = reader.ReadBarCodes();

                // Ensure a barcode was detected
                if (results.Length == 0)
                {
                    Console.WriteLine("FAILED: No barcode detected.");
                    return;
                }

                var result = results[0];

                // Decode the Swiss QR codetext from the scanned result
                var decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("FAILED: Unable to decode Swiss QR codetext.");
                    return;
                }

                // ------------------------------------------------------------
                // Verify that all fields match the expected values
                // ------------------------------------------------------------
                bool success =
                    decoded.Bill.Version == expected.Bill.Version &&
                    decoded.Bill.Account == expected.Bill.Account &&
                    decoded.Bill.Amount == expected.Bill.Amount &&
                    decoded.Bill.Currency == expected.Bill.Currency &&
                    decoded.Bill.Reference == expected.Bill.Reference &&
                    decoded.Bill.Creditor.Name == expected.Bill.Creditor.Name &&
                    decoded.Bill.Creditor.Street == expected.Bill.Creditor.Street &&
                    decoded.Bill.Creditor.HouseNo == expected.Bill.Creditor.HouseNo &&
                    decoded.Bill.Creditor.PostalCode == expected.Bill.Creditor.PostalCode &&
                    decoded.Bill.Creditor.Town == expected.Bill.Creditor.Town &&
                    decoded.Bill.Creditor.CountryCode == expected.Bill.Creditor.CountryCode &&
                    decoded.Bill.Debtor.Name == expected.Bill.Debtor.Name &&
                    decoded.Bill.Debtor.Street == expected.Bill.Debtor.Street &&
                    decoded.Bill.Debtor.HouseNo == expected.Bill.Debtor.HouseNo &&
                    decoded.Bill.Debtor.PostalCode == expected.Bill.Debtor.PostalCode &&
                    decoded.Bill.Debtor.Town == expected.Bill.Debtor.Town &&
                    decoded.Bill.Debtor.CountryCode == expected.Bill.Debtor.CountryCode;

                // Output verification result
                Console.WriteLine(success
                    ? "PASSED: QR code data matches expected values."
                    : "FAILED: QR code data does not match expected values.");
            }
        }
    }
}