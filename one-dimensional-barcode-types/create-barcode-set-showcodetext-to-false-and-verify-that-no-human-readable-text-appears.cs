// Title: Hide human‑readable text in a Code128 barcode
// Description: Demonstrates how to generate a Code128 barcode with Aspose.BarCode, disable the visible code text, and confirm the setting.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to control human‑readable text visibility. Developers often need to create barcodes without displaying the encoded value for aesthetic or security reasons; this snippet shows the typical API calls for that scenario.
// Prompt: Create a barcode, set ShowCodeText to false, and verify that no human‑readable text appears.
// Tags: barcode, code128, hide text, codetextparameters, aspose.barcode, generation, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing; // Required for Aspose.Drawing.Bitmap if needed

/// <summary>
/// Example program that generates a Code128 barcode with hidden human‑readable text.
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, hides the code text, saves the image, and verifies the setting.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    public static void Main(string[] args)
    {
        // Initialize a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the value to encode
            generator.CodeText = "123456";

            // Hide the human‑readable text by setting its location to None
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the generated barcode as a PNG file
            generator.Save("barcode.png");

            // Verify that the code text location is set to None (i.e., hidden)
            if (generator.Parameters.Barcode.CodeTextParameters.Location == CodeLocation.None)
            {
                Console.WriteLine("Human‑readable text is hidden.");
            }
            else
            {
                Console.WriteLine("Human‑readable text is visible.");
            }
        }
    }
}