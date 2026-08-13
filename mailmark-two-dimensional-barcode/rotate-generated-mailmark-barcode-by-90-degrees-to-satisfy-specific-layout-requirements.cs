// Title: Rotate Mailmark barcode by 90 degrees
// Description: Demonstrates generating a Mailmark barcode using Aspose.BarCode and rotating the image 90 degrees for layout requirements.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext classes to create a barcode, adjust rendering parameters like rotation, and save the result as an image. Developers working with postal barcodes or custom layout constraints can refer to this pattern for similar implementations.
// Prompt: Rotate the generated Mailmark barcode by 90 degrees to satisfy specific layout requirements.
// Tags: mailmark, barcode, rotation, image, aspnet, aspose.barcode, complexbarcodegenerator, csharp

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating and rotating a Mailmark barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Mailmark barcode, rotates it 90 degrees, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare Mailmark codetext with valid sample data
        var mailmark = new MailmarkCodetext
        {
            // 4-state Mailmark format
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            // Trailing space is required for the DestinationPostCodePlusDPS field
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the Mailmark barcode and apply a 90‑degree rotation
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // RotationAngle is a root Parameters property
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode image to a PNG file
            generator.Save("MailmarkRotated.png");
        }

        Console.WriteLine("Mailmark barcode generated and rotated successfully.");
    }
}