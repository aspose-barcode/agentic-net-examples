using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates reading a Swiss QR Code image, extracting the raw encoded text,
/// and decoding it into a structured Swiss QR Bill object.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the Swiss QR Code image file.
        string imagePath = "SwissQR.png";

        // Verify that the specified image file exists before proceeding.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        try
        {
            // Initialize a barcode reader configured to decode QR codes.
            using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
            {
                // Read all barcodes present in the image.
                BarCodeResult[] results = reader.ReadBarCodes();

                // If no barcodes were detected, inform the user and exit.
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No barcode detected in the image.");
                    return;
                }

                // Assume the first detected barcode corresponds to the Swiss QR Code.
                var result = results[0];
                string rawEncodedText = result.CodeText;
                Console.WriteLine($"Raw encoded text: {rawEncodedText}");

                // Attempt to decode the raw Swiss QR codetext into a structured object.
                SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(rawEncodedText);

                // If decoding fails, notify the user.
                if (decoded == null)
                {
                    Console.WriteLine("Failed to decode Swiss QR codetext.");
                }
                else
                {
                    // Display selected fields from the decoded Swiss QR Bill.
                    Console.WriteLine("Decoded Swiss QR Bill:");
                    Console.WriteLine($"  Account: {decoded.Bill.Account}");
                    Console.WriteLine($"  Amount: {decoded.Bill.Amount}");
                    Console.WriteLine($"  Creditor Name: {decoded.Bill.Creditor.Name}");
                    Console.WriteLine($"  Creditor Country Code: {decoded.Bill.Creditor.CountryCode}");
                    Console.WriteLine($"  Version: {decoded.Bill.Version}");
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors that occur during reading or decoding.
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}