// Title: Parallel generation of Swiss QR Code barcodes for payment records
// Description: Demonstrates how to create Swiss QR Code barcodes for multiple payment records using Aspose.BarCode in parallel to improve performance.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Code. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related QR settings to produce PNG images, a common requirement for payment processing systems. Developers often need to generate many QR codes quickly, and this pattern illustrates parallel execution for scalability.
// Prompt: Implement parallel generation of Swiss QR Code barcodes for multiple payment records to boost performance.
// Tags: swissqr, barcode, generation, png, parallel, aspose.barcode, complexbarcodegenerator

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates parallel generation of Swiss QR Code barcodes for a set of payment records.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates Swiss QR Code PNG files in parallel and writes their paths to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output directory for the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissQRBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define a collection of sample payment records to be encoded as Swiss QR Codes
        var payments = new List<PaymentRecord>
        {
            new PaymentRecord { Amount = 150.00m, Reference = "210000000001", CreditorName = "Alice Smith" },
            new PaymentRecord { Amount = 250.50m, Reference = "210000000002", CreditorName = "Bob Johnson" },
            new PaymentRecord { Amount = 99.99m,  Reference = "210000000003", CreditorName = "Carol Lee" },
            new PaymentRecord { Amount = 500.00m, Reference = "210000000004", CreditorName = "David Brown" },
            new PaymentRecord { Amount = 75.25m,  Reference = "210000000005", CreditorName = "Eve Davis" }
        };

        // Process each payment record in parallel to speed up barcode generation
        Parallel.ForEach(payments, payment =>
        {
            // Build the Swiss QR Code data structure (codetext) for the current payment
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            swissQr.Bill.Account = "CH9300762011623852957";
            swissQr.Bill.Amount = payment.Amount;
            swissQr.Bill.Currency = "CHF";
            swissQr.Bill.Reference = payment.Reference;
            swissQr.Bill.Creditor = new Address
            {
                Name = payment.CreditorName,
                Street = "Main Street",
                HouseNo = "1",
                PostalCode = "8000",
                Town = "Zurich",
                CountryCode = "CH"
            };
            // Optional debtor information can be added here if needed

            // Determine the file path for the generated PNG image
            string filePath = Path.Combine(outputDir, $"SwissQR_{payment.Reference}.png");

            // Generate the barcode image using ComplexBarcodeGenerator
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;               // Set module size
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;   // Use ECI encoding mode
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8; // Specify UTF-8 character set
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH; // High error correction
                generator.Save(filePath, BarCodeImageFormat.Png);                // Save as PNG
            }

            // Output the location of the generated barcode
            Console.WriteLine($"Generated: {filePath}");
        });

        // Inform the user that all barcodes have been processed
        Console.WriteLine("All Swiss QR Code barcodes have been generated.");
    }

    /// <summary>
    /// Simple DTO representing a payment record to be encoded in a Swiss QR Code.
    /// </summary>
    class PaymentRecord
    {
        public decimal Amount { get; set; }
        public string Reference { get; set; }
        public string CreditorName { get; set; }
    }
}