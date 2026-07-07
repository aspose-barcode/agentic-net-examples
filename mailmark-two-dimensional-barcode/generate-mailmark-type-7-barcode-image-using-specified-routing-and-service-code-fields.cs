// Title: Generate Mailmark Type 7 Barcode Image
// Description: Creates a Mailmark type 7 barcode with routing and service code fields and saves it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, demonstrating how to use MailmarkCodetext and ComplexBarcodeGenerator to produce Mailmark symbology. Developers commonly use these APIs to encode routing, service, and item information for postal automation and tracking systems.
// Prompt: Generate a Mailmark type 7 barcode image using specified routing and service code fields.
// Tags: mailmark, barcode, generation, png, aspose.barcode, complexbarcodegenerator

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creation of a Mailmark type 7 barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds a MailmarkCodetext, generates the barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize Mailmark codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            // Mailmark 4‑state format (type 7)
            Format = 4,
            // Version identifier (typically 1)
            VersionID = 1,
            // Class identifier as a string
            Class = "0",
            // Routing / supply chain identifier
            SupplychainID = 384224,
            // Unique item identifier
            ItemID = 16563762,
            // Destination postcode + DPS (trailing space required by spec)
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Optional visual settings: black bars on white background
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode image to a PNG file
            generator.Save("mailmark.png");
        }

        // Inform the user that the image has been saved
        Console.WriteLine("Mailmark barcode image saved as 'mailmark.png'.");
    }
}