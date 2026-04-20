using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample order numbers
        List<int> orderNumbers = new List<int> { 1001, 1002, 1003, 1004, 1005 };

        foreach (int order in orderNumbers)
        {
            // Configure Mailmark codetext
            MailmarkCodetext mailmark = new MailmarkCodetext
            {
                Format = 4,                     // 4‑state Mailmark
                VersionID = 1,                  // Version ID
                Class = "0",                    // Null/Test class
                SupplychainID = 384224,         // Example supply chain ID
                ItemID = order,                 // Use order number as ItemID
                DestinationPostCodePlusDPS = "EF61AH8T " // Fixed valid postcode + DPS
            };

            // Generate the barcode image
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Ensure a visible bar height
                generator.Parameters.Barcode.BarHeight.Point = 10f;

                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Convert image to Base64 string
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        string base64 = Convert.ToBase64String(ms.ToArray());
                        Console.WriteLine($"Order {order}: {base64}");
                    }
                }
            }
        }
    }
}