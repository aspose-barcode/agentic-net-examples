using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample GS1 DataBar Expanded Stacked code (Application Identifier 01 and 21)
        const string codeText = "(01)12345678901231(21)ABC123";

        // Create the barcode generator for DataBar Expanded Stacked symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, codeText))
        {
            // Set DataBar specific parameters: 3 columns and aspect ratio of 8
            generator.Parameters.Barcode.DataBar.Columns = 3;
            generator.Parameters.Barcode.DataBar.AspectRatio = 8f;

            // Save the barcode image as BMP
            generator.Save("DataBarExpandedStacked.bmp");
        }
    }
}