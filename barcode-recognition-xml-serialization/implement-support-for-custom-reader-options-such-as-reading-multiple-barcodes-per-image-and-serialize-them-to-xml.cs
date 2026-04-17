using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Paths for temporary files
        string code128Path = "code128.png";
        string qrPath = "qr.png";
        string multiPath = "multi.png";
        string xmlPath = "readerOptions.xml";

        // Generate first barcode (Code128)
        using (var generator1 = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            generator1.Save(code128Path);
        }

        // Generate second barcode (QR)
        using (var generator2 = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator2.Save(qrPath);
        }

        // Combine both images into one bitmap
        if (!File.Exists(code128Path) || !File.Exists(qrPath))
        {
            Console.WriteLine("Failed to generate barcode images.");
            return;
        }

        using (var bmp1 = new Bitmap(code128Path))
        using (var bmp2 = new Bitmap(qrPath))
        {
            int combinedWidth = bmp1.Width + bmp2.Width;
            int combinedHeight = Math.Max(bmp1.Height, bmp2.Height);
            using (var combined = new Bitmap(combinedWidth, combinedHeight))
            {
                using (var graphics = Graphics.FromImage(combined))
                {
                    graphics.DrawImage(bmp1, 0, 0, bmp1.Width, bmp1.Height);
                    graphics.DrawImage(bmp2, bmp1.Width, 0, bmp2.Width, bmp2.Height);
                }
                combined.Save(multiPath, ImageFormat.Png);
            }
        }

        // Verify combined image exists
        if (!File.Exists(multiPath))
        {
            Console.WriteLine("Failed to create combined image.");
            return;
        }

        // Create a reader, configure to read all supported types and high performance
        using (var reader = new BarCodeReader())
        {
            // Set to read all supported barcode types
            reader.BarCodeReadType = DecodeType.AllSupportedTypes;
            // Optional: set quality preset
            reader.QualitySettings = QualitySettings.HighPerformance;
            // Load the image
            reader.SetBarCodeImage(multiPath);
            // Export current reader options to XML
            reader.ExportToXml(xmlPath);
        }

        // Import reader options from XML and read barcodes
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("XML export failed.");
            return;
        }

        // Import creates a new BarCodeReader instance with the saved settings
        using (var importedReader = BarCodeReader.ImportFromXml(xmlPath))
        {
            if (importedReader == null)
            {
                Console.WriteLine("Failed to import reader settings from XML.");
                return;
            }

            // Assign the same image to the imported reader
            importedReader.SetBarCodeImage(multiPath);

            // Read all barcodes
            foreach (var result in importedReader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(code128Path);
            File.Delete(qrPath);
            File.Delete(multiPath);
            File.Delete(xmlPath);
        }
        catch
        {
            // Ignored
        }
    }
}