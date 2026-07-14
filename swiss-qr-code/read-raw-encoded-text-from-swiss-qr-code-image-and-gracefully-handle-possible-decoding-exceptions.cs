// Title: Read Swiss QR Code raw text and handle decoding errors
// Description: Demonstrates how to read a Swiss QR Code image, extract the raw encoded text, and safely decode it while handling possible exceptions.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on reading and decoding complex barcode symbologies such as Swiss QR. It showcases the use of BarCodeReader, DecodeType, and ComplexCodetextReader classes, typical for applications that need to process payment QR codes and handle errors gracefully. Developers often need to extract payment details from images and ensure robust error handling.
// Prompt: Read raw encoded text from a Swiss QR Code image and gracefully handle possible decoding exceptions.
// Tags: barcode symbology, decoding, swiss qr, aspose.barcode, complex barcode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that reads a Swiss QR Code image, extracts its raw text,
/// and attempts to decode it into structured payment information while handling errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs image validation, barcode detection,
    /// raw text extraction, and Swiss QR specific decoding with graceful error handling.
    /// </summary>
    static void Main()
    {
        // Path to the input image containing the Swiss QR Code
        string imagePath = "SwissQR.png";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Initialize the barcode reader for all supported types
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes present in the image
                var results = reader.ReadBarCodes();

                // If no barcodes were detected, inform the user and exit
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected in the image.");
                    return;
                }

                // Process each detected barcode
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");

                    // Retrieve the raw encoded text from the barcode
                    string codeText = result.CodeText;
                    if (string.IsNullOrEmpty(codeText))
                    {
                        Console.WriteLine("Barcode detected but codetext is empty.");
                        continue;
                    }

                    // Attempt to decode the raw text as a Swiss QR code
                    SwissQRCodetext swissQr = ComplexCodetextReader.TryDecodeSwissQR(codeText);
                    if (swissQr != null)
                    {
                        // Successful decoding – output payment details
                        Console.WriteLine("Successfully decoded Swiss QR code.");
                        Console.WriteLine($"Account: {swissQr.Bill.Account}");
                        Console.WriteLine($"Amount: {swissQr.Bill.Amount}");
                        Console.WriteLine($"Creditor Name: {swissQr.Bill.Creditor.Name}");
                        Console.WriteLine($"Creditor Country: {swissQr.Bill.Creditor.CountryCode}");
                    }
                    else
                    {
                        // Decoding failed – inform the user
                        Console.WriteLine("Failed to decode Swiss QR codetext.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors during processing and display a friendly message
            Console.WriteLine($"An error occurred while processing the image: {ex.Message}");
        }
    }
}