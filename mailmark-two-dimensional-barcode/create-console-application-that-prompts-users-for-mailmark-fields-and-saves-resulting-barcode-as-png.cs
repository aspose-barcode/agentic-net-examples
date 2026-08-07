// Title: Generate Mailmark Barcode and Save as PNG
// Description: Creates a Mailmark barcode using provided or default values and saves it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It demonstrates how to use the MailmarkCodetext class with ComplexBarcodeGenerator to produce a Mailmark symbology barcode, a common requirement for postal and logistics applications. Developers often need to customize fields such as format, version, class, and supply‑chain identifiers, then export the result to an image format like PNG.
// Prompt: Create a console application that prompts users for Mailmark fields and saves the resulting barcode as PNG.
// Tags: mailmark, barcode, generation, png, aspose.barcode, complexbarcode, console

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Mailmark barcode and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses optional command‑line arguments, builds the Mailmark codetext,
    /// generates the barcode and writes it to disk.
    /// </summary>
    /// <param name="args">
    /// Command‑line arguments in the following order:
    /// format versionId class supplyChainId itemId destinationPostCodePlusDps
    /// </param>
    static void Main(string[] args)
    {
        // Default Mailmark values (valid sample)
        int format = 4;               // 4‑state Mailmark
        int versionId = 1;
        string classValue = "0";
        int supplyChainId = 384224;
        int itemId = 16563762;
        string destinationPostCodePlusDps = "EF61AH8T ";

        // Parse command‑line arguments if provided
        // Expected order: format versionId class supplyChainId itemId destinationPostCodePlusDps
        try
        {
            if (args.Length >= 6)
            {
                format = int.Parse(args[0]);
                versionId = int.Parse(args[1]);
                classValue = args[2];
                supplyChainId = int.Parse(args[3]);
                itemId = int.Parse(args[4]);
                destinationPostCodePlusDps = args[5];

                // Ensure the required trailing space is present
                if (!destinationPostCodePlusDps.EndsWith(" "))
                    destinationPostCodePlusDps += " ";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Argument parsing error: {ex.Message}");
            Console.WriteLine("Using default Mailmark values.");
        }

        // Construct MailmarkCodetext with the collected values
        var mailmark = new MailmarkCodetext
        {
            Format = format,
            VersionID = versionId,
            Class = classValue,
            SupplychainID = supplyChainId,
            ItemID = itemId,
            DestinationPostCodePlusDPS = destinationPostCodePlusDps
        };

        // Generate the barcode and save it as a PNG file
        try
        {
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                string outputPath = "mailmark.png";
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Mailmark barcode saved to '{Path.GetFullPath(outputPath)}'.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to generate Mailmark barcode: {ex.Message}");
        }
    }
}