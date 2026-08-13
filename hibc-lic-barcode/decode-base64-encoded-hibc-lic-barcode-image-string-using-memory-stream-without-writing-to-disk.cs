// Title: Decode a Base64‑encoded HIBC LIC barcode from a memory stream
// Description: Demonstrates how to convert a Base64 string containing a HIBC LIC barcode image into a byte array, load it into a MemoryStream, and decode it using Aspose.BarCode without writing any files to disk.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on in‑memory image processing. It showcases the BarCodeReader class with the DecodeType.HIBCCode128LIC enumeration, a common scenario for applications that receive barcode images via APIs or messaging queues and need to extract data instantly. Developers often use this pattern to avoid I/O overhead when handling barcode images in web services or background jobs.
// Prompt: Decode a base64‑encoded HIBC LIC barcode image string using a memory stream without writing to disk.
// Tags: barcode, hibc, lic, decode, base64, memory stream, aspose.barcode, barcodereader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that decodes a HIBC LIC barcode from a Base64‑encoded image using an in‑memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Converts a Base64 string to a byte array, creates a MemoryStream,
    /// and reads the barcode using Aspose.BarCode's BarCodeReader.
    /// </summary>
    static void Main()
    {
        // Base64‑encoded image of a HIBC LIC barcode.
        // Replace the placeholder with an actual Base64 string when available.
        string base64Image = "iVBORw0KGgoAAAANSUhEUgAA...";

        // Validate that the Base64 string is not empty or whitespace.
        if (string.IsNullOrWhiteSpace(base64Image))
        {
            Console.WriteLine("No base64 image data provided.");
            return;
        }

        byte[] imageBytes;
        try
        {
            // Convert the Base64 string to a byte array.
            imageBytes = Convert.FromBase64String(base64Image);
        }
        catch (FormatException)
        {
            // Handle invalid Base64 format.
            Console.WriteLine("Invalid base64 string.");
            return;
        }

        // Load the image bytes into a memory stream to avoid disk I/O.
        using (var memoryStream = new MemoryStream(imageBytes))
        {
            // Initialize the barcode reader for HIBC Code128 LIC symbology.
            using (var reader = new BarCodeReader(memoryStream, DecodeType.HIBCCode128LIC))
            {
                // Perform the barcode detection.
                var results = reader.ReadBarCodes();

                // Check if any barcodes were found.
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                }
                else
                {
                    // Output each decoded barcode's text.
                    foreach (var result in results)
                    {
                        Console.WriteLine("Decoded CodeText: " + result.CodeText);
                    }
                }
            }
        }
    }
}