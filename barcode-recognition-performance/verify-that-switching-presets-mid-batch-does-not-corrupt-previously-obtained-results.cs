using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare temporary folder and file names
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempFolder);
        string file1 = Path.Combine(tempFolder, "code128.png");
        string file2 = Path.Combine(tempFolder, "qr.png");

        // Generate first barcode (Code128)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "First123"))
        {
            generator.Save(file1);
        }

        // Generate second barcode (QR)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Second456"))
        {
            generator.Save(file2);
        }

        // Reader instance that will switch quality presets mid‑batch
        using (var reader = new BarCodeReader())
        {
            // Detect all supported types
            reader.BarCodeReadType = DecodeType.AllSupportedTypes;

            // ---- First read with NormalQuality preset ----
            reader.QualitySettings = QualitySettings.NormalQuality;
            reader.SetBarCodeImage(file1);
            var firstResult = reader.ReadBarCodes().FirstOrDefault();
            string firstText = firstResult?.CodeText ?? "<none>";

            // ---- Switch to MaxQuality preset and read second image ----
            reader.QualitySettings = QualitySettings.MaxQuality;
            reader.SetBarCodeImage(file2);
            var secondResult = reader.ReadBarCodes().FirstOrDefault();
            string secondText = secondResult?.CodeText ?? "<none>";

            // ---- Re‑read first image after preset change to verify integrity ----
            reader.SetBarCodeImage(file1);
            var firstResultAgain = reader.ReadBarCodes().FirstOrDefault();
            string firstTextAgain = firstResultAgain?.CodeText ?? "<none>";

            // Output verification
            Console.WriteLine($"First read (NormalQuality):  {firstText}");
            Console.WriteLine($"Second read (MaxQuality):   {secondText}");
            Console.WriteLine($"First read after switch:    {firstTextAgain}");
            Console.WriteLine();

            if (firstText == firstTextAgain)
                Console.WriteLine("Verification passed: switching presets did not corrupt previous results.");
            else
                Console.WriteLine("Verification failed: previous result changed after preset switch.");
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(file1);
            File.Delete(file2);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup is best‑effort
        }
    }
}