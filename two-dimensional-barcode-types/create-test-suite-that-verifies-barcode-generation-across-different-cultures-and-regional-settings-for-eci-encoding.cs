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
        var testCases = new List<(string Text, ECIEncodings Encoding)>
        {
            ("Hello World", ECIEncodings.US_ASCII),
            ("Привет мир", ECIEncodings.UTF8),
            ("こんにちは世界", ECIEncodings.Shift_JIS),
            ("¡Hola Mundo!", ECIEncodings.ISO_8859_1),
            ("中文测试", ECIEncodings.GB18030)
        };

        int passed = 0;
        int failed = 0;

        foreach (var (text, encoding) in testCases)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.CodeText = text;
                generator.Parameters.Barcode.QR.ECIEncoding = encoding;

                using (MemoryStream ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.QR))
                    {
                        reader.BarcodeSettings.DetectEncoding = true;

                        bool matchFound = false;
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            if (result.CodeText == text)
                            {
                                matchFound = true;
                                break;
                            }
                        }

                        if (matchFound)
                        {
                            Console.WriteLine($"PASS: \"{text}\" with {encoding}");
                            passed++;
                        }
                        else
                        {
                            Console.WriteLine($"FAIL: \"{text}\" with {encoding}");
                            failed++;
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Test completed. Passed: {passed}, Failed: {failed}");
    }
}