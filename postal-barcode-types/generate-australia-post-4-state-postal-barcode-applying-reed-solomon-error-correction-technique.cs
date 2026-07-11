// Title: Generate Australia Post 4‑state barcode with Reed‑Solomon error correction
// Description: Demonstrates creating an Australia Post 4‑state postal barcode using Aspose.BarCode and applying Reed‑Solomon error correction.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on the Australia Post 4‑state symbology. It showcases the use of BarcodeGenerator, BarCodeReader, and related parameter classes to encode, render, and decode barcodes, a common task for developers handling postal automation and logistics solutions.
// Prompt: Generate an Australia Post 4‑state postal barcode applying Reed‑Solomon error correction technique.
// Tags: australia post, barcode generation, reed-solomon, error correction, aspnet, aspnetcore, c#, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and verification of an Australia Post 4‑state barcode with Reed‑Solomon error correction using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, and reads it back to verify decoding.
    /// </summary>
    static void Main()
    {
        // Sample codetext for Australia Post 4‑state barcode.
        string codeText = "5912345678ABCde";

        // Output image file path.
        string outputPath = "AustraliaPost.png";

        // Create a barcode generator for the Australia Post 4‑state symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Use CTable interpreting type (allows alphanumeric characters).
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Optional visual settings: black bars on white background.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode image to the specified file.
            generator.Save(outputPath);
        }

        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");

        // Verify the barcode by reading it back if the file was created successfully.
        if (File.Exists(outputPath))
        {
            // Load the saved image into a bitmap.
            using (var image = new Aspose.Drawing.Bitmap(outputPath))
            // Initialize a barcode reader for the Australia Post symbology.
            using (var reader = new BarCodeReader(image, DecodeType.AustraliaPost))
            {
                // Set decoding interpreting type to match the generation settings.
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                // Reed‑Solomon error correction is applied automatically by the symbology.

                // Iterate through all detected barcodes (should be one in this case).
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded Type: {result.CodeType}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to create barcode image.");
        }
    }
}