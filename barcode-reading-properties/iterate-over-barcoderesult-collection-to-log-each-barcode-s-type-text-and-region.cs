using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a sample barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Save barcode to a memory stream as PNG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Read barcodes from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // Perform recognition
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Iterate over each recognized barcode and log details
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine("Barcode Type: " + result.CodeTypeName);
                        Console.WriteLine("Barcode Text: " + result.CodeText);
                        Console.WriteLine("Barcode Region: " + result.Region.Rectangle);
                        Console.WriteLine(); // Blank line for readability
                    }
                }
            }
        }
    }
}