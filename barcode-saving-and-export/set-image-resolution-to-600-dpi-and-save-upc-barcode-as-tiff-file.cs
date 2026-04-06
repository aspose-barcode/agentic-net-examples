using System;
using Aspose.BarCode.Generation;
class Program
{
    static void Main()
    {
        // Create a UPC-A barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA))
        {
            // Set the data to encode (12 digits for UPC-A)
            generator.CodeText = "012345678905";

            // Set image resolution to 600 DPI
            generator.Parameters.Resolution = 600f;

            // Save the barcode as a TIFF file
            generator.Save("upc_a.tiff");
        }
    }
}