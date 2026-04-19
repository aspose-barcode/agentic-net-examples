using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeBatchExample
{
    class Program
    {
        static void Main()
        {
            // Number of barcodes to generate
            const int count = 5;

            for (int i = 0; i < count; i++)
            {
                // Alternate background colors: White for even, LightGray for odd indices
                Color background = (i % 2 == 0) ? Color.White : Color.LightGray;

                // Create a new barcode generator for Code128 symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
                {
                    // Set constant bar (foreground) color
                    generator.Parameters.Barcode.BarColor = Color.Black;

                    // Apply the alternating background color
                    generator.Parameters.BackColor = background;

                    // Set the code text for this barcode
                    generator.CodeText = $"CODE{i + 1}";

                    // Save the barcode image to a file
                    string fileName = $"barcode_{i + 1}.png";
                    generator.Save(fileName);
                }
            }
        }
    }
}