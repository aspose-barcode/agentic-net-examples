using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Binary data to encode
        byte[] originalData = new byte[] { 0x01, 0x02, 0xFF, 0x00, 0xAB };

        // Output file path
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "dotcode.png");

        // Create DotCode barcode with binary mode
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode))
        {
            // Set raw bytes as codetext
            generator.SetCodeText(originalData);

            // Use Binary encode mode
            generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.Binary;

            // Save the barcode image
            generator.Save(outputPath);
        }

        // Verify by reading the barcode back
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        using (var reader = new BarCodeReader(outputPath, DecodeType.DotCode))
        {
            bool matchFound = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Convert the decoded string back to bytes using ISO-8859-1 (one-to-one mapping)
                byte[] decodedBytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(result.CodeText);

                // Compare with original data
                if (decodedBytes.Length == originalData.Length)
                {
                    matchFound = true;
                    for (int i = 0; i < decodedBytes.Length; i++)
                    {
                        if (decodedBytes[i] != originalData[i])
                        {
                            matchFound = false;
                            break;
                        }
                    }
                }

                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                Console.WriteLine($"Verification result: {(matchFound ? "Success" : "Failure")}");
            }

            if (!matchFound)
            {
                Console.WriteLine("No matching barcode found or data mismatch.");
            }
        }
    }
}