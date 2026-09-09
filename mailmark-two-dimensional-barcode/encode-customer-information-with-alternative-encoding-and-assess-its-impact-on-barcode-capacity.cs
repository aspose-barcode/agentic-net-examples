// Title: DotCode barcode encoding modes comparison
// Description: Demonstrates encoding customer information using DotCode barcode with default auto mode and binary mode, and compares resulting image dimensions to illustrate capacity impact.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DotCode symbology. It showcases how to use the BarcodeGenerator class with different EncodeMode settings (Auto and Binary) to encode data. Developers often need to evaluate how encoding choices affect barcode size and data capacity, especially when optimizing for space or readability.
// Prompt: Encode customer information with an alternative encoding and assess its impact on barcode capacity.
// Tags: dotcode, encoding, png, barcodegenerator, dotcodeencodemode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates DotCode barcodes using different encoding modes
/// and compares the resulting image dimensions to evaluate capacity impact.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes, saves them, and prints image sizes.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output folder for the generated barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Sample customer information to encode into the barcode
        string customerInfo = "CustomerInfo123";

        // ---------- Auto encoding (default) ----------
        // Generate a DotCode barcode using the default Auto encoding mode
        string autoPath = Path.Combine(outputFolder, "DotCode_Auto.png");
        using (BarcodeGenerator autoGen = new BarcodeGenerator(EncodeTypes.DotCode, customerInfo))
        {
            // No additional settings required for Auto mode
            autoGen.Save(autoPath, BarCodeImageFormat.Png);
        }

        // ---------- Binary encoding ----------
        // Generate a DotCode barcode using Binary encoding mode for raw byte data
        string binaryPath = Path.Combine(outputFolder, "DotCode_Binary.png");
        using (BarcodeGenerator binaryGen = new BarcodeGenerator(EncodeTypes.DotCode))
        {
            // Set the encoding mode to Binary
            binaryGen.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Binary;

            // Convert the customer information string to a UTF-8 byte array
            byte[] dataBytes = Encoding.UTF8.GetBytes(customerInfo);

            // Assign the byte array as the barcode's code text
            binaryGen.SetCodeText(dataBytes);

            // Save the generated barcode image
            binaryGen.Save(binaryPath, BarCodeImageFormat.Png);
        }

        // ---------- Compare image dimensions ----------
        // Load both generated images to assess the impact of encoding mode on barcode size
        using (Bitmap autoBmp = new Bitmap(autoPath))
        using (Bitmap binaryBmp = new Bitmap(binaryPath))
        {
            Console.WriteLine("Auto encoding image size:  {0}x{1} pixels", autoBmp.Width, autoBmp.Height);
            Console.WriteLine("Binary encoding image size: {0}x{1} pixels", binaryBmp.Width, binaryBmp.Height);
        }

        // Optional cleanup: delete the temporary output folder and its contents
        // Directory.Delete(outputFolder, true);
    }
}