using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the image containing multiple barcodes
        string imagePath = "mixed_barcodes.png";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Define the expected barcode symbology names (as returned by CodeTypeName)
        string[] expectedTypes = new string[] { "Code128", "QRCode", "EAN13", "DataMatrix" };

        // Initialize the reader for all supported barcode types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform recognition
            reader.ReadBarCodes();

            // Retrieve recognized barcodes
            var foundBarCodes = reader.FoundBarCodes;
            Console.WriteLine($"Total barcodes found: {foundBarCodes.Length}");

            // Validate that each expected type is present
            foreach (string expected in expectedTypes)
            {
                bool present = false;
                foreach (var result in foundBarCodes)
                {
                    if (string.Equals(result.CodeTypeName, expected, StringComparison.OrdinalIgnoreCase))
                    {
                        present = true;
                        break;
                    }
                }

                Console.WriteLine(present
                    ? $"Expected barcode type '{expected}' found."
                    : $"Expected barcode type '{expected}' NOT found.");
            }

            // Report any unexpected barcode types
            foreach (var result in foundBarCodes)
            {
                bool isExpected = false;
                foreach (string expected in expectedTypes)
                {
                    if (string.Equals(result.CodeTypeName, expected, StringComparison.OrdinalIgnoreCase))
                    {
                        isExpected = true;
                        break;
                    }
                }

                if (!isExpected)
                {
                    Console.WriteLine($"Unexpected barcode type detected: {result.CodeTypeName}");
                }
            }
        }
    }
}