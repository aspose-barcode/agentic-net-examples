using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with UTF-8 ECI encoding,
/// saving it to a file, and then reading it back to verify the content.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a MaxiCode barcode containing Unicode text, saves it,
    /// and reads it back to display the decoded value.
    /// </summary>
    static void Main()
    {
        // Unicode text that requires UTF-8 encoding (Japanese greeting)
        string codeText = "こんにちは";

        // Determine the full path for the output image file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode.png");

        // Create a barcode generator for MaxiCode with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Configure the generator to use UTF-8 ECI encoding
            generator.Parameters.Barcode.MaxiCode.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode image to the file system
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode generated with ECIEncoding=UTF-8 at: {outputPath}");

        // Verify the generated barcode by reading it back, if the file exists
        if (File.Exists(outputPath))
        {
            // Initialize a barcode reader for MaxiCode format
            using (var reader = new BarCodeReader(outputPath, DecodeType.MaxiCode))
            {
                // Read all barcodes found in the image
                var results = reader.ReadBarCodes();

                // Output each decoded text to the console
                foreach (var result in results)
                {
                    Console.WriteLine("Decoded CodeText: " + result.CodeText);
                }
            }
        }
        else
        {
            // Notify the user that the image file was not created
            Console.WriteLine("Failed to generate the barcode image.");
        }
    }
}