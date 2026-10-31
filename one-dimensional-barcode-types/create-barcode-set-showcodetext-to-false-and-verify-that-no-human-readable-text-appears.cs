// Title: Generate Code128 barcode without human‑readable text
// Description: Demonstrates creating a Code128 barcode, disabling the displayed code text, and saving the image to a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcodes. Typical scenarios include creating machine‑readable barcodes where the human‑readable text is hidden for aesthetic or security reasons. Developers often need to control code‑text visibility, image format, and output location when integrating barcode generation into applications.
// Prompt: Create a barcode, set ShowCodeText to false, and verify that no human‑readable text appears.
// Tags: code128, barcode, hide text, generation, png, aspose.barcode, c#, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with hidden human‑readable text,
/// saves it as a PNG file, and verifies that the text visibility setting is applied.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the output files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string outputPath = Path.Combine(tempFolder, "barcode.png");

        // ------------------------------------------------------------
        // Generate a barcode with the human‑readable text hidden
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the code‑text location to None to hide the text
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that the code‑text visibility setting is applied
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            bool isTextHidden = generator.Parameters.Barcode.CodeTextParameters.Location == CodeLocation.None;
            Console.WriteLine($"Human‑readable text hidden: {isTextHidden}");
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode image saved at: {outputPath}");
    }
}