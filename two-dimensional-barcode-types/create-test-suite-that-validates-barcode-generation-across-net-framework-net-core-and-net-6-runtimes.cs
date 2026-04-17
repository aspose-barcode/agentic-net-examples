using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Prepare output folder
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define test cases: symbology, code text, optional file name suffix
        var testCases = new List<(BaseEncodeType type, string codeText, string suffix)>
        {
            (EncodeTypes.Code128, "ABC123456", "code128"),
            (EncodeTypes.QR, "https://example.com", "qr"),
            (EncodeTypes.EAN13, "5901234123457", "ean13"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "pdf417"),
            (EncodeTypes.DataMatrix, "DataMatrix123", "datamatrix")
        };

        int passed = 0;
        int failed = 0;

        foreach (var (type, codeText, suffix) in testCases)
        {
            string filePath = Path.Combine(outputDir, $"{suffix}.png");

            // Generate barcode
            using (BarcodeGenerator generator = new BarcodeGenerator(type, codeText))
            {
                // Common settings
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;
                generator.Parameters.Resolution = 96f;

                // 1D specific: set bar height
                if (type == EncodeTypes.Code128 || type == EncodeTypes.EAN13)
                {
                    generator.Parameters.Barcode.BarHeight.Point = 50f;
                }

                // Save image
                generator.Save(filePath);
            }

            // Verify by reading back
            try
            {
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    bool matchFound = false;
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        if (result.CodeText == codeText)
                        {
                            matchFound = true;
                            break;
                        }
                    }

                    if (matchFound)
                    {
                        Console.WriteLine($"[PASS] {suffix}: generated and verified successfully.");
                        passed++;
                    }
                    else
                    {
                        Console.WriteLine($"[FAIL] {suffix}: decoded text does not match.");
                        failed++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {suffix}: exception during verification - {ex.Message}");
                failed++;
            }
        }

        Console.WriteLine($"Test Summary: Passed = {passed}, Failed = {failed}");
    }
}