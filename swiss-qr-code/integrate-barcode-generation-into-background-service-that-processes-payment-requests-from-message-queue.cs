// Title: Barcode generation in a background service for payment processing
// Description: Demonstrates creating Code128 barcodes for payment references and saving them as PNG files, suitable for integration with a message‑queue‑driven background service.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and related parameter settings to produce barcodes programmatically. Typical use cases include encoding transaction identifiers, invoices, or other payment data for printing or digital distribution. Developers often need to generate barcodes in batch jobs or background services, handling errors and managing output files.
// Prompt: Integrate barcode generation into a background service that processes payment requests from a message queue.
// Tags: barcode generation, code128, png, background service, payment processing, aspose.barcode, encode types

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeBackgroundServiceDemo
{
    /// <summary>
    /// Simple model representing a payment request.
    /// </summary>
    class PaymentRequest
    {
        public string Id { get; set; }          // Unique identifier.
        public decimal Amount { get; set; }     // Payment amount.
        public string Reference { get; set; }   // Reference string to encode in the barcode.
    }

    /// <summary>
    /// Demonstrates processing a collection of payment requests and generating barcodes for each.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that simulates a background service processing payment requests and creating barcode images.
        /// </summary>
        static void Main()
        {
            // Sample payment requests – in a real scenario these would come from a message queue.
            var payments = new List<PaymentRequest>
            {
                new PaymentRequest { Id = "PAY001", Amount = 123.45m, Reference = "INV001-12345" },
                new PaymentRequest { Id = "PAY002", Amount = 67.89m, Reference = "INV002-67890" },
                new PaymentRequest { Id = "PAY003", Amount = 250.00m, Reference = "INV003-25000" },
                new PaymentRequest { Id = "PAY004", Amount = 5.00m, Reference = "INV004-00005" },
                new PaymentRequest { Id = "PAY005", Amount = 99.99m, Reference = "INV005-99999" }
            };

            // Determine the output directory for generated barcode images.
            string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Process each payment request.
            foreach (var payment in payments)
            {
                try
                {
                    // Resolve the symbology name to a BaseEncodeType using reflection (rule 26).
                    string symbologyName = "Code128"; // Using Code128 for payment references.
                    var field = typeof(EncodeTypes).GetField(symbologyName);
                    if (field == null)
                    {
                        Console.WriteLine($"Unknown symbology: {symbologyName}");
                        continue;
                    }
                    BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                    // Create the barcode generator with the selected symbology and payment reference.
                    using (var generator = new BarcodeGenerator(encodeType, payment.Reference))
                    {
                        // Configure barcode appearance.
                        generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                        generator.Parameters.ImageWidth.Point = 300f;   // Width in points.
                        generator.Parameters.ImageHeight.Point = 100f;  // Height in points.
                        generator.Parameters.Barcode.XDimension.Point = 2f; // Module size.
                        generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                        generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                        generator.Parameters.Barcode.Padding.Left.Point = 5f;
                        generator.Parameters.Barcode.Padding.Top.Point = 5f;
                        generator.Parameters.Barcode.Padding.Right.Point = 5f;
                        generator.Parameters.Barcode.Padding.Bottom.Point = 5f;
                        generator.Parameters.Barcode.FilledBars = false;
                        generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                        // Human‑readable text styling (optional).
                        generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                        generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;
                        generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                        // Save the barcode image to the output directory.
                        string fileName = $"{payment.Id}_{payment.Reference}.png";
                        string filePath = Path.Combine(outputDir, fileName);
                        generator.Save(filePath);
                        Console.WriteLine($"Generated barcode for payment {payment.Id} at: {filePath}");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any unexpected errors gracefully.
                    Console.WriteLine($"Failed to generate barcode for payment {payment.Id}: {ex.Message}");
                }
            }

            // Program completes after processing the sample batch.
        }
    }
}