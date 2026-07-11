// Title: Generate Swiss Post Parcel barcode with service description metadata
// Description: Demonstrates creating a Swiss Post Parcel barcode, adding a service description as a caption, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel. Typical use cases include generating parcel barcodes for logistics, attaching additional service information, and customizing visual appearance. Developers often need to embed metadata such as service descriptions alongside barcodes for printing and scanning workflows.
// Prompt: Generate a Swiss Post Parcel additional service code barcode and attach the service description as metadata.
// Tags: barcode symbology, generation, png, aspose.barcode, swisspostparcel

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Swiss Post Parcel barcode,
/// attaches a service description as a caption, and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates and configures a BarcodeGenerator,
    /// adds a caption with the service description, and writes the barcode image to disk.
    /// </summary>
    static void Main()
    {
        // Define the service code (10‑digit) and its human‑readable description.
        string serviceCode = "1234567890";          // Example service code
        string serviceDescription = "Express Delivery";

        // Initialize the barcode generator for the Swiss Post Parcel symbology using the service code.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, serviceCode))
        {
            // Set visual appearance: black bars on a white background.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Configure the caption that appears above the barcode to show the service description.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = serviceDescription;
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 10f;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionAbove.TextColor = Color.DarkBlue;

            // Define the output file path and save the barcode as a PNG image.
            string outputPath = "SwissPostParcel.png";
            generator.Save(outputPath);

            // Inform the user where the file was saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}