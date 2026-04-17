using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Output folder for generated barcodes
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Sample batch of Code128 texts
        string[] codeTexts = new string[]
        {
            "ABC123",
            "9876543210",
            "CODE128TEST",
            "12345ABCD",
            "ZXCVBNM"
        };

        // Process each barcode in the batch
        for (int i = 0; i < codeTexts.Length; i++)
        {
            string text = codeTexts[i];
            string filePath = Path.Combine(outputFolder, $"Code128_{i + 1}.png");

            // Create a BarcodeGenerator for Code128
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Hide captions globally for this generator
                generator.Parameters.CaptionAbove.Visible = false;
                generator.Parameters.CaptionBelow.Visible = false;

                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Saved barcode '{text}' to '{filePath}'");
        }

        Console.WriteLine("All barcodes have been generated with captions hidden.");
    }
}