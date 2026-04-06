using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Create a barcode reader with a very short timeout to trigger abort
                using (var reader = new BarCodeReader())
                {
                    reader.Timeout = 1; // timeout in milliseconds
                    reader.SetBarCodeImage(ms);

                    try
                    {
                        // Attempt to read barcodes; may throw RecognitionAbortedException
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                        }
                    }
                    catch (RecognitionAbortedException ex)
                    {
                        // Handle the abort scenario
                        Console.WriteLine($"Recognition aborted: {ex.Message}");
                        Console.WriteLine($"Execution time (ms): {ex.ExecutionTime}");
                    }
                }
            }
        }
    }
}