using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and reading of an Australia Post barcode using Aspose.BarCode with
/// CustomerInformationInterpretingType set to Other.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it to a file, verifies the file, and then reads the barcode back.
    /// </summary>
    static void Main()
    {
        // Define sample barcode text (valid length for "Other" interpreting type) and output image path.
        string codeText = "59123456780123012301230123";
        string imagePath = "AustraliaPost.png";

        // ---------- Barcode Generation ----------
        // Create a BarcodeGenerator for Australia Post format with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Configure the generator to interpret customer information as "Other".
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.Other;

            // Save the generated barcode image to the specified file.
            generator.Save(imagePath);
        }

        // ---------- Verify Image Creation ----------
        // Ensure the barcode image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ---------- Barcode Reading ----------
        // Initialize a BarCodeReader for the saved image, specifying Australia Post decode type.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
        {
            // Set the decoding interpreting type to match the generation setting.
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.Other;

            // Iterate through all detected barcodes and output their type and text.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeType);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
            }
        }
    }
}