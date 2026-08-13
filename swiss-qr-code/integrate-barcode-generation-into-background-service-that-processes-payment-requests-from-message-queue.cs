// Title: Generate Code128 Barcodes for Payment Requests in a Background Service
// Description: Demonstrates how to create Code128 barcode images for payment identifiers using Aspose.BarCode and save them as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the BarcodeGenerator class for creating barcodes in .NET applications. Typical use cases include encoding transaction IDs, order numbers, or any alphanumeric data for printing or digital distribution. Developers often need to customize appearance, set padding, and output to common image formats such as PNG or JPEG.
// Prompt: Integrate barcode generation into a background service that processes payment requests from a message queue.
// Tags: code128, barcode generation, png, aspose.barcode, background service, payment processing, .net

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeBackgroundServiceDemo
{
    /// <summary>
    /// Simple representation of a payment request.
    /// </summary>
    public class PaymentRequest
    {
        public string PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Payee { get; set; }
    }

    /// <summary>
    /// Demonstrates processing a collection of payment requests and generating a Code128 barcode for each.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application entry point. Simulates a background service that processes payment requests from a queue
        /// and generates corresponding barcode images.
        /// </summary>
        public static void Main(string[] args)
        {
            // Prepare a small set of sample payment requests.
            var paymentQueue = new List<PaymentRequest>
            {
                new PaymentRequest { PaymentId = "PAY001", Amount = 123.45m, Payee = "Alice" },
                new PaymentRequest { PaymentId = "PAY002", Amount = 67.89m, Payee = "Bob" },
                new PaymentRequest { PaymentId = "PAY003", Amount = 250.00m, Payee = "Charlie" },
                new PaymentRequest { PaymentId = "PAY004", Amount = 99.99m, Payee = "Diana" },
                new PaymentRequest { PaymentId = "PAY005", Amount = 10.00m, Payee = "Eve" }
            };

            // Directory where barcode images will be saved.
            string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Process each payment request (simulating a background service).
            for (int i = 0; i < paymentQueue.Count; i++)
            {
                var request = paymentQueue[i];
                string barcodePath = Path.Combine(outputDir, $"{request.PaymentId}.png");
                try
                {
                    GenerateBarcodeForPayment(request, barcodePath);
                    Console.WriteLine($"Generated barcode for PaymentId={request.PaymentId} at {barcodePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error generating barcode for PaymentId={request.PaymentId}: {ex.Message}");
                }
            }

            // Indicate completion.
            Console.WriteLine("All payment barcodes have been processed.");
        }

        // Generates a Code128 barcode image for the given payment request and saves it to the specified path.
        private static void GenerateBarcodeForPayment(PaymentRequest request, string outputPath)
        {
            // Use Code128 symbology; encode the PaymentId as the barcode text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, request.PaymentId))
            {
                // Optional visual customizations.
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;

                // Set module size (XDimension) to 2 points.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Add modest padding around the barcode.
                generator.Parameters.Barcode.Padding.Left.Point = 5f;
                generator.Parameters.Barcode.Padding.Top.Point = 5f;
                generator.Parameters.Barcode.Padding.Right.Point = 5f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

                // Ensure human‑readable text appears below the barcode.
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Save the barcode as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }
}