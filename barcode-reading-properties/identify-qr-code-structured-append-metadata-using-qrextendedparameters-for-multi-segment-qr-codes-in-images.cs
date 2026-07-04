// Title: Identify QR Code Structured-Append Metadata
// Description: Demonstrates how to read QR code structured‑append information from an image using Aspose.BarCode.
// Prompt: Identify QR Code structured‑append metadata using QrExtendedParameters for multi‑segment QR codes in images.
// Tags: qr code, structured-append, barcode recognition, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that reads QR codes from an image and extracts structured‑append metadata
/// using the <c>QrExtendedParameters</c> provided by Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the image containing multi‑segment QR codes.
        const string imagePath = "qr_multi.png";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader configured to decode QR codes only.
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Iterate through all barcodes detected in the image.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output basic barcode information.
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");

                // Access QR structured‑append metadata via the extended parameters.
                var qrExt = result.Extended?.QR;
                if (qrExt != null)
                {
                    // Display the quantity of QR codes that belong to the same structured‑append group.
                    Console.WriteLine($"Structured Append Quantity: {qrExt.QRStructuredAppendModeBarCodesQuantity}");
                    // Display the index of the current QR code within the group (zero‑based).
                    Console.WriteLine($"Structured Append Index: {qrExt.QRStructuredAppendModeBarCodeIndex}");
                    // Display the parity data used for error detection across the group.
                    Console.WriteLine($"Structured Append Parity Data: {qrExt.QRStructuredAppendModeParityData}");
                }
                else
                {
                    // No extended QR parameters were found for this barcode.
                    Console.WriteLine("No QR extended parameters available.");
                }

                Console.WriteLine(); // Separator between results.
            }
        }
    }
}