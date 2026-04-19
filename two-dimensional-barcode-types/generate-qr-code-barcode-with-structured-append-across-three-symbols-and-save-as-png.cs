using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

namespace QRStructuredAppendExample
{
    class Program
    {
        static void Main()
        {
            // Parts of the data to be split across three QR symbols
            string[] parts = new string[]
            {
                "Part 1: This is the first segment of the message.",
                "Part 2: This is the second segment of the message.",
                "Part 3: This is the third segment of the message."
            };

            // Total number of QR symbols in the structured append sequence
            const int totalCount = 3;

            for (int i = 0; i < totalCount; i++)
            {
                // Create a QR barcode generator for each segment
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
                {
                    // Set the data for this segment
                    generator.CodeText = parts[i];

                    // Configure Structured Append parameters
                    generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = totalCount;
                    generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i; // Index starts from 0
                    generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = 0; // Optional parity, set to 0

                    // Optional: set error correction level (e.g., LevelM)
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                    // Save each QR symbol as a separate PNG file
                    string fileName = $"qr_part{i}.png";
                    generator.Save(fileName, BarCodeImageFormat.Png);
                }
            }

            Console.WriteLine("QR structured append symbols have been generated.");
        }
    }
}