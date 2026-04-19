using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeDemo
{
    // Interface for barcode generation – enables dependency injection
    public interface IBarcodeGenerator
    {
        void Generate(string text, string outputPath);
    }

    // Real implementation using Aspose.BarCode
    public class AsposeBarcodeGenerator : IBarcodeGenerator, IDisposable
    {
        private readonly BarcodeGenerator _generator;

        public AsposeBarcodeGenerator()
        {
            // Initialize generator for QR Code symbology
            _generator = new BarcodeGenerator(EncodeTypes.QR);
            // Set high error correction level
            _generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
        }

        public void Generate(string text, string outputPath)
        {
            _generator.CodeText = text;
            // Save directly using Aspose API
            _generator.Save(outputPath);
        }

        public void Dispose()
        {
            _generator?.Dispose();
        }
    }

    // Mock implementation for testing – does not create an actual image
    public class MockBarcodeGenerator : IBarcodeGenerator
    {
        public void Generate(string text, string outputPath)
        {
            Console.WriteLine($"[Mock] Would generate barcode for '{text}' and save to '{outputPath}'.");
        }
    }

    // Service that depends on IBarcodeGenerator
    public class BarcodeService
    {
        private readonly IBarcodeGenerator _barcodeGenerator;

        public BarcodeService(IBarcodeGenerator barcodeGenerator)
        {
            _barcodeGenerator = barcodeGenerator ?? throw new ArgumentNullException(nameof(barcodeGenerator));
        }

        public void CreateQrCode(string url, string filePath)
        {
            _barcodeGenerator.Generate(url, filePath);
        }
    }

    class Program
    {
        static void Main()
        {
            const string sampleUrl = "https://example.com";
            const string outputFile = "qr.png";

            // Use real generator; replace with MockBarcodeGenerator for unit tests
            using (var realGenerator = new AsposeBarcodeGenerator())
            {
                var service = new BarcodeService(realGenerator);
                service.CreateQrCode(sampleUrl, outputFile);
                Console.WriteLine($"QR code saved to '{outputFile}'.");
            }
        }
    }
}