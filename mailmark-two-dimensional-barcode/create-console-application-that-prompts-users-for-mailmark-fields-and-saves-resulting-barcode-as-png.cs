// Title: Generate Mailmark 4‑State Barcode and Save as PNG
// Description: Demonstrates how to create a Mailmark 4‑state barcode using Aspose.BarCode, optionally accepting command‑line arguments, and saving the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of MailmarkCodetext and ComplexBarcodeGenerator classes to produce Mailmark symbologies, a common requirement for logistics and postal applications. Developers often need to generate such barcodes programmatically for batch processing, integration with shipping systems, or automated label creation.
// Prompt: Create a console application that prompts users for Mailmark fields and saves the resulting barcode as PNG.
// Tags: mailmark, barcode, generation, png, console, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Console application that generates a Mailmark 4‑state barcode and writes it to a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional command‑line arguments to override default Mailmark fields,
    /// creates the barcode, and saves it as a PNG image.
    /// </summary>
    /// <param name="args">
    /// Expected order (if provided): format, versionId, class, supplychainId, itemId, destinationPostCodePlusDps.
    /// </param>
    static void Main(string[] args)
    {
        // Default values for Mailmark 4‑state fields
        int format = 4;
        int versionId = 1;
        string classValue = "0";
        int supplychainId = 384224;
        int itemId = 16563762;
        string destinationPostCodePlusDps = "EF61AH8T ";

        // Parse command‑line arguments if supplied (order: format versionId class supplychainId itemId destinationPostCodePlusDps)
        if (args.Length >= 6)
        {
            if (!int.TryParse(args[0], out format))
                format = 4;
            if (!int.TryParse(args[1], out versionId))
                versionId = 1;
            classValue = args[2];
            if (!int.TryParse(args[3], out supplychainId))
                supplychainId = 384224;
            if (!int.TryParse(args[4], out itemId))
                itemId = 16563762;
            destinationPostCodePlusDps = args[5];
        }

        // Build a unique output file name and combine it with the current directory
        string outputFileName = $"Mailmark4State_{Guid.NewGuid():N}.png";
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), outputFileName);

        // Create a MailmarkCodetext instance populated with the field values
        var mailmarkCode = new MailmarkCodetext
        {
            Format = format,
            VersionID = versionId,
            Class = classValue,
            SupplychainID = supplychainId,
            ItemID = itemId,
            DestinationPostCodePlusDPS = destinationPostCodePlusDps
        };

        // Generate the barcode using ComplexBarcodeGenerator and save it as PNG
        using (var generator = new ComplexBarcodeGenerator(mailmarkCode))
        {
            // Set the X‑dimension (module width) to 4 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Mailmark 4‑state barcode saved to: {outputPath}");
    }
}