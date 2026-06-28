using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating QR codes with FNC1 symbols and reading them
/// with and without stripping the FNC characters using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates QR barcodes, saves them to disk, and reads them back
    /// demonstrating the effect of the StripFNC setting.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory for generated barcode images
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // Define descriptions for the two barcode variants and allocate array
        // to hold the file paths of the generated images
        // --------------------------------------------------------------------
        string[] descriptions = { "FNC1_FirstPosition", "FNC1_SecondPosition" };
        string[] filePaths = new string[descriptions.Length];

        // --------------------------------------------------------------------
        // Generate each QR barcode variant
        // --------------------------------------------------------------------
        for (int i = 0; i < descriptions.Length; i++)
        {
            // Build extended codetext using QrExtCodetextBuilder
            var builder = new QrExtCodetextBuilder();

            if (i == 0)
            {
                // Variant 1: FNC1 placed in the first position, followed by plain data
                builder.AddFNC1FirstPosition();
                builder.AddPlainCodetext("DATA123");
            }
            else
            {
                // Variant 2: FNC1 placed in the second position with value "12"
                builder.AddFNC1SecondPosition("12");
                builder.AddPlainCodetext("MOREDATA");
            }

            // Retrieve the full extended codetext string
            string extendedCode = builder.GetExtendedCodetext();

            // Define the output file path for the current barcode image
            string filePath = Path.Combine(outputDir, $"{descriptions[i]}.png");

            // Generate the QR barcode image using the extended codetext
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, extendedCode))
            {
                // Set QR encoding mode to Extended to support FNC1 symbols
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Extended;
                // Use low error correction level (Level L)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelL;
                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Store the generated file path for later reading
            filePaths[i] = filePath;
            Console.WriteLine($"Generated barcode: {filePath}");
        }

        // --------------------------------------------------------------------
        // Read each generated barcode twice:
        //   1. Without stripping FNC characters (StripFNC = false)
        //   2. With stripping FNC characters (StripFNC = true)
        // --------------------------------------------------------------------
        foreach (var path in filePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            Console.WriteLine($"\nReading barcode: {Path.GetFileName(path)}");

            // ------------------------------------------------------------
            // Read barcode without stripping FNC characters
            // ------------------------------------------------------------
            using (var reader = new BarCodeReader(path, DecodeType.QR))
            {
                reader.BarcodeSettings.StripFNC = false;
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"StripFNC = false => CodeText: {result.CodeText}");
                }
            }

            // ------------------------------------------------------------
            // Read barcode with stripping FNC characters
            // ------------------------------------------------------------
            using (var reader = new BarCodeReader(path, DecodeType.QR))
            {
                reader.BarcodeSettings.StripFNC = true;
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"StripFNC = true  => CodeText: {result.CodeText}");
                }
            }
        }
    }
}