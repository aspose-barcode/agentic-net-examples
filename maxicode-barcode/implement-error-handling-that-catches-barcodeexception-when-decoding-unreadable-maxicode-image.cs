using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Path to the MaxiCode image (replace with an actual file path if needed)
        string imagePath = "unreadable_maxicode.png";

        // Validate that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        try
        {
            // Create a BarCodeReader for MaxiCode symbology
            using (var reader = new BarCodeReader(imagePath, DecodeType.MaxiCode))
            {
                // Read all barcodes from the image
                var results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }

                foreach (var result in results)
                {
                    // Attempt to decode the MaxiCode codetext using ComplexCodetextReader
                    var decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                        result.Extended.MaxiCode.MaxiCodeMode,
                        result.CodeText);

                    if (decoded != null)
                    {
                        Console.WriteLine($"Decoded MaxiCode codetext: {decoded.GetConstructedCodetext()}");
                    }
                    else
                    {
                        Console.WriteLine("Failed to decode MaxiCode codetext.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during decoding (e.g., unreadable image)
            Console.WriteLine($"Error decoding barcode: {ex.Message}");
        }
    }
}