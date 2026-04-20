using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main(string[] args)
    {
        // Default Mailmark values (valid sample)
        int versionId = 1;
        string classValue = "0";
        int supplyChainId = 384224;
        int itemId = 16563762;
        string destinationPostCodePlusDps = "EF61AH8T ";

        // Try to parse command‑line arguments, otherwise keep defaults
        // Expected order: versionId class supplyChainId itemId destinationPostCodePlusDps
        try
        {
            if (args.Length >= 5)
            {
                versionId = int.Parse(args[0]);
                classValue = args[1];
                supplyChainId = int.Parse(args[2]);
                itemId = int.Parse(args[3]);
                destinationPostCodePlusDps = args[4];
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Argument parsing error: {ex.Message}");
            Console.WriteLine("Using default sample values.");
        }

        // Validate required fields (basic range checks)
        if (versionId < 0) throw new ArgumentOutOfRangeException(nameof(versionId));
        if (supplyChainId < 0) throw new ArgumentOutOfRangeException(nameof(supplyChainId));
        if (itemId < 0) throw new ArgumentOutOfRangeException(nameof(itemId));
        if (string.IsNullOrWhiteSpace(classValue)) throw new ArgumentException("Class cannot be empty.", nameof(classValue));
        if (string.IsNullOrWhiteSpace(destinationPostCodePlusDps)) throw new ArgumentException("DestinationPostCodePlusDps cannot be empty.", nameof(destinationPostCodePlusDps));

        // Build Mailmark codetext
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state Mailmark
            VersionID = versionId,
            Class = classValue,
            SupplychainID = supplyChainId,
            ItemID = itemId,
            DestinationPostCodePlusDPS = destinationPostCodePlusDps
        };

        // Generate the barcode
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // BarHeight must be positive non‑zero
            generator.Parameters.Barcode.BarHeight.Point = 10f;

            // Optional: set resolution (dpi) if desired
            generator.Parameters.Resolution = 300;

            // Output file path
            string outputPath = "mailmark.png";

            // Save as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Mailmark barcode saved to '{Path.GetFullPath(outputPath)}'.");
        }
    }
}