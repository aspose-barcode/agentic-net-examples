// Title: Generate Mailmark 4‑state barcode and save as PNG
// Description: Demonstrates prompting (or using command‑line) for Mailmark fields, creating a Mailmark barcode, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of Aspose.BarCode.ComplexBarcode.MailmarkCodetext and ComplexBarcodeGenerator classes to encode postal data. Developers creating shipping labels, postal automation, or logistics solutions often need to generate Mailmark barcodes for UK postal services.
// Prompt: Create a console application that prompts users for Mailmark fields and saves the resulting barcode as PNG.
// Tags: mailmark, barcode, generation, png, console, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Console application that creates a Mailmark 4‑state barcode from user‑provided data
/// and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses command‑line arguments (or uses defaults), builds a MailmarkCodetext,
    /// generates the barcode, and writes it to "mailmark.png" in the current directory.
    /// </summary>
    /// <param name="args">Optional arguments: format versionId class supplyChainId itemId destinationPostCodePlusDps</param>
    static void Main(string[] args)
    {
        // Default sample values for Mailmark 4‑state barcode
        int format = 4;                     // 4 = unspecified/default
        int versionId = 1;
        string classValue = "0";            // "0" – Null or Test
        int supplyChainId = 384224;
        int itemId = 16563762;
        string destinationPostCodePlusDps = "EF61AH8T ";

        // If command‑line arguments are provided, try to parse them.
        // Expected order: format versionId class supplyChainId itemId destinationPostCodePlusDps
        if (args.Length >= 6)
        {
            int.TryParse(args[0], out format);
            int.TryParse(args[1], out versionId);
            classValue = args[2];
            int.TryParse(args[3], out supplyChainId);
            int.TryParse(args[4], out itemId);
            destinationPostCodePlusDps = args[5];
        }

        // Validate that the destination postcode plus DPS is not empty.
        if (string.IsNullOrWhiteSpace(destinationPostCodePlusDps))
        {
            Console.WriteLine("Invalid DestinationPostCodePlusDPS. Using default value.");
            destinationPostCodePlusDps = "EF61AH8T ";
        }

        // Create and populate the MailmarkCodetext object.
        var mailmark = new MailmarkCodetext
        {
            Format = format,
            VersionID = versionId,
            Class = classValue,
            SupplychainID = supplyChainId,
            ItemID = itemId,
            DestinationPostCodePlusDPS = destinationPostCodePlusDps
        };

        // Generate the barcode and save it as PNG.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "mailmark.png");
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Save directly to file in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Mailmark barcode saved to: {outputPath}");
    }
}