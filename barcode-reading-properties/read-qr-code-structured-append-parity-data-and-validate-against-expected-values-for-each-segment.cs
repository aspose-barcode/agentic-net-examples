using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Folder to store generated QR code images
        string outputFolder = Path.Combine(Path.GetTempPath(), "QrStructuredAppendDemo");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Define structured append parameters
        const int totalSegments = 3;
        const byte expectedParity = 0xAB; // Example parity byte to validate against
        string[] segmentTexts = { "Segment0", "Segment1", "Segment2" };

        // -----------------------------------------------------------------
        // Generate QR code images with structured append settings
        // -----------------------------------------------------------------
        for (int i = 0; i < totalSegments; i++)
        {
            string filePath = Path.Combine(outputFolder, $"qr_{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, segmentTexts[i]))
            {
                // Set QR structured append parameters
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = totalSegments;
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i;
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = expectedParity;

                // Save the barcode image
                generator.Save(filePath);
            }
        }

        // -----------------------------------------------------------------
        // Read each QR code and validate the parity data
        // -----------------------------------------------------------------
        Console.WriteLine("Validating QR Structured Append parity data:");
        for (int i = 0; i < totalSegments; i++)
        {
            string filePath = Path.Combine(outputFolder, $"qr_{i}.png");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    int parity = result.Extended.QR.StructuredAppendModeParityData;
                    int quantity = result.Extended.QR.StructuredAppendModeBarCodesQuantity;
                    int index = result.Extended.QR.StructuredAppendModeBarCodeIndex;

                    bool parityMatches = parity == expectedParity;

                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Expected Parity: {expectedParity}");
                    Console.WriteLine($"  Detected Parity: {parity}  => {(parityMatches ? "OK" : "MISMATCH")}");
                    Console.WriteLine($"  Structured Append Quantity: {quantity}");
                    Console.WriteLine($"  Structured Append Index: {index}");
                }
            }
        }

        // Cleanup: optional removal of generated files
        // Uncomment the following lines if you want to delete the temporary images after validation
        // foreach (var file in Directory.GetFiles(outputFolder, "qr_*.png"))
        // {
        //     File.Delete(file);
        // }
        // Directory.Delete(outputFolder);
    }
}