// Title: Read and Decode Swiss QR Code with Aspose.BarCode
// Description: Generates a Swiss QR Code image, reads it back, and extracts the raw encoded data, handling decoding errors gracefully.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on Swiss QR Bill (QR) symbology. It demonstrates using ComplexBarcodeGenerator to create a QR code, BarCodeReader for decoding, and ComplexCodetextReader for parsing the structured data. Developers working with payment QR codes, invoice automation, or QR-based data exchange commonly need to generate, read, and validate such barcodes.
// Prompt: Read raw encoded text from a Swiss QR Code image and gracefully handle possible decoding exceptions.
// Tags: swissqr, qr, barcode, generation, recognition, exception-handling, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Swiss QR Code, reading it back, and handling possible decoding exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a temporary Swiss QR Code image, decodes it, and outputs the extracted fields.
    /// </summary>
    static void Main()
    {
        // Ensure Unicode characters (e.g., umlauts) are displayed correctly in the console.
        Console.OutputEncoding = Encoding.Unicode;

        // Create a temporary folder for the sample image.
        string tempDir = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "SwissQRBill.png");

        // Build the Swiss QR Code data structure with sample billing information.
        SwissQRCodetext swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH4431999123000889012";
        swissQRCode.Bill.Amount = 1000.25m;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Reference = "210000000003139471430009017";
        swissQRCode.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };
        swissQRCode.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate the QR code image using the complex barcode generator.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f; // Set module size for better readability.
            generator.Save(imagePath);
        }

        // Verify the image exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Generated image not found.");
            return;
        }

        // Read and decode the Swiss QR Code from the generated image.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            try
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Attempt to parse the raw QR code text into a structured Swiss QR object.
                    SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                    if (decoded == null)
                    {
                        Console.WriteLine("Failed to parse Swiss QR Code text.");
                        continue;
                    }

                    // Output the extracted billing fields.
                    Console.WriteLine($"Version:{decoded.Bill.Version}");
                    Console.WriteLine($"Account:{decoded.Bill.Account}");
                    Console.WriteLine($"Amount:{decoded.Bill.Amount}");
                    Console.WriteLine($"Currency:{decoded.Bill.Currency}");
                    Console.WriteLine($"Reference:{decoded.Bill.Reference}");
                    Console.WriteLine($"Creditor:{decoded.Bill.Creditor.Name}");
                    Console.WriteLine($"Debtor:{decoded.Bill.Debtor.Name}");
                }
            }
            catch (Exception ex)
            {
                // Gracefully handle any exceptions that occur during barcode reading.
                Console.WriteLine($"Error during barcode reading: {ex.Message}");
            }
        }

        // Clean up temporary files; ignore any errors during cleanup.
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome.
        }
    }
}