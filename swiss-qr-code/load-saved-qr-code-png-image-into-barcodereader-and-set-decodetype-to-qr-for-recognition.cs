using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to read a QR code from an image file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the path to the QR code image (PNG format)
        string imagePath = "qr.png";

        // Check that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            // Inform the user if the file cannot be found and exit
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for the specified image, limiting decoding to QR codes
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Read all barcodes present in the image
            BarCodeResult[] results = reader.ReadBarCodes();

            // Determine whether any QR codes were detected
            if (results.Length == 0)
            {
                // No QR code found – notify the user
                Console.WriteLine("No QR code detected in the image.");
            }
            else
            {
                // Iterate through each detected barcode and display its details
                foreach (var result in results)
                {
                    Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                    Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                }
            }
        }
    }
}