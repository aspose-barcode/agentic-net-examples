using System;
using System.IO;
using Aspose.Drawing;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading Australia Post barcodes from a set of PNG files using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes each PNG file, reads barcodes, and prints results.
    /// </summary>
    static void Main()
    {
        // Define the list of PNG files to be processed.
        string[] pngFiles = new string[]
        {
            "barcode1.png",
            "barcode2.png",
            "barcode3.png"
        };

        // Iterate over each file path in the array.
        foreach (string filePath in pngFiles)
        {
            // Verify that the file exists before attempting to process it.
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue; // Skip to the next file if the current one is missing.
            }

            // Load the image from the file into a Bitmap object.
            using (Bitmap bitmap = new Bitmap(filePath))
            {
                // Create a BarCodeReader configured to decode Australia Post barcodes.
                using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
                {
                    // Set the customer information interpreting type to CTable.
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                    // Read all barcodes found in the image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Output the file name and barcode details to the console.
                        Console.WriteLine($"File: {filePath}");
                        Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}