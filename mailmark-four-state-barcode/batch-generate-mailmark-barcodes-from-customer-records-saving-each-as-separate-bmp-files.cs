using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

namespace MailmarkBatchGenerator
{
    class Program
    {
        static void Main()
        {
            // Output folder for generated BMP files
            string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "MailmarkBarcodes");
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Sample customer records (replace with real data as needed)
            var customers = new List<CustomerRecord>
            {
                new CustomerRecord { VersionID = 1, Class = "0", SupplychainID = 384224, ItemID = 16563762, DestinationPostCodePlusDPS = "EF61AH8T " },
                new CustomerRecord { VersionID = 1, Class = "1", SupplychainID = 384224, ItemID = 16563763, DestinationPostCodePlusDPS = "EF61AH8T " },
                new CustomerRecord { VersionID = 1, Class = "2", SupplychainID = 384224, ItemID = 16563764, DestinationPostCodePlusDPS = "EF61AH8T " },
                new CustomerRecord { VersionID = 1, Class = "3", SupplychainID = 384224, ItemID = 16563765, DestinationPostCodePlusDPS = "EF61AH8T " },
                new CustomerRecord { VersionID = 1, Class = "4", SupplychainID = 384224, ItemID = 16563766, DestinationPostCodePlusDPS = "EF61AH8T " }
            };

            int index = 1;
            foreach (var record in customers)
            {
                // Prepare Mailmark codetext
                var mailmark = new MailmarkCodetext();
                mailmark.Format = 4; // 4-state
                mailmark.VersionID = record.VersionID;
                mailmark.Class = record.Class;
                mailmark.SupplychainID = record.SupplychainID;
                mailmark.ItemID = record.ItemID;
                mailmark.DestinationPostCodePlusDPS = record.DestinationPostCodePlusDPS;

                // Generate barcode using ComplexBarcodeGenerator
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    // Set a positive non‑zero bar height (required for Mailmark)
                    generator.Parameters.Barcode.BarHeight.Point = 10f;

                    // Construct output file name
                    string fileName = Path.Combine(outputFolder, $"Mailmark_{index}.bmp");

                    // Save as BMP
                    generator.Save(fileName, BarCodeImageFormat.Bmp);
                }

                index++;
            }

            Console.WriteLine($"Generated {customers.Count} Mailmark BMP files in: {outputFolder}");
        }
    }

    // Simple data holder for customer information needed for Mailmark
    class CustomerRecord
    {
        public int VersionID { get; set; }
        public string Class { get; set; }
        public int SupplychainID { get; set; }
        public int ItemID { get; set; }
        public string DestinationPostCodePlusDPS { get; set; }
    }
}