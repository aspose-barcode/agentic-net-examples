using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a OneCode 2‑state barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a OneCode barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // 20‑digit numeric string required for OneCode 2‑state barcode
        string codeText = "12345678901234567890";

        // Specify the barcode type as OneCode
        BaseEncodeType encodeType = EncodeTypes.OneCode;

        // Create a generator instance with the specified type and data
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Define the output file name
            string outputPath = "onecode.png";

            // Save the generated barcode image to the file system
            generator.Save(outputPath);
        }
    }
}