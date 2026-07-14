// Title: QR Code Recognition from PNG using BarCodeReader
// Description: Demonstrates loading a saved QR Code PNG image and recognizing its content with Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating how to use BarCodeReader with DecodeType to identify QR symbology. It shows typical steps such as file validation, reader initialization, and result processing—common tasks for developers integrating barcode scanning into .NET applications.
// Prompt: Load a saved QR Code PNG image into BarCodeReader and set DecodeType to QR for recognition.
// Tags: qr, barcode, recognition, png, decode, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates loading a saved QR Code PNG image and recognizing it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Validates the image file, reads QR codes, and prints detected values.
    /// </summary>
    static void Main()
    {
        // Path to the saved QR Code PNG image
        string imagePath = "qr.png";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize BarCodeReader with the image file and specify QR as the decode type
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Perform the recognition operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Check if any QR codes were detected and output the results
            if (results.Length == 0)
            {
                Console.WriteLine("No QR code detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected QR Code: {result.CodeText}");
                }
            }
        }
    }
}