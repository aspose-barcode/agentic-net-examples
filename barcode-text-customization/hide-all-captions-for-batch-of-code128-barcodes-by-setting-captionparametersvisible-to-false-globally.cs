using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Folder to store generated barcodes
        string outputFolder = "Barcodes";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Sample batch of Code128 values
        string[] codeTexts = new string[]
        {
            "ABC123",
            "9876543210",
            "CODE128TEST",
            "12345XYZ"
        };

        foreach (string text in codeTexts)
        {
            // Create a Code128 barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the code text to encode
                generator.CodeText = text;

                // Hide both above and below captions globally
                generator.Parameters.CaptionAbove.Visible = false;
                generator.Parameters.CaptionBelow.Visible = false;

                // Save the barcode image
                string filePath = Path.Combine(outputFolder, $"{text}.png");
                generator.Save(filePath);
                Console.WriteLine($"Saved barcode for \"{text}\" to \"{filePath}\"");
            }
        }
    }
}