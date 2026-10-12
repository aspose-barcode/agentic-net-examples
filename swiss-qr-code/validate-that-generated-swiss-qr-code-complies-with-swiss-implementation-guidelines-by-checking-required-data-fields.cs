// Title: Generate and Validate a Swiss QR Code using Aspose.BarCode
// Description: Demonstrates creating a Swiss QR Code with required bill data, saving it as an image, and validating the encoded fields against the Swiss Implementation Guidelines.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and BarCodeReader to produce and decode Swiss QR Codes, a common requirement for Swiss payment standards. Developers often need to generate QR codes for invoices and verify that mandatory fields such as creditor information, IBAN, amount, and version are correctly encoded.
// Prompt: Validate that the generated Swiss QR Code complies with Swiss Implementation Guidelines by checking required data fields.
// Tags: swiss qr code, barcode generation, barcode validation, aspnet, aspose.barcode, complexbarcode, qr, payment

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and validation of a Swiss QR Code using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Swiss QR Code, saves it, reads it back, and validates required fields.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder and file path for the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "SwissQR.png");

        // Create Swiss QR Code data and populate mandatory bill fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";

        // Set creditor address information
        swissQr.Bill.Creditor = new Address
        {
            Name = "John Doe",
            Street = "Main Street",
            HouseNo = "1",
            PostalCode = "8000",
            Town = "Zurich",
            CountryCode = "CH"
        };

        // Set debtor address information
        swissQr.Bill.Debtor = new Address
        {
            Name = "Jane Smith",
            Street = "Second Street",
            HouseNo = "2",
            PostalCode = "3000",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate the barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4; // Set module size
            generator.Save(barcodePath); // Save as PNG
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Read and decode the barcode, then validate required fields
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            bool anyValid = false;

            foreach (var result in reader.ReadBarCodes())
            {
                var decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Unable to decode Swiss QR Code.");
                    continue;
                }

                bool isValid = true;

                // Validate creditor name
                if (string.IsNullOrWhiteSpace(decoded.Bill.Creditor.Name))
                {
                    Console.WriteLine("Invalid: Creditor name is missing.");
                    isValid = false;
                }

                // Validate creditor country code (must be CH)
                if (decoded.Bill.Creditor.CountryCode != "CH")
                {
                    Console.WriteLine("Invalid: Creditor country code must be 'CH'.");
                    isValid = false;
                }

                // Validate IBAN presence
                if (string.IsNullOrWhiteSpace(decoded.Bill.Account))
                {
                    Console.WriteLine("Invalid: Account (IBAN) is missing.");
                    isValid = false;
                }

                // Validate amount is greater than zero
                if (decoded.Bill.Amount <= 0)
                {
                    Console.WriteLine("Invalid: Amount must be greater than zero.");
                    isValid = false;
                }

                // Validate bill version matches expected standard
                if (decoded.Bill.Version != SwissQRBill.QrBillStandardVersion.V2_0)
                {
                    Console.WriteLine("Invalid: Bill version is not V2_0.");
                    isValid = false;
                }

                Console.WriteLine(isValid ? "Swiss QR Code validation passed." : "Swiss QR Code validation failed.");
                anyValid = true;
            }

            if (!anyValid)
            {
                Console.WriteLine("No barcode results were read.");
            }
        }

        // Cleanup temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}