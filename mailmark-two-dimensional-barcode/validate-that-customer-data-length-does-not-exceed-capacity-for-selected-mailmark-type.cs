using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace MailmarkValidationDemo
{
    class Program
    {
        static void Main()
        {
            // Sample data for a 2D Mailmark barcode
            string customerContent = "ABC123";
            Mailmark2DType mailmarkType = Mailmark2DType.Type_7; // Change to Type_9 or Type_29 as needed

            try
            {
                ValidateCustomerContent(customerContent, mailmarkType);
                Console.WriteLine("Customer content is valid for the selected Mailmark type.");

                // Build Mailmark2D codetext
                var mailmark2D = new Mailmark2DCodetext
                {
                    InformationTypeID = "0", // single-character string as required
                    VersionID = "1",         // single-character string as required
                    Class = "1",             // single-character string as required
                    RTSFlag = "0",           // single-character string as required
                    SupplyChainID = 1234567,
                    ItemID = 12345678,
                    DestinationPostCodeAndDPS = "EF61AH8T ",
                    DataMatrixType = mailmarkType,
                    CustomerContent = customerContent
                };

                // Generate the complex barcode
                using (var generator = new ComplexBarcodeGenerator(mailmark2D))
                {
                    // Ensure a positive non‑zero bar height
                    generator.Parameters.Barcode.BarHeight.Point = 0.1f;

                    // Generate image into a memory stream
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        // Save to a file for demonstration
                        File.WriteAllBytes("Mailmark2D.png", ms.ToArray());
                        Console.WriteLine("Barcode image saved as Mailmark2D.png");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that the customer content length does not exceed the capacity of the specified Mailmark2D type.
        /// </summary>
        /// <param name="content">The customer content string.</param>
        /// <param name="type">The Mailmark2D type.</param>
        static void ValidateCustomerContent(string content, Mailmark2DType type)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            int maxLength = type switch
            {
                Mailmark2DType.Type_7 => 6,
                Mailmark2DType.Type_9 => 45,
                Mailmark2DType.Type_29 => 25,
                _ => throw new ArgumentOutOfRangeException(nameof(type), "Unsupported Mailmark2D type.")
            };

            if (content.Length > maxLength)
                throw new ArgumentException($"Customer content length ({content.Length}) exceeds maximum allowed ({maxLength}) for Mailmark type {type}.");
        }
    }
}