// Title: Custom Decoding of Australia Post Barcodes Using CustomerInformationInterpretingType
// Description: Demonstrates how to generate an Australia Post barcode and decode it with a custom CustomerInformationInterpretingType setting.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, focusing on the Australia Post symbology. Developers often need to customize decoding behavior, such as interpreting customer information differently, and this snippet illustrates the required API calls.
// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to Other for custom decoding of Australia Post barcodes.
// Tags: australia post, barcode symbology, custom decoding, png output, barcodegenerator, barcodereader

using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates an Australia Post barcode, saves it as PNG, and then reads it back using a custom
/// CustomerInformationInterpretingType setting. This demonstrates how to control decoding behavior
/// for Australia Post barcodes with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saves the image, and reads it back
    /// with custom decoding settings.
    /// </summary>
    static void Main()
    {
        // Path for the generated barcode image
        const string imagePath = "australiapost.png";

        // -------------------- Generate Australia Post barcode --------------------
        // Create a generator for the Australia Post symbology with a sample postal code.
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "1100000000"))
        {
            // Set the interpreting type for the customer information to 'Other'.
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.Other;

            // Save the generated barcode as a PNG image.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // -------------------- Recognize the barcode with custom settings --------------------
        // Initialize a reader for the saved image, specifying the Australia Post decode type.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
        {
            // Apply the same interpreting type for decoding the customer information.
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.Other;

            // Iterate through all detected barcodes and output their type and text.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeType}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
            }
        }
    }
}