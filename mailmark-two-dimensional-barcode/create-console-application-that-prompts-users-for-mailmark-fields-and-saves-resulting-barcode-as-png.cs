// Title: Generate Mailmark 4-State Barcode and Save as PNG
// Description: Demonstrates creating a Mailmark barcode with default or command-line values and saving it as a PNG image file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use the MailmarkCodetext class together with ComplexBarcodeGenerator to produce Mailmark 4-state barcodes, a common requirement for postal automation. Developers typically need to set fields such as format, version ID, class, supply-chain ID, item ID, and destination postcode before rendering the barcode to an image format like PNG.
// Prompt: Create a console application that prompts users for Mailmark fields and saves the resulting barcode as PNG.
// Tags: mailmark, barcode, generation, png, console, aspose.barcode, complexbarcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Console application that generates a Mailmark 4‑state barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional command‑line arguments to override default Mailmark fields,
    /// creates the barcode, and writes the image to disk.
    /// </summary>
    /// <param name="args">Command‑line arguments: format, versionID, class, supplychainID, itemID, destination.</param>
    static void Main(string[] args)
    {
        // Default values for Mailmark 4‑state fields
        int format = 4;
        int versionID = 1;
        string classValue = "0";
        int supplychainID = 384224;
        int itemID = 16563762;
        string destination = "EF61AH8T ";

        // Override defaults with command‑line arguments if provided
        if (args.Length >= 6)
        {
            int.TryParse(args[0], out format);
            int.TryParse(args[1], out versionID);
            classValue = args[2];
            int.TryParse(args[3], out supplychainID);
            int.TryParse(args[4], out itemID);
            destination = args[5];
        }

        // Create Mailmark codetext object with the specified field values
        var mailmark = new MailmarkCodetext
        {
            Format = format,
            VersionID = versionID,
            Class = classValue,
            SupplychainID = supplychainID,
            ItemID = itemID,
            DestinationPostCodePlusDPS = destination
        };

        // Output file name for the generated PNG image
        string outputPath = "Mailmark4State.png";

        // Generate the barcode using ComplexBarcodeGenerator and save it as PNG
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set the X‑dimension (module width) in pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Mailmark barcode saved to {outputPath}");
    }
}