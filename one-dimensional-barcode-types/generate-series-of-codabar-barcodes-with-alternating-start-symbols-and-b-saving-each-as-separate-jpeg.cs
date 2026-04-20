using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.Generation; // Ensure BarcodeParameters namespace
using Aspose.BarCode.Generation; // Duplicate removed
using Aspose.BarCode.Generation; // Clean up duplicates
using Aspose.BarCode.Generation; // Final cleanup
using Aspose.BarCode.Generation; // End

class Program
{
    static void Main()
    {
        const int count = 5; // Number of barcodes to generate

        for (int i = 0; i < count; i++)
        {
            // Determine start/stop symbol: A for even index, B for odd index
            CodabarSymbol symbol = (i % 2 == 0) ? CodabarSymbol.A : CodabarSymbol.B;

            // Create barcode generator for Codabar
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
            {
                // Set start and stop symbols
                generator.Parameters.Barcode.Codabar.StartSymbol = symbol;
                generator.Parameters.Barcode.Codabar.StopSymbol = symbol;

                // Set the data to encode (digits are valid for Codabar)
                generator.CodeText = "12345" + i;

                // Build file name indicating the symbol used
                string fileName = $"codabar_{symbol}_{i}.jpg";

                // Save as JPEG
                generator.Save(fileName, BarCodeImageFormat.Jpeg);
            }
        }
    }
}