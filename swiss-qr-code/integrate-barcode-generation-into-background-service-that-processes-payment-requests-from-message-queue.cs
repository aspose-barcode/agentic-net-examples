using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code128 barcodes for a list of payment requests using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Simple payment request model containing a transaction identifier and an amount.
    /// </summary>
    class PaymentRequest
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
    }

    /// <summary>
    /// Entry point of the application. Generates barcode images for sample payment requests.
    /// </summary>
    static void Main()
    {
        // Sample payment requests simulating messages from a queue
        var paymentRequests = new List<PaymentRequest>
        {
            new PaymentRequest { TransactionId = "TXN001", Amount = 123.45m },
            new PaymentRequest { TransactionId = "TXN002", Amount = 67.89m },
            new PaymentRequest { TransactionId = "TXN003", Amount = 250.00m },
            new PaymentRequest { TransactionId = "TXN004", Amount = 5.99m },
            new PaymentRequest { TransactionId = "TXN005", Amount = 99.99m }
        };

        // Determine output directory for generated barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each payment request and generate a corresponding barcode image
        foreach (var request in paymentRequests)
        {
            // Use Code128 symbology; encode the transaction ID as the barcode value
            BaseEncodeType encodeType = EncodeTypes.Code128;

            // Build the full file path for the barcode image (e.g., barcode_TXN001.png)
            string barcodePath = Path.Combine(outputDir, $"barcode_{request.TransactionId}.png");

            // Generate and save the barcode using Aspose.BarCode
            using (var generator = new BarcodeGenerator(encodeType, request.TransactionId))
            {
                // Optional: set resolution for better image quality (dots per inch)
                generator.Parameters.Resolution = 300f;

                // Save the barcode image as a PNG file
                generator.Save(barcodePath);
            }

            // Log the successful generation of the barcode
            Console.WriteLine($"Generated barcode for Transaction {request.TransactionId} (Amount: {request.Amount:C}) at: {barcodePath}");
        }

        // Indicate that all barcodes have been processed
        Console.WriteLine("All barcodes have been generated.");
    }
}