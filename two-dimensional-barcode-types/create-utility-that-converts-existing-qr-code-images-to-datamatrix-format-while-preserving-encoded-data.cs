using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading a QR code from an image file and generating a DataMatrix barcode
/// with the same encoded text using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments for input and output file paths.
    /// </summary>
    /// <param name="args">
    /// args[0] – path to the input QR code image (default: "qr_input.png").
    /// args[1] – path for the generated DataMatrix image (default: "datamatrix_output.png").
    /// </param>
    static void Main(string[] args)
    {
        // Default file paths for input QR code and output DataMatrix images
        string inputPath = "qr_input.png";
        string outputPath = "datamatrix_output.png";

        // Override defaults with command‑line arguments when provided
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            inputPath = args[0];
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            outputPath = args[1];

        // Ensure the input QR code image file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        string codeText = null; // Variable to hold the decoded QR code text

        // Read the QR code from the input image to extract its encoded text
        using (var reader = new BarCodeReader(inputPath, DecodeType.QR))
        {
            // Attempt to read all barcodes of type QR in the image
            var results = reader.ReadBarCodes();

            // If no QR code is found, inform the user and exit
            if (results.Length == 0)
            {
                Console.WriteLine("No QR code detected in the input image.");
                return;
            }

            // Use the first detected QR code's text for further processing
            codeText = results[0].CodeText;
            Console.WriteLine($"Decoded QR Code Text: {codeText}");
        }

        // Generate a DataMatrix barcode using the decoded text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Save the generated DataMatrix image to the specified output path
            generator.Save(outputPath);
        }

        // Inform the user that the DataMatrix barcode has been saved
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}