using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code with Aspose.BarCode, saving it to a file,
/// and then reading the generated QR code to verify its contents.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// Accepts optional command‑line arguments to specify the QR code text,
    /// error correction level, and output file path.
    /// </summary>
    /// <param name="args">
    /// args[0] - QR code text (default: "Hello Aspose QR")
    /// args[1] - Error correction level (L, M, Q, H; default: M)
    /// args[2] - Output file path (default: "qr.png")
    /// </param>
    static void Main(string[] args)
    {
        // Default configuration values
        string codeText = "Hello Aspose QR";
        QRErrorLevel errorLevel = QRErrorLevel.LevelM;
        string outputPath = "qr.png";

        // --------------------------------------------------------------------
        // Parse command‑line arguments (if any) to override defaults
        // --------------------------------------------------------------------
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            // First argument: QR code text
            codeText = args[0];
        }

        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
        {
            // Second argument: error correction level (L, M, Q, H)
            string level = args[1].Trim().ToUpperInvariant();
            if (level == "L")
                errorLevel = QRErrorLevel.LevelL;
            else if (level == "M")
                errorLevel = QRErrorLevel.LevelM;
            else if (level == "Q")
                errorLevel = QRErrorLevel.LevelQ;
            else if (level == "H")
                errorLevel = QRErrorLevel.LevelH;
            else
                Console.WriteLine($"Unrecognized error level '{args[1]}', using default LevelM.");
        }

        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
        {
            // Third argument: output file path
            outputPath = args[2];
        }

        // --------------------------------------------------------------------
        // Ensure the output directory exists before saving the image
        // --------------------------------------------------------------------
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // --------------------------------------------------------------------
        // Generate the QR code with the specified settings
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set QR specific parameters
            generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Parameters.Resolution = 300f; // DPI

            // Save the generated QR code image to the output path
            generator.Save(outputPath);
        }

        Console.WriteLine($"QR Code saved to: {outputPath}");

        // --------------------------------------------------------------------
        // Verify the generated QR code by reading it back
        // --------------------------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Generated file not found, cannot perform recognition.");
            return;
        }

        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            // Iterate through all recognized barcodes (should be one)
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Recognized CodeText: {result.CodeText}");
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                Console.WriteLine($"Confidence: {result.Confidence}");
            }
        }
    }
}