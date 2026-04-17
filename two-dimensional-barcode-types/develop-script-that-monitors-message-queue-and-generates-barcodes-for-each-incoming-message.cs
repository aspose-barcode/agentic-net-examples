using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Simulated message queue with a few sample messages
        List<string> messages = new List<string>
        {
            "Order12345",
            "Invoice67890",
            "CustomerABC"
        };

        // Ensure output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each message and generate a barcode image
        for (int i = 0; i < messages.Count; i++)
        {
            string message = messages[i];
            string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.png");

            try
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, message))
                {
                    // Set basic generation parameters
                    generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                    generator.Parameters.Barcode.XDimension.Point = 2f;      // Smallest bar width
                    generator.Parameters.Barcode.BarHeight.Point = 100f;   // Height for 1D barcode
                    generator.Parameters.Resolution = 300;                 // DPI

                    // Optional: customize colors
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Save the barcode image
                    generator.Save(filePath);
                }

                Console.WriteLine($"Generated barcode for message '{message}' at '{filePath}'.");
            }
            catch (BarCodeException ex)
            {
                Console.WriteLine($"Failed to generate barcode for message '{message}': {ex.Message}");
            }
        }
    }
}