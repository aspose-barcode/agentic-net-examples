using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a DotCode barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "SampleData"))
        {
            // Set rectangular layout by specifying an aspect ratio (height/width)
            generator.Parameters.Barcode.DotCode.AspectRatio = 1.5f;

            // Configure the number of columns to 20 for higher data capacity
            generator.Parameters.Barcode.DotCode.Columns = 20;

            // Save the generated barcode image
            generator.Save("dotcode.png");
        }
    }
}