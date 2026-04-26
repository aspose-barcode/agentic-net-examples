using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    // Simple payment request model
    class PaymentRequest
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string Payee { get; set; }
    }

    static void Main()
    {
        // Simulated queue of payment requests
        var requests = new List<PaymentRequest>
        {
            new PaymentRequest { Id = "REQ001", Amount = 123.45m, Payee = "Alice" },
            new PaymentRequest { Id = "REQ002", Amount = 67.89m, Payee = "Bob" },
            new PaymentRequest { Id = "REQ003", Amount = 250.00m, Payee = "Charlie" }
        };

        // Output folder for generated barcodes
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process each request and generate a QR code barcode
        foreach (var request in requests)
        {
            // Encode payment details into a QR code string
            string codeText = $"{request.Id}|{request.Amount:F2}|{request.Payee}";

            // Create barcode generator for QR code
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set high error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Define image size (points)
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;

                // Set resolution (dpi)
                generator.Parameters.Resolution = 300;

                // Optional: set foreground and background colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save barcode as PNG file
                string filePath = Path.Combine(outputFolder, $"barcode_{request.Id}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode for request {request.Id} at {filePath}");
            }
        }
    }
}