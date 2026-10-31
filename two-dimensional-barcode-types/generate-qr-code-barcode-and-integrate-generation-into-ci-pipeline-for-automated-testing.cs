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
        // Prepare temporary folder for CI test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeCI_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        string codeText = "https://example.com/ci-test";
        string qrPath = Path.Combine(tempFolder, "qr.png");

        try
        {
            // Generate QR Code
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Save(qrPath, BarCodeImageFormat.Png);
            }

            // Verify QR Code by decoding
            BaseDecodeType decodeType = DecodeType.QR;
            using (var reader = new BarCodeReader(qrPath, decodeType))
            {
                var results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine("FAILED: No barcode detected.");
                }
                else
                {
                    var result = results[0];
                    bool match = string.Equals(result.CodeText, codeText, StringComparison.Ordinal);
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                    Console.WriteLine($"Match Expected: {match}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    if (!match)
                    {
                        Console.WriteLine("FAILED: Decoded text does not match expected.");
                    }
                    else
                    {
                        Console.WriteLine("SUCCESS: QR code generated and verified.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAILED: Exception occurred - {ex.Message}");
        }
        finally
        {
            // Cleanup temporary files
            try
            {
                if (File.Exists(qrPath))
                {
                    File.Delete(qrPath);
                }
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
            catch
            {
                // Ignored - cleanup failure should not affect CI result
            }
        }
    }
}