using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation of a DataBar Stacked barcode and saving it as a PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a DataBar Stacked barcode with specific settings.
    /// </summary>
    static void Main()
    {
        // Define a sample GTIN code wrapped in Application Identifier (01)
        string codeText = "(01)12345678901231";

        // Choose the DataBar Stacked symbology for encoding
        BaseEncodeType encodeType = EncodeTypes.DatabarStacked;

        // Create a barcode generator with the selected symbology and data
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set the aspect ratio of the DataBar stacked barcode (12:1)
            generator.Parameters.Barcode.DataBar.AspectRatio = 12f;

            // Enable the 2‑D composite component for the DataBar barcode
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = true;

            // Save the generated barcode to a PDF file
            generator.Save("DataBarStacked.pdf");
        }

        // Inform the user that the PDF has been created
        Console.WriteLine("Barcode PDF generated successfully.");
    }
}