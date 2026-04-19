using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare temporary file paths
        string tempDir = Path.GetTempPath();
        string qrPath = Path.Combine(tempDir, "sample_qr.png");
        string pdf417Path = Path.Combine(tempDir, "sample_pdf417.png");

        // Generate QR Code image
        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR"))
        {
            qrGenerator.Save(qrPath);
        }

        // Generate PDF417 image
        using (var pdfGenerator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417"))
        {
            pdfGenerator.Save(pdf417Path);
        }

        // Verify files exist before reading
        if (!File.Exists(qrPath) || !File.Exists(pdf417Path))
        {
            Console.WriteLine("Failed to create sample barcode images.");
            return;
        }

        // Read barcodes, limiting to QR and PDF417 symbologies for performance
        using (var reader = new BarCodeReader(qrPath, DecodeType.QR, DecodeType.Pdf417))
        {
            // Use high‑performance mode
            reader.QualitySettings = QualitySettings.HighPerformance;

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        using (var reader = new BarCodeReader(pdf417Path, DecodeType.QR, DecodeType.Pdf417))
        {
            reader.QualitySettings = QualitySettings.HighPerformance;

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(qrPath);
            File.Delete(pdf417Path);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}