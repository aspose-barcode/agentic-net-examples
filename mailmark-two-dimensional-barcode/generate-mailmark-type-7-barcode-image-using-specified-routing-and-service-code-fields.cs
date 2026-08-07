// Title: Generate Mailmark Type 7 Barcode Image
// Description: Creates a Mailmark 2D (type 7) barcode with routing and service code fields and saves it as a PNG file.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation for Mailmark symbology. Shows how to configure Mailmark2DCodetext, set optional customer content, and render the barcode using ComplexBarcodeGenerator. Useful for developers needing to produce Mailmark barcodes for postal services, logistics, or tracking applications.
// Prompt: Generate a Mailmark type 7 barcode image using specified routing and service code fields.
// Tags: mailmark, type7, barcode generation, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Mailmark type 7 (2‑D) barcode and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the Mailmark2DCodetext, configures visual parameters, and writes the barcode to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a Mailmark 2D (type 7) codetext object
        var mailmark2d = new Mailmark2DCodetext();

        // ----- Required Mailmark fields -------------------------------------------------
        mailmark2d.VersionID = "1";                     // Version identifier (mandatory)
        mailmark2d.InformationTypeID = "7";             // Type 7 indicates a 2‑D Mailmark
        mailmark2d.Class = "0";                         // Class identifier (default)
        mailmark2d.DestinationPostCodeAndDPS = "EF61AH8T "; // Destination postcode + DPS (trailing space required by spec)
        mailmark2d.SupplyChainID = 384224;              // Supply‑chain identifier assigned by the postal service
        mailmark2d.ItemID = 16563762;                   // Unique item identifier within the supply chain

        // ----- Optional customer content (routing & service codes) -----------------------
        mailmark2d.CustomerContent = "R123S456";        // Example routing and service codes
        mailmark2d.CustomerContentEncodeMode = DataMatrixEncodeMode.C40; // Encode using C40 to reduce size

        // Output file path for the generated PNG image
        const string outputPath = "mailmark_type7.png";

        // Use ComplexBarcodeGenerator to render the barcode based on the configured codetext
        using (var generator = new ComplexBarcodeGenerator(mailmark2d))
        {
            // Optional visual customizations
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black; // Barcode foreground color
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;       // Background color

            // Save the rendered barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Mailmark type 7 barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}