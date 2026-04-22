using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Prepare common Mailmark2D data
        const string destinationPostCode = "EF61AH8T "; // 9 chars with trailing spaces
        const string versionId = "1";
        const string informationTypeId = "0";
        const string classId = "0";
        const string rtsFlag = "0";
        const int supplyChainId = 384224;
        const int itemId = 16563762;

        // Measure Type 7 (24x24 modules)
        var result7 = GenerateMailmark2D(Mailmark2DType.Type_7, destinationPostCode, versionId, informationTypeId, classId, rtsFlag, supplyChainId, itemId);
        Console.WriteLine($"Mailmark Type 7 - Generation Time: {result7.timeMs} ms, Image Size: {result7.sizeBytes} bytes");

        // Measure Type 29 (16x48 modules)
        var result29 = GenerateMailmark2D(Mailmark2DType.Type_29, destinationPostCode, versionId, informationTypeId, classId, rtsFlag, supplyChainId, itemId);
        Console.WriteLine($"Mailmark Type 29 - Generation Time: {result29.timeMs} ms, Image Size: {result29.sizeBytes} bytes");
    }

    private static (long timeMs, long sizeBytes) GenerateMailmark2D(
        Mailmark2DType matrixType,
        string destinationPostCode,
        string versionId,
        string informationTypeId,
        string classId,
        string rtsFlag,
        int supplyChainId,
        int itemId)
    {
        // Build Mailmark2D codetext
        var mailmark = new Mailmark2DCodetext
        {
            DestinationPostCodeAndDPS = destinationPostCode,
            VersionID = versionId,
            InformationTypeID = informationTypeId,
            Class = classId,
            RTSFlag = rtsFlag,
            SupplyChainID = supplyChainId,
            ItemID = itemId,
            DataMatrixType = matrixType
        };

        // Measure generation time
        var stopwatch = Stopwatch.StartNew();
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Generate image into memory stream
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                stopwatch.Stop();
                long size = ms.Length;
                // Optionally save to file for visual verification
                string fileName = $"Mailmark_{matrixType}.png";
                File.WriteAllBytes(fileName, ms.ToArray());
                return (stopwatch.ElapsedMilliseconds, size);
            }
        }
    }
}