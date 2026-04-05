using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define barcode content and symbology
        const string codeText = "1234567890";

        // Create a unique file name with timestamp
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
        string fileName = $"barcode_{timestamp}.png";
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        // Generate barcode with custom colors
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set foreground (bars) color
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set background color
            generator.Parameters.BackColor = Color.Yellow;

            // Save the barcode image
            generator.Save(outputPath);
        }

        // Optionally inform the user (no waiting for input)
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}