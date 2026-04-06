using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define QR structured append parameters
        int totalSegments = 3;
        byte parityByte = 0xAB; // Expected parity data for all segments
        string[] segmentTexts = { "First part of data", "Second part of data", "Third part of data" };
        string[] fileNames = { "qr_part1.png", "qr_part2.png", "qr_part3.png" };

        // Generate QR code images with structured append settings
        for (int i = 0; i < totalSegments; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the data for this segment
                generator.CodeText = segmentTexts[i];

                // Configure structured append parameters
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = totalSegments;
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i; // index starts from 0
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = parityByte;

                // Optional: set a simple size for the image
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;

                // Save the image
                generator.Save(fileNames[i]);
            }
        }

        // Read each QR code and validate structured append data
        for (int i = 0; i < totalSegments; i++)
        {
            using (var reader = new BarCodeReader(fileNames[i], DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    int readQuantity = result.Extended.QR.QRStructuredAppendModeBarCodesQuantity;
                    int readIndex = result.Extended.QR.QRStructuredAppendModeBarCodeIndex;
                    int readParity = result.Extended.QR.QRStructuredAppendModeParityData;

                    bool quantityMatches = readQuantity == totalSegments;
                    bool indexMatches = readIndex == i;
                    bool parityMatches = readParity == parityByte;

                    Console.WriteLine($"File: {fileNames[i]}");
                    Console.WriteLine($"  Expected Quantity: {totalSegments}, Read: {readQuantity} => {(quantityMatches ? "OK" : "FAIL")}");
                    Console.WriteLine($"  Expected Index: {i}, Read: {readIndex} => {(indexMatches ? "OK" : "FAIL")}");
                    Console.WriteLine($"  Expected Parity: {parityByte}, Read: {readParity} => {(parityMatches ? "OK" : "FAIL")}");
                }
            }
        }

        // Clean up generated files (optional)
        foreach (string file in fileNames)
        {
            if (File.Exists(file))
                File.Delete(file);
        }
    }
}