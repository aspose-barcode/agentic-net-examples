using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Han Xin barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Han Xin barcode from a sample text and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the alphanumeric text that will be encoded into the barcode.
        string codeText = "ABC123XYZ";

        // Initialize the barcode generator with Han Xin symbology and the provided text.
        // The generator implements IDisposable, so we use a using block to ensure resources are released.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // No additional configuration is required for the default square module size.
            // Han Xin automatically selects the appropriate version based on the payload length.

            // Specify the output file path for the generated barcode image.
            string outputPath = "hanxin.png";

            // Save the barcode image to the specified path in PNG format.
            generator.Save(outputPath);

            // Inform the user that the barcode has been saved successfully.
            Console.WriteLine($"Han Xin barcode saved to: {outputPath}");
        }
    }
}