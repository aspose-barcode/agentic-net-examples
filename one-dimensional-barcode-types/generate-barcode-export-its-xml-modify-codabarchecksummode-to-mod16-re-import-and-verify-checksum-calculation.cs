using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Paths for temporary files
        string imagePath = "codabar.png";
        string modifiedImagePath = "codabar_modified.png";
        string xmlPath = "codabar.xml";

        // -------------------------------------------------
        // 1. Create a Codabar barcode generator
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            // Enable checksum generation
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
            // Set checksum mode to Mod16 (default, but set explicitly)
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Save barcode image
            generator.Save(imagePath);

            // Export generator settings to XML
            generator.ExportToXml(xmlPath);
        }

        // -------------------------------------------------
        // 2. Import settings from XML and modify checksum mode
        // -------------------------------------------------
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Ensure checksum generation is enabled
            importedGenerator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
            // Modify checksum mode to Mod16
            importedGenerator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Save the barcode generated from imported settings
            importedGenerator.Save(modifiedImagePath);
        }

        // -------------------------------------------------
        // 3. Read the barcode and display checksum information
        // -------------------------------------------------
        if (!File.Exists(modifiedImagePath))
        {
            Console.WriteLine("Modified barcode image not found.");
            return;
        }

        using (var reader = new BarCodeReader(modifiedImagePath, DecodeType.Codabar))
        {
            // For Codabar checksum validation is not required, but we can still read extended data
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Detected Code Type: " + result.CodeTypeName);
                Console.WriteLine("Code Text: " + result.CodeText);

                // Attempt to retrieve checksum from extended parameters (if available)
                if (result.Extended != null && result.Extended.OneD != null)
                {
                    Console.WriteLine("Checksum: " + result.Extended.OneD.CheckSum);
                }
                else
                {
                    Console.WriteLine("Checksum information not available for this symbology.");
                }
            }
        }

        // Clean up temporary files (optional)
        // File.Delete(imagePath);
        // File.Delete(modifiedImagePath);
        // File.Delete(xmlPath);
    }
}