using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    // Simulated API endpoint that processes an uploaded image stream
    static List<(string TypeName, string CodeText)> ProcessBarcodeImage(Stream imageStream)
    {
        if (imageStream == null)
            throw new ArgumentException("Image stream cannot be null.", nameof(imageStream));

        // Ensure the stream is positioned at the beginning
        if (imageStream.CanSeek)
            imageStream.Position = 0;

        var results = new List<(string, string)>();

        using (var reader = new BarCodeReader(imageStream))
        {
            // Specify which barcode types to look for (example set)
            reader.SetBarCodeReadType(DecodeType.Code128, DecodeType.Code39, DecodeType.QR, DecodeType.EAN13);

            // Perform recognition
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                results.Add((result.CodeTypeName, result.CodeText));
            }
        }

        return results;
    }

    static void Main()
    {
        // Generate a sample barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Save barcode to a memory stream as PNG
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);

                // Simulate receiving the image stream via a web API call
                List<(string TypeName, string CodeText)> detectedBarcodes = ProcessBarcodeImage(imageStream);

                // Output detection results
                if (detectedBarcodes.Count == 0)
                {
                    Console.WriteLine("No barcodes were detected.");
                }
                else
                {
                    foreach (var (typeName, codeText) in detectedBarcodes)
                    {
                        Console.WriteLine($"Detected Barcode Type: {typeName}");
                        Console.WriteLine($"Detected Barcode Text: {codeText}");
                    }
                }
            }
        }
    }
}