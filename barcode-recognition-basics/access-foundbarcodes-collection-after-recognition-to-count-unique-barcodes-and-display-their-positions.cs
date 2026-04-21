using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare a sample barcode image file
        string imagePath = "sample.png";

        // Generate a barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            // Ensure the image is saved as PNG
            generator.Save(imagePath);
        }

        // Verify that the image file exists before attempting recognition
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image file '{imagePath}' not found.");
            return;
        }

        // Dictionary to hold unique code texts and their detected positions
        var barcodePositions = new Dictionary<string, List<Rectangle>>();

        // Read barcodes from the image using all supported types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform recognition
            reader.ReadBarCodes();

            // Iterate over the recognized barcodes
            for (int i = 0; i < reader.FoundCount; i++)
            {
                var result = reader.FoundBarCodes[i];
                string codeText = result.CodeText ?? string.Empty;
                Rectangle rect = result.Region.Rectangle;

                if (!barcodePositions.ContainsKey(codeText))
                {
                    barcodePositions[codeText] = new List<Rectangle>();
                }

                barcodePositions[codeText].Add(rect);
            }
        }

        // Output the count of unique barcodes
        Console.WriteLine($"Unique barcodes found: {barcodePositions.Count}");

        // Display each unique barcode and its positions
        foreach (var kvp in barcodePositions)
        {
            string code = kvp.Key;
            List<Rectangle> rects = kvp.Value;

            Console.WriteLine($"CodeText: {code}");
            for (int idx = 0; idx < rects.Count; idx++)
            {
                Rectangle r = rects[idx];
                Console.WriteLine($"  Position {idx + 1}: X={r.X}, Y={r.Y}, Width={r.Width}, Height={r.Height}");
            }
        }
    }
}