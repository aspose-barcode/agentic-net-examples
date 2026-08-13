// Title: Generate Stacked DataBar Barcode with Custom Aspect Ratio
// Description: Demonstrates how to configure Aspose.BarCode to create a stacked DataBar barcode, set its aspect ratio to ten, and disable the 2D composite component.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataBar symbologies. It shows usage of the BarcodeGenerator class with EncodeTypes.DatabarStacked, adjusting DataBar parameters such as AspectRatio and Is2DCompositeComponent. Developers commonly use these settings to meet specific size requirements or to test barcode components without the 2D composite part.
// Prompt: Configure DataBar parameters to generate stacked barcodes with aspect ratio ten, disable 2D component for testing.
// Tags: databar, stacked, aspectratio, disable-2d-component, barcode-generation, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a stacked DataBar barcode with a custom aspect ratio and disabled 2D component using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures parameters, saves the image, and writes a confirmation message.
    /// </summary>
    static void Main()
    {
        // Define a sample GTIN code text for DataBar (required format)
        string codeText = "(01)01234567890123";

        // Initialize a DataBar stacked barcode generator with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, codeText))
        {
            // Set the DataBar aspect ratio to 10 (height/width)
            generator.Parameters.Barcode.DataBar.AspectRatio = 10f;

            // Disable the 2D composite component for testing purposes
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = false;

            // Save the generated barcode image to a PNG file
            generator.Save("databar_stacked.png");
        }

        // Inform the user that the barcode was generated successfully
        Console.WriteLine("DataBar stacked barcode generated successfully.");
    }
}