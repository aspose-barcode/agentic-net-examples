// Title: Generate Swiss QR Code for Payment Using Aspose.BarCode ComplexBarcodeGenerator
// Description: Demonstrates how to create a Swiss QR Code image containing payment information with Aspose.BarCode's ComplexBarcodeGenerator and SwissQRCodetext.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator, SwissQRCodetext, and QR error correction settings. Typical use cases include generating payment QR codes for Swiss banking standards, where developers need to embed creditor details, amount, and currency into a scannable image. The example illustrates preparing bill data, configuring the generator, and saving the result as a PNG file.
// Prompt: Generate a Swiss QR Code image from payment details using ComplexBarcodeGenerator and SwissQRCodetext.
// Tags: swiss qr, payment, barcode, complexbarcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Swiss QR Code image for a payment using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds payment data, generates the QR code, and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Prepare Swiss QR payment data using the SwissQRCodetext model
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create a ComplexBarcodeGenerator instance initialized with the payment data
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Optional: set a high error correction level for better scan reliability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define the output file path for the generated PNG image
            string outputPath = "SwissQR.png";

            // Save the barcode image to a memory stream, then write the bytes to the file system
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                File.WriteAllBytes(outputPath, ms.ToArray());
            }

            // Inform the user where the image has been saved
            Console.WriteLine("Swiss QR Code generated at: " + Path.GetFullPath(outputPath));
        }
    }
}