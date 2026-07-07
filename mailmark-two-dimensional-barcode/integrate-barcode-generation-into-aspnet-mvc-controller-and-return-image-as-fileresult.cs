// Title: Generate Code128 barcode and save as PNG
// Description: Demonstrates creating a Code128 barcode image using Aspose.BarCode and saving it to disk, which can be adapted to return as a FileResult in ASP.NET MVC.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to encode data, customize colors, and output images. Developers working on web applications often need to generate barcodes on-the-fly for reports, tickets, or inventory systems, and then return the image via an MVC controller action as a FileResult.
// Prompt: Integrate barcode generation into an ASP.NET MVC controller and return the image as a FileResult.
// Tags: code128, barcode generation, png, aspnet mvc, filereturn, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point for the console demonstration. In an MVC app this logic would reside in a controller action returning a FileResult.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a BarcodeGenerator for Code128 symbology with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Set the barcode's foreground (bar) color.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the background color of the image.
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode image to the specified file path.
            generator.Save(outputPath);
        }

        // Output the full path of the saved barcode image for verification.
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
    }
}