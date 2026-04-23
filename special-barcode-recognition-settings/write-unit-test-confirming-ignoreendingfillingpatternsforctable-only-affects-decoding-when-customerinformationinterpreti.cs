using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare barcode data
        const string codeText = "5912345678AB";

        // Generate AustraliaPost barcode with CTable interpreting type
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            using (var ms = new MemoryStream())
            {
                // Save barcode image to memory
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Decode with IgnoreEndingFillingPatternsForCTable = true
                string resultWithIgnore;
                using (var reader = new BarCodeReader(ms, DecodeType.AustraliaPost))
                {
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;
                    var result = reader.ReadBarCodes();
                    resultWithIgnore = result.Length > 0 ? result[0].CodeText : null;
                }

                // Reset stream for next read
                ms.Position = 0;

                // Decode with IgnoreEndingFillingPatternsForCTable = false
                string resultWithoutIgnore;
                using (var reader = new BarCodeReader(ms, DecodeType.AustraliaPost))
                {
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = false;
                    var result = reader.ReadBarCodes();
                    resultWithoutIgnore = result.Length > 0 ? result[0].CodeText : null;
                }

                // Reset stream for NTable test
                ms.Position = 0;

                // Decode with NTable (flag should have no effect)
                string resultNTableIgnore;
                using (var reader = new BarCodeReader(ms, DecodeType.AustraliaPost))
                {
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;
                    var result = reader.ReadBarCodes();
                    resultNTableIgnore = result.Length > 0 ? result[0].CodeText : null;
                }

                ms.Position = 0;

                string resultNTableNoIgnore;
                using (var reader = new BarCodeReader(ms, DecodeType.AustraliaPost))
                {
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = false;
                    var result = reader.ReadBarCodes();
                    resultNTableNoIgnore = result.Length > 0 ? result[0].CodeText : null;
                }

                // Validate expectations
                bool cTableEffect = resultWithIgnore != resultWithoutIgnore;
                bool nTableEffect = resultNTableIgnore != resultNTableNoIgnore;

                Console.WriteLine("CTable - With IgnoreEndingFillingPatternsForCTable: " + resultWithIgnore);
                Console.WriteLine("CTable - Without IgnoreEndingFillingPatternsForCTable: " + resultWithoutIgnore);
                Console.WriteLine("NTable - With IgnoreEndingFillingPatternsForCTable: " + resultNTableIgnore);
                Console.WriteLine("NTable - Without IgnoreEndingFillingPatternsForCTable: " + resultNTableNoIgnore);

                Console.WriteLine("Test Result: " + (cTableEffect && !nTableEffect ? "PASS" : "FAIL"));
            }
        }
    }
}