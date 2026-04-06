using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a DataMatrix barcode with Reed‑Solomon ECC (Ecc200)
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleText"))
        {
            generator.Parameters.Barcode.DataMatrix.DataMatrixEcc = DataMatrixEccType.Ecc200;
            generator.Save("datamatrix.png");
        }

        // Read the barcode using high quality settings to enable Reed‑Solomon error correction
        using (var reader = new BarCodeReader("datamatrix.png", DecodeType.DataMatrix))
        {
            // Configure QualitySettings for robust error correction
            reader.QualitySettings = QualitySettings.HighQuality;

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Code Type: " + result.CodeTypeName);
                Console.WriteLine("Code Text: " + result.CodeText);
                Console.WriteLine("Confidence: " + result.Confidence);
            }
        }
    }
}