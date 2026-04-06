using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define the data for each QR segment
        string[] segments = { "First part of message", "Second part of message", "Third part of message" };
        int totalSegments = segments.Length;

        // Folder to store temporary QR images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeQrStructuredAppend");
        Directory.CreateDirectory(tempFolder);

        // Generate QR codes with Structured Append parameters
        for (int i = 0; i < totalSegments; i++)
        {
            string filePath = Path.Combine(tempFolder, $"qr_segment_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.CodeText = segments[i];

                // Set Structured Append parameters
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = totalSegments;      // total number of segments
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i;        // index of this segment (0‑based)
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = 0;               // parity (optional, 0 is acceptable)

                // Save the segment image
                generator.Save(filePath);
            }
        }

        // Read each generated QR image and display Structured Append metadata
        Console.WriteLine("Reading QR segments and displaying Structured Append metadata:");
        for (int i = 0; i < totalSegments; i++)
        {
            string filePath = Path.Combine(tempFolder, $"qr_segment_{i}.png");
            using (var reader = new BarCodeReader(filePath, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    Console.WriteLine($"  Structured Append Quantity: {result.Extended.QR.QRStructuredAppendModeBarCodesQuantity}");
                    Console.WriteLine($"  Structured Append Index: {result.Extended.QR.QRStructuredAppendModeBarCodeIndex}");
                    Console.WriteLine($"  Structured Append ParityData: {result.Extended.QR.QRStructuredAppendModeParityData}");
                }
            }
        }

        // Cleanup temporary files (optional)
        // Directory.Delete(tempFolder, true);
    }
}