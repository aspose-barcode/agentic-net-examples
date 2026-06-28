using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and verification of an Australia Post barcode (RM4SCC 4‑state) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it as an image, then reads the image back to verify the content.
    /// </summary>
    static void Main()
    {
        // Sample Australia Post barcode text (RM4SCC 4‑state)
        string codeText = "5912345678AB";

        // Output image file path
        string outputPath = "AustraliaPost.png";

        // -------------------------------------------------
        // Generate the barcode and save it as a PNG image
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set the encoding table to CTable for interpreting customer information
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the generated barcode image (PNG format) to the specified path
            generator.Save(outputPath);
        }

        // -------------------------------------------------
        // Verify the generated barcode by reading it back
        // -------------------------------------------------
        using (var image = (Bitmap)Aspose.Drawing.Image.FromFile(outputPath))
        using (var reader = new BarCodeReader(image, DecodeType.AustraliaPost))
        {
            // Configure the reader to match the generator's settings
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
            reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

            // Iterate through all detected barcodes and display their type and text
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeType);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
            }
        }

        // Indicate that the process has completed
        Console.WriteLine("Barcode generation and verification completed.");
    }
}