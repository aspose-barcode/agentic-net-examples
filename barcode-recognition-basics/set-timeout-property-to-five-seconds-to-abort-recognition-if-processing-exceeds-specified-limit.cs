using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a Code128 barcode and store it in a memory stream.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Initialize the BarCodeReader with the generated image stream.
                using (var reader = new BarCodeReader(ms))
                {
                    // Set the timeout to 5000 milliseconds (5 seconds).
                    reader.Timeout = 5000;

                    try
                    {
                        // Read barcodes; aborts if processing exceeds the timeout.
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                    catch (RecognitionAbortedException ex)
                    {
                        // Handle the timeout scenario.
                        Console.WriteLine($"Recognition aborted after {ex.ExecutionTime} ms: {ex.Message}");
                    }
                }
            }
        }
    }
}