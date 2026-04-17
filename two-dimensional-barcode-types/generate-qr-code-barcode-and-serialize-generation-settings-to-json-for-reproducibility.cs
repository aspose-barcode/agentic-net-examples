using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define output file names
        const string imagePath = "qr.png";
        const string jsonPath = "qr_settings.json";

        // Create a QR Code generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose QR!"))
        {
            // Set QR error correction level to high
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optionally adjust image size
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save the QR Code image
            generator.Save(imagePath);

            // Serialize generation parameters to JSON for reproducibility
            var json = JsonSerializer.Serialize(
                generator.Parameters,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(jsonPath, json);
        }

        Console.WriteLine($"QR Code image saved to: {Path.GetFullPath(imagePath)}");
        Console.WriteLine($"Generation settings saved to: {Path.GetFullPath(jsonPath)}");
    }
}