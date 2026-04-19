using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Output folder for generated QR code images
        string outputFolder = "OutputQR";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Generate a small batch of QR codes with hidden human‑readable text
        for (int i = 1; i <= 5; i++)
        {
            string codeText = $"Sample{i}";
            string filePath = Path.Combine(outputFolder, $"qr_{i}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Hide the main barcode text
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Save the QR code image
                generator.Save(filePath);
            }
        }
    }
}