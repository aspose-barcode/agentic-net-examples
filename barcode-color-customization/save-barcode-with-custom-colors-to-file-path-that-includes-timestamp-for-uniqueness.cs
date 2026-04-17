using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeSample
{
    class Program
    {
        static void Main()
        {
            // Barcode content
            const string codeText = "1234567890";

            // Create a unique file name with timestamp
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
            string fileName = $"barcode_{timestamp}.png";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

            // Generate the barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set custom colors
                generator.Parameters.Barcode.BarColor = Color.Blue;   // Foreground (bars)
                generator.Parameters.BackColor = Color.Yellow;       // Background

                // Save the barcode image
                generator.Save(filePath);
            }

            Console.WriteLine($"Barcode saved to: {filePath}");
        }
    }
}