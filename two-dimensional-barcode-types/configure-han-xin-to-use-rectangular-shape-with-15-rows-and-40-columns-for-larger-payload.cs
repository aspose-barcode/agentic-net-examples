using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Han Xin barcode using Aspose.BarCode.
/// The barcode is automatically sized to a square version suitable for the payload.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Han Xin barcode and saves it as an image file.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        string codeText = "This is a sample payload that requires a larger Han Xin barcode.";
        // Define the output file path for the generated barcode image.
        string outputPath = "hanxin.png";

        // Create a BarcodeGenerator for Han Xin type with the specified payload.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Han Xin supports only square symbols; let the generator select the optimal square version.
            generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Han Xin barcode saved to '{outputPath}'.");
        // Reminder about the limitation of rectangular shapes for Han Xin.
        Console.WriteLine("Note: Rectangular shape with specific rows and columns is not supported for Han Xin.");
    }
}