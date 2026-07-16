// Title: Generate DotCode barcode with rectangular layout and 20 columns
// Description: Demonstrates configuring a DotCode barcode to use a rectangular layout with 20 columns, increasing data capacity.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize DotCode symbology parameters such as layout and column count. It uses BarcodeGenerator, EncodeTypes, and DotCode settings to produce a barcode image. Developers often need to adjust DotCode dimensions for higher data payloads or specific scanning requirements.
// Prompt: Configure DotCode to use rectangular layout with 20 columns for increased data capacity.
// Tags: dotcode, barcode, generation, rectangular layout, columns, aspnet, aspnetcore, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a DotCode barcode using a rectangular layout with 20 columns.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates and saves a DotCode barcode image.
    /// </summary>
    static void Main()
    {
        // Define the data to be encoded in the DotCode barcode.
        string codeText = "Sample DotCode Data";

        // Initialize the BarcodeGenerator with DotCode symbology and the sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Set the rectangular layout by specifying 20 columns.
            // The number of rows will be calculated automatically based on DotCode constraints.
            generator.Parameters.Barcode.DotCode.Columns = 20;

            // Define the output file name for the generated barcode image.
            string outputFile = "dotcode.png";

            // Save the barcode image to the specified file.
            generator.Save(outputFile);

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"DotCode barcode saved to {outputFile}");
        }
    }
}