// Title: Multi-Decode Barcode Reader for UPC-A, UPC-E, and EAN-8
// Description: Demonstrates configuring Aspose.BarCode's MultiDecodeType to recognize UPC-A, UPC-E, and EAN-8 barcodes, useful for retail scanning scenarios.
// Prompt: Configure MultyDecodeType to include UPC-A, UPC-E, and EAN-8 for comprehensive retail barcode scanning.
// Tags: barcode symbology, decoding, upc-a, upc-e, ean-8, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates sample UPC-A, UPC-E, and EAN-8 barcodes
/// and then reads them using a MultiDecodeType configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcode images, configures the reader,
    /// and outputs detected barcode types and values.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Generate sample barcode images for UPC-A, UPC-E, and EAN-8
        // --------------------------------------------------------------------
        GenerateSampleBarcodes();

        // --------------------------------------------------------------------
        // Define the list of image files to be scanned
        // --------------------------------------------------------------------
        string[] files = { "upca.png", "upce.png", "ean8.png" };

        // --------------------------------------------------------------------
        // Configure the barcode reader to detect the three desired symbologies
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader())
        {
            // MultiDecodeType includes UPC-A, UPC-E, and EAN-8
            reader.BarCodeReadType = new MultiDecodeType(DecodeType.UPCA, DecodeType.UPCE, DecodeType.EAN8);

            // Iterate over each file and attempt recognition
            foreach (var file in files)
            {
                // Verify that the file exists before processing
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                // Assign the current image to the reader
                reader.SetBarCodeImage(file);

                // Perform recognition and output results
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {file}");
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine();
                }
            }
        }
    }

    // ------------------------------------------------------------------------
    // Generates sample barcode images for demonstration purposes
    // ------------------------------------------------------------------------
    private static void GenerateSampleBarcodes()
    {
        // UPC-A (12 digits, last digit is checksum; generator can calculate it)
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "01234567890"))
        {
            generator.Save("upca.png");
        }

        // UPC-E (6 digits, generator will expand to UPC-A internally)
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCE, "0123456"))
        {
            generator.Save("upce.png");
        }

        // EAN-8 (7 digits, checksum calculated automatically)
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN8, "1234567"))
        {
            generator.Save("ean8.png");
        }
    }
}