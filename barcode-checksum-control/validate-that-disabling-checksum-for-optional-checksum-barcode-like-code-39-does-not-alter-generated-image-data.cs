// Title: Code39 checksum disabling does not affect generated image
// Description: Demonstrates that disabling the optional checksum for a Code 39 barcode yields the same image as the default configuration.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes, configure checksum settings, and compare output images. Developers often need to verify that optional features like checksum toggling do not alter visual results, especially when integrating barcode creation into automated pipelines.
// Prompt: Validate that disabling checksum for an optional‑checksum barcode like Code 39 does not alter the generated image data.
// Tags: code39, checksum, barcode generation, png, aspose.barcode, image comparison

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code 39 barcode with and without checksum
/// and verifies that the resulting images are identical.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two barcode images and compares them.
    /// </summary>
    static void Main()
    {
        // Sample text to encode in the Code 39 barcode
        const string codeText = "ABC123";

        // Create a barcode generator with default settings (checksum enabled by default for Code 39)
        using (var generatorDefault = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Save the default barcode image to a memory stream
            using (var msDefault = new MemoryStream())
            {
                generatorDefault.Save(msDefault, BarCodeImageFormat.Png);
                byte[] imageDefault = msDefault.ToArray();

                // Create a second generator and explicitly disable the checksum
                using (var generatorNoChecksum = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
                {
                    generatorNoChecksum.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                    // Save the no‑checksum barcode image to another memory stream
                    using (var msNoChecksum = new MemoryStream())
                    {
                        generatorNoChecksum.Save(msNoChecksum, BarCodeImageFormat.Png);
                        byte[] imageNoChecksum = msNoChecksum.ToArray();

                        // Compare the two image byte arrays for equality
                        bool imagesIdentical = imageDefault.SequenceEqual(imageNoChecksum);
                        Console.WriteLine("Images identical: " + imagesIdentical);
                    }
                }
            }
        }
    }
}