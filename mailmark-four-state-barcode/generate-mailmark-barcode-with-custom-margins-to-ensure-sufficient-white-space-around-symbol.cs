using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;

namespace MailmarkExample
{
    class Program
    {
        static void Main()
        {
            // Create Mailmark codetext with required fields
            var mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            // Generate the Mailmark barcode
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Ensure a positive bar height (required for Mailmark)
                generator.Parameters.Barcode.BarHeight.Point = 5f;

                // Set custom padding (white space) around the symbol
                generator.Parameters.Barcode.Padding.Left.Point = 10f;
                generator.Parameters.Barcode.Padding.Top.Point = 10f;
                generator.Parameters.Barcode.Padding.Right.Point = 10f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

                // Save the barcode image to a file
                string outputPath = "mailmark.png";
                generator.Save(outputPath);
                Console.WriteLine($"Mailmark barcode saved to {Path.GetFullPath(outputPath)}");
            }
        }
    }
}