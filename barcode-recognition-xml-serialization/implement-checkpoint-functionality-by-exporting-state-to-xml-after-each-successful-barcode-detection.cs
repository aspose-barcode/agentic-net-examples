using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using System.IO;

class Program
{
    static void Main()
    {
        // Sample data to encode
        string[] codes = { "ABC123", "XYZ789" };
        // Loop through each code, generate barcode, detect it and export state
        for (int i = 0; i < codes.Length; i++)
        {
            string code = codes[i];
            string imagePath = $"barcode_{i}.png";
            string xmlPath = $"checkpoint_{i}.xml";

            // Create barcode generator, set code text and save image
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = code;
                generator.Save(imagePath);
                // After successful generation, we will export state after detection
                // Detect the barcode from the saved image
                using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
                {
                    // Enable checksum validation (optional)
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Successful detection
                        Console.WriteLine($"Detected CodeText: {result.CodeText}");
                        // Export generator state to XML as a checkpoint
                        bool exported = generator.ExportToXml(xmlPath);
                        Console.WriteLine($"Exported checkpoint to '{xmlPath}': {exported}");
                    }
                }
            }
        }
    }
}