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
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Read the barcode with a timeout of 200 ms
                try
                {
                    using (var reader = new BarCodeReader(ms))
                    {
                        reader.Timeout = 200; // milliseconds
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"BarCode Type: {result.CodeTypeName}, CodeText: {result.CodeText}");
                        }
                    }
                }
                catch (RecognitionAbortedException ex)
                {
                    // Log timeout occurrence
                    Console.WriteLine($"Recognition timed out after {ex.ExecutionTime} ms.");
                }
            }
        }
    }
}