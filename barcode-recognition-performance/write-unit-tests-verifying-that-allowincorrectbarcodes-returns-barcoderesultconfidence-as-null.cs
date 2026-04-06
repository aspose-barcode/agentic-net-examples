using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate an EAN13 barcode with an incorrect checksum.
        // Correct checksum for "123456789012" is 8, we use 3 to make it invalid.
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890123"))
            {
                // Save the barcode to a memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Prepare the stream for reading.
            ms.Position = 0;

            // Read the barcode with AllowIncorrectBarcodes enabled.
            using (var reader = new BarCodeReader(ms, DecodeType.EAN13))
            {
                // Enable recognition of incorrect barcodes.
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                var results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("Test Failed: No barcode detected.");
                    return;
                }

                foreach (var result in results)
                {
                    // Expect the confidence to be None (equivalent to null for incorrect barcodes).
                    if (result.Confidence == BarCodeConfidence.None)
                    {
                        Console.WriteLine("Test Passed: Confidence is None as expected.");
                    }
                    else
                    {
                        Console.WriteLine($"Test Failed: Expected Confidence=None, but got {result.Confidence}.");
                    }
                }
            }
        }
    }
}