using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a Mailmark barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Parses optional command‑line arguments, validates input, creates a MailmarkCodetext,
    /// generates the barcode, and saves it to a PNG file.
    /// </summary>
    /// <param name="args">
    /// Optional arguments in the following order:
    /// format versionId class supplyChainId itemId destinationPostCodePlusDps outputPath
    /// </param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Default Mailmark values (valid sample)
        // --------------------------------------------------------------------
        int format = 4;                 // 4 – unspecified/default (4‑state)
        int versionId = 1;
        string mailClass = "0";         // string property
        int supplyChainId = 384224;
        int itemId = 16563762;
        string destinationPostCodePlusDps = "EF61AH8T "; // 9‑char string with trailing spaces
        string outputPath = "mailmark.png";

        // --------------------------------------------------------------------
        // Parse command‑line arguments if provided.
        // Expected order: format versionId class supplyChainId itemId destinationPostCodePlusDps outputPath
        // --------------------------------------------------------------------
        try
        {
            if (args.Length >= 7)
            {
                format = int.Parse(args[0]);
                versionId = int.Parse(args[1]);
                mailClass = args[2];
                supplyChainId = int.Parse(args[3]);
                itemId = int.Parse(args[4]);
                destinationPostCodePlusDps = args[5];
                outputPath = args[6];
            }
        }
        catch (Exception ex)
        {
            // Inform the user about parsing errors and fall back to defaults.
            Console.WriteLine($"Argument parsing error: {ex.Message}");
            Console.WriteLine("Using default values.");
        }

        // --------------------------------------------------------------------
        // Validate required string length (basic check)
        // --------------------------------------------------------------------
        if (destinationPostCodePlusDps.Length != 9)
        {
            Console.WriteLine("DestinationPostCodePlusDPS must be exactly 9 characters. Using default value.");
            destinationPostCodePlusDps = "EF61AH8T ";
        }

        // --------------------------------------------------------------------
        // Build Mailmark codetext object with the collected parameters
        // --------------------------------------------------------------------
        var mailmark = new MailmarkCodetext
        {
            Format = format,
            VersionID = versionId,
            Class = mailClass,
            SupplychainID = supplyChainId,
            ItemID = itemId,
            DestinationPostCodePlusDPS = destinationPostCodePlusDps
        };

        // --------------------------------------------------------------------
        // Generate and save the Mailmark barcode
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the full path of the saved file for user confirmation.
        Console.WriteLine($"Mailmark barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}