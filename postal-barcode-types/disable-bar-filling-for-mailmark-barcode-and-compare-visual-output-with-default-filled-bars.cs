// Title: Disable Bar Filling for Mailmark Barcode and Compare Images
// Description: Demonstrates how to generate a Mailmark barcode with default filled bars and with bar filling disabled, then compares the resulting images.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext classes to create Mailmark symbology, a postal barcode used in the UK. Typical use cases include generating printable mail items and verifying visual differences when toggling barcode rendering options such as FilledBars. Developers working with postal barcodes often need to adjust visual properties for compliance or aesthetic reasons.
// Prompt: Disable bar filling for a Mailmark barcode and compare visual output with default filled bars.
// Tags: mailmark, barcode, filledbars, image, comparison, aspose.barcode, csharp

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates Mailmark barcodes with and without filled bars and compares the outputs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcode images, one with default filled bars and one with bars empty, then reports whether they differ.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output folder for the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Construct Mailmark codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        byte[] filledBytes;
        // Generate barcode with default filled bars and save to file
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                filledBytes = ms.ToArray();
                File.WriteAllBytes(Path.Combine(outputDir, "Mailmark_Filled.png"), filledBytes);
            }
        }

        byte[] emptyBytes;
        // Generate barcode with bar filling disabled and save to file
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Parameters.Barcode.FilledBars = false; // Disable bar filling
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                emptyBytes = ms.ToArray();
                File.WriteAllBytes(Path.Combine(outputDir, "Mailmark_Empty.png"), emptyBytes);
            }
        }

        // Compare the two images byte‑by‑byte
        bool areIdentical = filledBytes.SequenceEqual(emptyBytes);
        Console.WriteLine($"Bar filling disabled: images are {(areIdentical ? "identical" : "different")}.");
        Console.WriteLine($"Images saved to: {outputDir}");
    }
}