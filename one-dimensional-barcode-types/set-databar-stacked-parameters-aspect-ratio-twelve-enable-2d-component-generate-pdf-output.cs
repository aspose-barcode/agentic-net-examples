using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for DataBar stacked with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, "01234567890123"))
        {
            // Set the aspect ratio of the stacked DataBar to 12
            generator.Parameters.Barcode.DataBar.AspectRatio = 12f;

            // Enable the 2D composite component for the DataBar
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = true;

            // Save the generated barcode as a PDF file
            generator.Save("databar.pdf");
        }
    }
}