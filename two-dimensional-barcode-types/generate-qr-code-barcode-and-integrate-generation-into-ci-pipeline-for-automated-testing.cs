using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static int Main(string[] args)
    {
        // Determine output file path (first argument or default)
        string outputPath = args.Length > 0 ? args[0] : "qr.png";

        // Ensure the directory for the output file exists
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            try
            {
                Directory.CreateDirectory(directory);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create directory '{directory}': {ex.Message}");
                return 1;
            }
        }

        // Create QR Code generator with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optional: set image resolution (96 DPI)
            generator.Parameters.Resolution = 96;

            // Save the barcode image
            try
            {
                generator.Save(outputPath);
            }
            catch (BarCodeException ex)
            {
                Console.Error.WriteLine($"Barcode generation failed: {ex.Message}");
                return 1;
            }
        }

        // Verify that the file was created
        if (File.Exists(outputPath))
        {
            Console.WriteLine($"QR Code generated successfully: {outputPath}");
            return 0;
        }
        else
        {
            Console.Error.WriteLine("QR Code generation completed but file not found.");
            return 1;
        }
    }
}