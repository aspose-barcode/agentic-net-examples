using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeBatchExport
{
    class Program
    {
        static void Main()
        {
            // Prepare output folder for XML files
            string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "BarcodesXml");
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Define a set of barcode configurations
            var configs = new (BaseEncodeType type, string codeText, Action<BarcodeGenerator> configure)[]
            {
                // Code128 with default settings
                (EncodeTypes.Code128, "ABC123456", generator => { }),

                // QR code with higher error correction and custom size
                (EncodeTypes.QR, "https://example.com",
                    generator =>
                    {
                        generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                        generator.Parameters.ImageWidth.Point = 300f;
                        generator.Parameters.ImageHeight.Point = 300f;
                    }),

                // DataMatrix with specific version
                (EncodeTypes.DataMatrix, "DataMatrixSample",
                    generator =>
                    {
                        generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_12x12;
                        generator.Parameters.ImageWidth.Point = 200f;
                        generator.Parameters.ImageHeight.Point = 200f;
                    }),

                // AustraliaPost with CTable interpreting type
                (EncodeTypes.AustraliaPost, "5912345678ABCde",
                    generator =>
                    {
                        generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;
                    }),

                // GS1 Composite Bar with linear and 2D components
                (EncodeTypes.GS1CompositeBar, "(01)03212345678906|(21)A1B2C3D4E5F6G7H8",
                    generator =>
                    {
                        generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                        generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
                        generator.Parameters.Barcode.XDimension.Pixels = 3f;
                        generator.Parameters.Barcode.BarHeight.Pixels = 100f;
                    })
            };

            // Process each configuration and export to XML
            foreach (var (type, codeText, configure) in configs)
            {
                string fileName = $"{type}_{Guid.NewGuid():N}.xml";
                string xmlPath = Path.Combine(outputFolder, fileName);

                using (var generator = new BarcodeGenerator(type, codeText))
                {
                    // Apply custom configuration if any
                    configure?.Invoke(generator);

                    // Export the generator settings to XML
                    bool success = generator.ExportToXml(xmlPath);
                    if (!success)
                    {
                        Console.WriteLine($"Failed to export configuration for {type} to {xmlPath}");
                    }
                }
            }

            Console.WriteLine("Batch export completed.");
        }
    }
}