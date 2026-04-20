using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Use Code39 which encodes uppercase letters A‑Z.
        const string symbology = "Code39";
        bool allPassed = true;

        for (char ch = 'A'; ch <= 'Z'; ch++)
        {
            string text = ch.ToString();

            // Generate barcode image in memory.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, text))
            {
                // Optional: set size parameters.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.BarHeight.Point = 50f;
                generator.Parameters.Barcode.XDimension.Point = 1f;

                using (var ms = new MemoryStream())
                {
                    // Save to stream as PNG.
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    // Recognize barcode from the generated image.
                    using (var reader = new BarCodeReader(ms, DecodeType.Code39))
                    {
                        // Disable checksum validation for simplicity.
                        reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                        var results = reader.ReadBarCodes();
                        if (results.Length == 0)
                        {
                            Console.WriteLine($"[FAIL] No barcode detected for '{text}'.");
                            allPassed = false;
                            continue;
                        }

                        var result = results[0];
                        if (result.CodeText == text)
                        {
                            Console.WriteLine($"[PASS] '{text}' encoded and decoded correctly.");
                        }
                        else
                        {
                            Console.WriteLine($"[FAIL] Expected '{text}', got '{result.CodeText}'.");
                            allPassed = false;
                        }
                    }
                }
            }
        }

        Console.WriteLine(allPassed ? "All uppercase letters validated successfully."
                                    : "Some validations failed.");
    }
}