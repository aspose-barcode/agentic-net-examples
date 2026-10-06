// Title: Read Swiss QR Code and extract bill details
// Description: Demonstrates how to load a Swiss QR Code image, decode its raw text, and parse the bill information using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on QR code decoding and complex code text parsing. It showcases the BarCodeReader class with DecodeType.QR, handling recognition exceptions, and using ComplexCodetextReader to interpret Swiss QR bill data. Developers working with payment QR codes can use this pattern to extract structured information from scanned images.
// Prompt: Read raw encoded text from a Swiss QR Code image and gracefully handle possible decoding exceptions.
// Tags: swiss qr code, barcode recognition, qr, aspnet, aspose.barcode, complexcodetextreader, decode, exception handling

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates reading a Swiss QR Code image, extracting raw encoded text,
/// and parsing the bill details while handling possible decoding exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Sets console encoding, validates the image file,
    /// reads barcodes, decodes Swiss QR bill data, and outputs the extracted fields.
    /// </summary>
    static void Main()
    {
        // Ensure Unicode characters are displayed correctly in the console.
        Console.OutputEncoding = Encoding.Unicode;

        // Build the full path to the image file located in the current directory.
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "SwissQRBill.png");

        // Verify that the image file exists before attempting processing.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        try
        {
            // Initialize the barcode reader for QR codes using the specified image.
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
            {
                BarCodeResult[] results = null;

                try
                {
                    // Attempt to read all barcodes present in the image.
                    results = reader.ReadBarCodes();
                }
                catch (RecognitionAbortedException ex)
                {
                    // Handle cases where the recognition process is aborted.
                    Console.WriteLine($"Recognition aborted: {ex.Message}");
                    return;
                }

                // If no results were found, inform the user and exit.
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                    return;
                }

                // Iterate through each detected barcode result.
                foreach (BarCodeResult result in results)
                {
                    // Skip results that do not contain any text.
                    if (string.IsNullOrEmpty(result.CodeText))
                    {
                        Console.WriteLine("Empty CodeText in result.");
                        continue;
                    }

                    // Attempt to parse the raw QR code text as a Swiss QR bill.
                    SwissQRCodetext swissResult = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                    if (swissResult == null)
                    {
                        Console.WriteLine("Failed to decode Swiss QR codetext.");
                        continue;
                    }

                    // Output the extracted bill details to the console.
                    Console.WriteLine($"Version: {swissResult.Bill.Version}");
                    Console.WriteLine($"Account: {swissResult.Bill.Account}");
                    Console.WriteLine($"Amount: {swissResult.Bill.Amount}");
                    Console.WriteLine($"Currency: {swissResult.Bill.Currency}");
                    Console.WriteLine($"Reference: {swissResult.Bill.Reference}");
                    Console.WriteLine($"Creditor: {swissResult.Bill.Creditor?.Name}");
                    Console.WriteLine($"Debtor: {swissResult.Bill.Debtor?.Name}");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors during processing and display a message.
            Console.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}