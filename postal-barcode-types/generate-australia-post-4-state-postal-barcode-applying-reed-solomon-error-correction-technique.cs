// Title: Generate Australia Post 4‑State Postal Barcode with Reed‑Solomon Error Correction
// Description: Demonstrates creating an Australia Post 4‑state postal barcode, saving it as PNG, and decoding it using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing the use of BarcodeGenerator, BarCodeReader, and related settings for Australia Post symbology. Typical use cases include printing postal barcodes for mailing and verifying them programmatically. Developers often need to configure encoding tables, dimensions, and error‑correction features when working with postal barcodes.
// Prompt: Generate an Australia Post 4‑state postal barcode applying Reed‑Solomon error correction technique.
// Tags: australia post, barcode generation, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates, saves, and reads an Australia Post 4‑state postal barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, writes it to a PNG file, then reads it back to verify the content.
    /// </summary>
    static void Main()
    {
        // Define the barcode data according to Australia Post specifications:
        // FCC = 59 (supports customer info), DPID = 80123456, Customer info = "AB" (CTable, max 5 chars)
        string codeText = "5980123456AB";

        // Initialize the barcode generator for the Australia Post symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set the customer information interpreting type to CTable
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Optional visual customizations
            generator.Parameters.Barcode.XDimension.Point = 2f;               // Module size (pixel density)
            generator.Parameters.Barcode.Padding.Left.Point = 5f;            // Left margin
            generator.Parameters.Barcode.Padding.Top.Point = 5f;             // Top margin
            generator.Parameters.Barcode.Padding.Right.Point = 5f;           // Right margin
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;          // Bottom margin

            // Save the generated barcode image as a PNG file
            string outputPath = "AustraliaPost.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode image saved to: {outputPath}");

            // Generate an in‑memory bitmap for immediate recognition
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Create a reader configured for Australia Post decoding
                using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
                {
                    // Ensure the reader uses the same customer information interpreting type
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                    // Iterate through all detected barcodes (should be one in this case)
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Decoded Type : {result.CodeType}");
                        Console.WriteLine($"Decoded Text : {result.CodeText}");
                    }
                }
            }
        }

        // Note: Reed‑Solomon error correction is applied internally by the Australia Post symbology.
        // No additional API calls are required to enable it.
    }
}