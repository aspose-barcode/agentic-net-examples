using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace SwissQRParallelGeneration
{
    /// <summary>
    /// Simple DTO for payment information required by Swiss QR Code.
    /// </summary>
    class PaymentRecord
    {
        public string CreditorName { get; set; }
        public string CountryCode { get; set; } // ISO country code, e.g., "CH"
        public string Account { get; set; }     // IBAN
        public decimal Amount { get; set; }     // Amount in CHF
    }

    /// <summary>
    /// Demonstrates parallel generation of Swiss QR Code barcodes using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Creates sample payment records,
        /// sets up the output directory, and generates QR code images in parallel.
        /// </summary>
        static void Main()
        {
            // Sample payment records (safe small batch)
            var records = new List<PaymentRecord>
            {
                new PaymentRecord { CreditorName = "John Doe", CountryCode = "CH", Account = "CH9300762011623852957", Amount = 199.95m },
                new PaymentRecord { CreditorName = "Alice Smith", CountryCode = "CH", Account = "CH9300762011623852957", Amount = 250.00m },
                new PaymentRecord { CreditorName = "Bob Johnson", CountryCode = "CH", Account = "CH9300762011623852957", Amount = 75.50m },
                new PaymentRecord { CreditorName = "Carol White", CountryCode = "CH", Account = "CH9300762011623852957", Amount = 120.00m },
                new PaymentRecord { CreditorName = "David Brown", CountryCode = "CH", Account = "CH9300762011623852957", Amount = 300.00m }
            };

            // Output directory for generated PNG files
            string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "SwissQRBarcodes");
            Directory.CreateDirectory(outputDir);

            // Configure parallel execution to use all available processors
            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };

            // Parallel generation of Swiss QR Code barcodes
            Parallel.ForEach(records, parallelOptions, (record, state, index) =>
            {
                // Build file name based on the record index (1‑based)
                string filePath = Path.Combine(outputDir, $"SwissQR_{index + 1}.png");

                // Generate the QR code image and save it to disk
                GenerateSwissQR(record, filePath);

                // Log progress to the console
                Console.WriteLine($"Generated: {filePath}");
            });

            Console.WriteLine("All barcodes have been generated.");
        }

        /// <summary>
        /// Generates a Swiss QR Code barcode for a single payment record and saves it to the specified path.
        /// </summary>
        /// <param name="record">The payment information to encode.</param>
        /// <param name="outputPath">The full file path where the PNG image will be saved.</param>
        static void GenerateSwissQR(PaymentRecord record, string outputPath)
        {
            // Build the Swiss QR codetext object with required fields
            var swissQr = new SwissQRCodetext
            {
                Bill =
                {
                    Creditor = { Name = record.CreditorName, CountryCode = record.CountryCode },
                    Account = record.Account,
                    Amount = record.Amount,
                    Version = SwissQRBill.QrBillStandardVersion.V2_0
                }
            };

            // Use ComplexBarcodeGenerator to create the image
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                // Save directly to file (PNG format is default)
                generator.Save(outputPath);
            }
        }
    }
}