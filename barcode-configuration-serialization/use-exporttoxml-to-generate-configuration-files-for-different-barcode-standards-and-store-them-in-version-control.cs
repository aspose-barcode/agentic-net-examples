using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeConfigExport
{
    class Program
    {
        static void Main()
        {
            // Directory to store generated XML configuration files
            string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "BarcodesConfig");
            Directory.CreateDirectory(outputDir);

            // 1. Code128 barcode configuration
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123456"))
            {
                // Example: set Code128 encoding mode
                generator.Parameters.Barcode.Code128.Code128EncodeMode = Code128EncodeMode.Auto;
                string xmlPath = Path.Combine(outputDir, "Code128.xml");
                generator.ExportToXml(xmlPath);
            }

            // 2. QR code configuration with high error correction
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                // Optional: set QR version (auto by default)
                string xmlPath = Path.Combine(outputDir, "QR_HighErrorCorrection.xml");
                generator.ExportToXml(xmlPath);
            }

            // 3. DataMatrix configuration with specific version
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DataMatrixSample"))
            {
                // Set a specific DataMatrix version (e.g., 12x12)
                generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_12x12;
                string xmlPath = Path.Combine(outputDir, "DataMatrix_12x12.xml");
                generator.ExportToXml(xmlPath);
            }

            // 4. Australia Post barcode configuration with CTable interpreting type
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "5912345678ABCde"))
            {
                generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;
                string xmlPath = Path.Combine(outputDir, "AustraliaPost_CTable.xml");
                generator.ExportToXml(xmlPath);
            }

            // 5. GS1 Composite barcode configuration (linear + 2D components)
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, "(01)03212345678906|(21)A1B2C3D4E5F6G7H8"))
            {
                // Linear component type
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                // 2D component type
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
                // Example: adjust X-Dimension and BarHeight
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 100f;
                string xmlPath = Path.Combine(outputDir, "GS1CompositeBar.xml");
                generator.ExportToXml(xmlPath);
            }

            // Indicate completion
            Console.WriteLine("Barcode configuration XML files have been generated in: " + outputDir);
        }
    }
}