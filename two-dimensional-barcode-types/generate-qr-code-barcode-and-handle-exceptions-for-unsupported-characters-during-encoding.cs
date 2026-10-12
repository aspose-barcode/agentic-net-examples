// Title: Generate QR Code with Binary Encode Mode and Exception Handling
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, setting binary encode mode which does not support Unicode characters, and handling the resulting exception.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure QR Code parameters such as EncodeMode and exception behavior. It shows typical use cases for developers who need to generate QR codes programmatically, customize encoding settings, and gracefully handle unsupported characters during generation. The key API classes used are BarcodeGenerator, EncodeTypes, QREncodeMode, and BarCodeImageFormat.
// Prompt: Generate a QR Code barcode and handle exceptions for unsupported characters during encoding.
// Tags: qr code,barcode generation,exception handling,binary encode mode,aspose.barcode,encode types,output png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that generates a QR Code barcode and demonstrates exception handling for unsupported characters.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR Code in binary mode, which does not support Unicode characters, and catches any exceptions thrown during generation.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output image.
        string outputDir = Path.Combine(Path.GetTempPath(), "QrDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "qr_binary.png");

        try
        {
            // Initialize the barcode generator for QR Code with Unicode text that will cause an error in binary mode.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "漢字"))
            {
                // Set encode mode to Binary, which does not support Unicode characters; this will trigger an exception.
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

                // Configure the generator to throw an exception when the code text is invalid.
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Save the generated QR Code image to the specified path in PNG format.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"QR Code generated successfully: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Output exception details to the console.
            Console.WriteLine("Exception during QR Code generation:");
            Console.WriteLine(ex.Message);
        }
    }
}