using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Original Australia Post barcode text (CTable encoding)
        const string originalCodeText = "5912345678AB";

        // Generate the barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, originalCodeText))
        {
            // Set CTable interpreting type
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Generate bitmap
            using (var barcodeImage = generator.GenerateBarCodeImage())
            {
                // Corrupt a small area of the image to simulate damage
                using (var graphics = Graphics.FromImage(barcodeImage))
                {
                    // Draw a white rectangle over the middle of the barcode
                    var rect = new Rectangle(barcodeImage.Width / 3, barcodeImage.Height / 3,
                                             barcodeImage.Width / 3, barcodeImage.Height / 3);
                    graphics.FillRectangle(Brushes.White, rect);
                }

                // Decode the possibly damaged barcode
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.AustraliaPost))
                {
                    // Configure decoding to use the same interpreting type
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    // Enable checksum validation (optional but safe)
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    string decodedText = null;
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        decodedText = result.CodeText;
                        break; // Only need the first result
                    }

                    // Verify that the decoded text matches the original
                    if (decodedText == originalCodeText)
                    {
                        Console.WriteLine("Success: Reed‑Solomon error correction recovered the original code text.");
                    }
                    else if (decodedText == null)
                    {
                        Console.WriteLine("Failure: No barcode could be decoded.");
                    }
                    else
                    {
                        Console.WriteLine($"Failure: Decoded text '{decodedText}' does not match original '{originalCodeText}'.");
                    }
                }
            }
        }
    }
}