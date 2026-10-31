// Title: Read Swiss QR Code raw text with exception handling
// Description: Demonstrates how to load a Swiss QR Code image, decode its raw text using Aspose.BarCode, and display bill details while handling possible errors.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating the use of BarCodeReader with DecodeType.QR and ComplexCodetextReader to extract structured data from Swiss QR Code (QR‑Bill) images. Typical use cases include processing payment QR codes, validating bill information, and integrating QR‑Bill scanning into financial applications. Developers often need to read raw encoded text, map it to domain objects, and gracefully handle missing files or decoding failures.
// Prompt: Read raw encoded text from a Swiss QR Code image and gracefully handle possible decoding exceptions.
// Tags: swiss qr code, barcode recognition, qr, exception handling, aspose.barcode, complexcodetextreader, bill data

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates reading raw encoded text from a Swiss QR Code image and handling decoding exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Loads the image, decodes the QR code, extracts bill information, and writes it to the console.
    /// </summary>
    static void Main()
    {
        // Ensure console can display Unicode characters (e.g., accented letters in names)
        Console.OutputEncoding = Encoding.Unicode;

        // Path to the Swiss QR Code image file
        string imagePath = "SwissQRBill.png";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Initialize the barcode reader for QR codes using the specified image
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
            {
                bool anyDetected = false;

                // Iterate through all detected barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    anyDetected = true;

                    // Attempt to decode the raw Swiss QR Code text into a structured object
                    SwissQRCodetext swissResult = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                    if (swissResult == null)
                    {
                        Console.WriteLine("Failed to decode Swiss QR Code text.");
                        continue;
                    }

                    // Output relevant bill details to the console
                    Console.WriteLine($"Version: {swissResult.Bill.Version}");
                    Console.WriteLine($"Account: {swissResult.Bill.Account}");
                    Console.WriteLine($"Amount: {swissResult.Bill.Amount}");
                    Console.WriteLine($"Currency: {swissResult.Bill.Currency}");
                    Console.WriteLine($"Reference: {swissResult.Bill.Reference}");
                    Console.WriteLine($"Creditor: {swissResult.Bill.Creditor?.Name}");
                    Console.WriteLine($"Debtor: {swissResult.Bill.Debtor?.Name}");
                }

                // Inform the user if no barcodes were detected in the image
                if (!anyDetected)
                {
                    Console.WriteLine("No barcodes detected in the image.");
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors during barcode reading or decoding
            Console.WriteLine($"Error during barcode reading: {ex.Message}");
        }
    }
}