// Title: Decode Mailmark barcode from JPEG image
// Description: Demonstrates how to read a Mailmark barcode from a JPEG file and display its raw and parsed data.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on Mailmark symbology. It showcases the use of BarCodeReader with DecodeType.Mailmark and ComplexCodetextReader to extract structured information. Developers working with postal or logistics barcodes can use this pattern to integrate Mailmark decoding into .NET applications.
// Prompt: Decode a Mailmark barcode from a JPEG image and output the result to the console.
// Tags: mailmark, barcode, decoding, console, aspose.barcode, complexcodetextreader

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Program to decode a Mailmark barcode from a JPEG image and display the results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads the image, decodes Mailmark barcodes, and prints raw and parsed data.
    /// </summary>
    static void Main()
    {
        // Path to the JPEG image containing the Mailmark barcode
        const string imagePath = "mailmark.jpg";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader configured for Mailmark decoding
        using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
        {
            // Read all Mailmark barcodes present in the image
            var results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit
            if (results.Length == 0)
            {
                Console.WriteLine("No Mailmark barcode detected.");
                return;
            }

            // Process each detected barcode
            foreach (var result in results)
            {
                // Output the raw decoded text from the barcode
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");

                // Attempt to parse the raw text into structured Mailmark fields
                var mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                if (mailmark != null)
                {
                    // Display the parsed Mailmark details
                    Console.WriteLine("Mailmark Details:");
                    Console.WriteLine($"  Format: {mailmark.Format}");
                    Console.WriteLine($"  VersionID: {mailmark.VersionID}");
                    Console.WriteLine($"  Class: {mailmark.Class}");
                    Console.WriteLine($"  SupplychainID: {mailmark.SupplychainID}");
                    Console.WriteLine($"  ItemID: {mailmark.ItemID}");
                    Console.WriteLine($"  DestinationPostCodePlusDPS: {mailmark.DestinationPostCodePlusDPS}");
                }
                else
                {
                    // Inform the user if parsing failed
                    Console.WriteLine("Failed to parse Mailmark codetext into structured data.");
                }
            }
        }
    }
}