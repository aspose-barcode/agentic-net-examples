using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing; // Required for Bitmap if needed

/// <summary>
/// Demonstrates how to read and decode Mailmark barcodes from an image file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads a JPEG image, detects Mailmark barcodes, and prints decoded fields.
    /// </summary>
    static void Main()
    {
        // Path to the JPEG image containing a Mailmark barcode.
        const string imagePath = "mailmark.jpg";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Open the image as a read‑only stream to avoid locking the file.
        using (FileStream imageStream = File.OpenRead(imagePath))
        {
            // Initialize the barcode reader for Mailmark symbology.
            using (BarCodeReader reader = new BarCodeReader(imageStream, DecodeType.Mailmark))
            {
                // Perform recognition and retrieve all detected barcodes.
                BarCodeResult[] results = reader.ReadBarCodes();

                // If no barcodes were found, inform the user and exit.
                if (results.Length == 0)
                {
                    Console.WriteLine("No Mailmark barcode detected.");
                    return;
                }

                // Iterate over each detected barcode result.
                foreach (BarCodeResult result in results)
                {
                    // Output the raw codetext as read from the barcode.
                    Console.WriteLine($"Raw CodeText: {result.CodeText}");

                    // Attempt to decode the complex Mailmark codetext into a structured object.
                    MailmarkCodetext mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                    if (mailmark != null)
                    {
                        // Successfully decoded; display each field of the Mailmark.
                        Console.WriteLine("Decoded Mailmark fields:");
                        Console.WriteLine($"  Format: {mailmark.Format}");
                        Console.WriteLine($"  VersionID: {mailmark.VersionID}");
                        Console.WriteLine($"  Class: {mailmark.Class}");
                        Console.WriteLine($"  SupplychainID: {mailmark.SupplychainID}");
                        Console.WriteLine($"  ItemID: {mailmark.ItemID}");
                        Console.WriteLine($"  DestinationPostCodePlusDPS: {mailmark.DestinationPostCodePlusDPS}");
                    }
                    else
                    {
                        // Decoding failed; inform the user.
                        Console.WriteLine("Failed to decode Mailmark codetext into structured object.");
                    }
                }
            }
        }
    }
}