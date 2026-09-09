// Title: Decode Swiss QR Code using ComplexCodetextReader
// Description: Demonstrates generating a Swiss QR code, saving it as an image, and decoding it back into a SwissQRCodetext object for data extraction.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to create a Swiss QR payment code, BarCodeReader to scan the image, and ComplexCodetextReader to parse the raw QR text into a strongly‑typed SwissQRCodetext object. Developers working with payment QR codes, QR bill standards, or any complex barcode formats will find these APIs essential for creating and extracting structured data.
// Prompt: Use ComplexCodetextReader.TryDecodeSwissQR to parse raw text into a SwissQRCodetext object for extraction.
// Tags: barcode, swissqr, decoding, complexbarcode, aspose.barcode, qr, payment

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a Swiss QR code, saves it to a temporary file,
/// reads the QR code back, and extracts the encoded payment information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, decoding, and cleanup steps.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Ensure Unicode characters (e.g., umlauts) are displayed correctly in the console.
        Console.OutputEncoding = Encoding.Unicode;

        // Create a unique temporary folder to store the generated QR image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "SwissQR.png");

        // Build the Swiss QR code data structure with sample payment details.
        SwissQRCodetext swissCodetext = new SwissQRCodetext();
        swissCodetext.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissCodetext.Bill.Account = "CH4431999123000889012";
        swissCodetext.Bill.Amount = 1000.25m;
        swissCodetext.Bill.Currency = "CHF";
        swissCodetext.Bill.Reference = "210000000003139471430009017";
        swissCodetext.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };
        swissCodetext.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate the QR code image using the complex barcode generator.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissCodetext))
        {
            // Configure visual appearance: module size and QR encoding mode.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated QR code to the temporary file.
            generator.Save(imagePath);
        }

        // Verify that the image was created before attempting to read it.
        if (File.Exists(imagePath))
        {
            // Initialize a barcode reader for QR codes.
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
            {
                // Iterate over all detected QR codes (normally just one).
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Decode the raw QR text into a strongly‑typed SwissQRCodetext object.
                    SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                    if (decoded == null)
                    {
                        Console.WriteLine("Failed to decode Swiss QR code.");
                        continue;
                    }

                    // Output the extracted payment information.
                    Console.WriteLine($"Version: {decoded.Bill.Version}");
                    Console.WriteLine($"Account: {decoded.Bill.Account}");
                    Console.WriteLine($"Amount: {decoded.Bill.Amount}");
                    Console.WriteLine($"Currency: {decoded.Bill.Currency}");
                    Console.WriteLine($"Reference: {decoded.Bill.Reference}");
                    Console.WriteLine($"Creditor: {decoded.Bill.Creditor?.Name}");
                    Console.WriteLine($"Debtor: {decoded.Bill.Debtor?.Name}");
                }
            }
        }
        else
        {
            Console.WriteLine("Generated image not found.");
        }

        // Clean up temporary files and folder; ignore any errors during deletion.
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Suppress cleanup exceptions.
        }
    }
}