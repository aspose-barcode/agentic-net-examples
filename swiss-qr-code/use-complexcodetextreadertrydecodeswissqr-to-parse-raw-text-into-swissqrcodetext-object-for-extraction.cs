// Title: Decode Swiss QR Code using ComplexCodetextReader
// Description: Demonstrates generating a Swiss QR Bill barcode, reading it back, and extracting bill data via ComplexCodetextReader.TryDecodeSwissQR.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations category, showcasing how to create, save, and recognize Swiss QR Code (QR‑Bill) symbology. It uses ComplexBarcodeGenerator for encoding and BarCodeReader with DecodeType.QR for decoding, while ComplexCodetextReader parses the raw text into a SwissQRCodetext object. Developers working with payment QR codes can use this pattern to automate bill data extraction.
// Prompt: Use ComplexCodetextReader.TryDecodeSwissQR to parse raw text into a SwissQRCodetext object for extraction.
// Tags: swissqr, barcode, generation, recognition, complexcodetextreader, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Swiss QR Bill barcode, decodes it, and extracts bill information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Swiss QR Code image, reads it, decodes the codetext, and prints bill details.
    /// </summary>
    static void Main()
    {
        // Ensure Unicode output for console
        Console.OutputEncoding = Encoding.Unicode;

        // Create Swiss QR Code data object and populate bill fields
        SwissQRCodetext swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH4431999123000889012";
        swissQRCode.Bill.Amount = 1000.25m;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Reference = "210000000003139471430009017";
        swissQRCode.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            CountryCode = "CH"
        };

        // Define temporary file path for the generated barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "SwissQRBill.png");

        // Generate and save the barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            // Set visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Save image to file
            generator.Save(imagePath);
        }

        // Initialize a reader for QR codes and decode the saved image
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Parse raw codetext into a strongly‑typed SwissQRCodetext object
                SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                if (decoded == null)
                {
                    continue; // Skip if decoding failed
                }

                // Output extracted bill information
                Console.WriteLine($"Version: {decoded.Bill.Version}");
                Console.WriteLine($"Account: {decoded.Bill.Account}");
                Console.WriteLine($"Amount: {decoded.Bill.Amount}");
                Console.WriteLine($"Currency: {decoded.Bill.Currency}");
                Console.WriteLine($"Reference: {decoded.Bill.Reference}");
                Console.WriteLine($"Creditor Name: {decoded.Bill.Creditor.Name}");
                Console.WriteLine($"Creditor Country: {decoded.Bill.Creditor.CountryCode}");
            }
        }

        // Clean up the temporary image file
        if (File.Exists(imagePath))
        {
            try
            {
                File.Delete(imagePath);
            }
            catch
            {
                // Ignore any cleanup errors
            }
        }
    }
}