// Title: EAN13 Barcode Generation and Recognition Example
// Description: Demonstrates generating an EAN13 barcode image and using BarCodeReader with DecodeType set to EAN13 to detect only European Article Number barcodes.
// Prompt: Use BarCodeReader with DecodeType set to EAN13 to exclusively detect European Article Number barcodes.
// Tags: barcode symbology, ean13, generation, recognition, aspose.barcode, console

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates an EAN13 barcode and reads it using <see cref="BarCodeReader"/> configured for EAN13 only.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, configures the reader, and outputs detected barcode information.
    /// </summary>
    static void Main()
    {
        // Generate a sample EAN13 barcode image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.EAN13, "123456789012"))
        {
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Initialize the reader and configure it to detect only EAN13 barcodes
                using (BarCodeReader reader = new BarCodeReader())
                {
                    // Restrict decoding to EAN13 symbology
                    reader.BarCodeReadType = DecodeType.EAN13;

                    // Provide the generated image to the reader
                    reader.SetBarCodeImage(barcodeImage);

                    // Perform recognition and output results
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("Detected Type: " + result.CodeTypeName);
                        Console.WriteLine("Detected CodeText: " + result.CodeText);
                    }
                }
            }
        }
    }
}