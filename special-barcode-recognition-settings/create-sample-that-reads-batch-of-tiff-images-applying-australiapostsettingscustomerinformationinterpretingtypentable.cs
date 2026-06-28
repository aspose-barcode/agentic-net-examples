using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and recognition of Australia Post barcodes using NTable interpreting type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample TIFF files with Australia Post barcodes,
    /// then reads and decodes them applying the NTable interpreting type.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare an array of TIFF file names to be created in the current directory.
        // ------------------------------------------------------------
        string[] tiffFiles = new string[5];
        for (int i = 0; i < tiffFiles.Length; i++)
        {
            tiffFiles[i] = $"AustraliaPost_{i + 1}.tif";
        }

        // ------------------------------------------------------------
        // Generate sample Australia Post barcodes and save each as a TIFF file.
        // ------------------------------------------------------------
        for (int i = 0; i < tiffFiles.Length; i++)
        {
            // Create a simple numeric code text for the barcode.
            string codeText = $"5912345678{i}";

            // Initialize the barcode generator with Australia Post encoding.
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                // Set the interpreting type to NTable for generation.
                generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.NTable;

                // Save the generated barcode image as a TIFF file.
                generator.Save(tiffFiles[i], BarCodeImageFormat.Tiff);
            }
        }

        // ------------------------------------------------------------
        // Read each generated TIFF file and decode the barcode using NTable interpreting type.
        // ------------------------------------------------------------
        foreach (var filePath in tiffFiles)
        {
            // Verify that the file exists before attempting to read it.
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Initialize the barcode reader for Australia Post type.
            using (var reader = new BarCodeReader(filePath, DecodeType.AustraliaPost))
            {
                // Apply NTable interpreting type for recognition.
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;

                // Iterate through all detected barcodes in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"  Code Text: {result.CodeText}");
                }
            }
        }
    }
}