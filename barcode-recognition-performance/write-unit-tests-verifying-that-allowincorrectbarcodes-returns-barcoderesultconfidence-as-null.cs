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
        // Generate an EAN13 barcode with an incorrect checksum (last digit is wrong)
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890123"))
        {
            using (var ms = new MemoryStream())
            {
                // Save barcode image to memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Read the barcode allowing incorrect barcodes
                using (var reader = new BarCodeReader(ms, DecodeType.EAN13))
                {
                    // Enable recognition of incorrect barcodes
                    reader.QualitySettings.AllowIncorrectBarcodes = true;

                    bool testPassed = false;

                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // According to the task, Confidence should be null.
                        // In Aspose.BarCode the Confidence enum returns BarCodeConfidence.None for incorrect barcodes.
                        // We treat this as the expected condition.
                        if (result.Confidence == BarCodeConfidence.None)
                        {
                            testPassed = true;
                        }
                    }

                    Console.WriteLine(testPassed ? "Test Passed: Confidence is None (treated as null)." : "Test Failed: Confidence is not None.");
                }
            }
        }
    }
}