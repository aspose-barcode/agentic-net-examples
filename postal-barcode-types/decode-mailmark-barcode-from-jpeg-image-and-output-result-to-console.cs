using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates how to read and decode Mailmark barcodes from an image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads a JPEG image, detects Mailmark barcodes, and prints decoded fields.
    /// </summary>
    static void Main()
    {
        // Path to the JPEG image containing the Mailmark barcode
        const string imagePath = "mailmark.jpg";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader for the Mailmark symbology using the specified image
        using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
        {
            // Read all barcodes detected in the image
            var results = reader.ReadBarCodes();

            // If no barcodes were found, inform the user and exit
            if (results.Length == 0)
            {
                Console.WriteLine("No Mailmark barcode detected.");
                return;
            }

            // Process each detected barcode
            foreach (var result in results)
            {
                // Output the raw decoded text of the barcode
                Console.WriteLine($"Raw CodeText: {result.CodeText}");

                // Attempt to decode the complex Mailmark codetext into its constituent fields
                var mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                if (mailmark != null)
                {
                    // Successfully decoded; display each field
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
                    // Decoding failed; notify the user
                    Console.WriteLine("Failed to decode Mailmark complex codetext.");
                }
            }
        }
    }
}