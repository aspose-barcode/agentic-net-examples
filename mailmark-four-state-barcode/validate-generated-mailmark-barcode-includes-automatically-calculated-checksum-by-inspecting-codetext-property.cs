// Title: Validate Mailmark barcode checksum via Codetext property
// Description: Demonstrates generating a Mailmark barcode, retrieving its automatically calculated checksum from the constructed codetext, and performing a simple validation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and ComplexCodetextReader to create, inspect, and decode Mailmark symbols. Developers working with postal and logistics solutions often need to generate Mailmark barcodes, verify checksums, and extract embedded data, making this pattern a common requirement.
// Prompt: Validate generated Mailmark barcode includes automatically calculated checksum by inspecting Codetext property.
// Tags: mailmark, barcode, checksum, generation, recognition, complexbarcode, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Mailmark barcode, checks the automatically generated checksum,
/// and decodes the codetext back into its component fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, validates its checksum,
    /// and demonstrates decoding of the codetext.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "mailmark.png");

        // --------------------------------------------------------------
        // Build the Mailmark codetext with required fields (checksum is auto‑calculated).
        // --------------------------------------------------------------
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "1",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // --------------------------------------------------------------
        // Generate the barcode image using ComplexBarcodeGenerator.
        // --------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------
        // Retrieve the constructed codetext, which includes the automatically calculated checksum.
        // --------------------------------------------------------------
        string constructedCodetext = mailmark.GetConstructedCodetext();
        Console.WriteLine("Constructed Mailmark codetext: " + constructedCodetext);

        // --------------------------------------------------------------
        // Simple validation: verify that the last character of the codetext is a digit (the checksum).
        // --------------------------------------------------------------
        if (constructedCodetext.Length > 0 && char.IsDigit(constructedCodetext[constructedCodetext.Length - 1]))
        {
            Console.WriteLine("Checksum appears to be present (last character is a digit).");
        }
        else
        {
            Console.WriteLine("Checksum validation failed (last character is not a digit).");
        }

        // --------------------------------------------------------------
        // Decode the codetext back into a MailmarkCodetext object to demonstrate round‑trip parsing.
        // --------------------------------------------------------------
        var decoded = ComplexCodetextReader.TryDecodeMailmark(constructedCodetext);
        if (decoded != null)
        {
            Console.WriteLine("Decoded ItemID: " + decoded.ItemID);
        }
        else
        {
            Console.WriteLine("Failed to decode Mailmark codetext.");
        }
    }
}