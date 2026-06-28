using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and decoding of Mailmark and Mailmark2D barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    // Define capacity limits for different Mailmark types
    private const int Mailmark2DCustomerContentMaxLength = 30; // example limit

    /// <summary>
    /// Entry point of the application. Generates a barcode based on the selected Mailmark type,
    /// validates customer data, saves the barcode image, and optionally decodes it.
    /// </summary>
    static void Main()
    {
        // Sample inputs
        string selectedMailmarkType = "Mailmark2D"; // could be "Mailmark" or "Mailmark2D"
        string customerContent = "Sample customer data for Mailmark 2D barcode";

        try
        {
            // Validate the customer content against the selected Mailmark type's constraints
            ValidateCustomerData(customerContent, selectedMailmarkType);
            Console.WriteLine("Customer data validation passed.");

            // Build the appropriate codetext object based on the selected type
            if (selectedMailmarkType.Equals("Mailmark2D", StringComparison.OrdinalIgnoreCase))
            {
                // Populate Mailmark2D codetext with required and optional fields
                var mailmark2D = new Mailmark2DCodetext
                {
                    VersionID = "1",
                    InformationTypeID = "0",
                    Class = "1",
                    SupplyChainID = 384224,
                    ItemID = 16563762,
                    DestinationPostCodeAndDPS = "EF61AH8T ",
                    CustomerContent = customerContent,               // Set after validation
                    CustomerContentEncodeMode = DataMatrixEncodeMode.C40 // Optional encoding mode
                };

                // Generate the barcode image and save it as PNG
                using (var generator = new ComplexBarcodeGenerator(mailmark2D))
                {
                    string outputPath = "Mailmark2D.png";
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Mailmark2D barcode saved to {outputPath}");
                }

                // Demonstrate decoding the generated barcode (read back the image)
                using (var imageStream = new FileStream("Mailmark2D.png", FileMode.Open, FileAccess.Read))
                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Decode the complex codetext from the raw code text
                        Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                        if (decoded != null)
                        {
                            Console.WriteLine("Decoded Mailmark2D CustomerContent: " + decoded.CustomerContent);
                        }
                        else
                        {
                            Console.WriteLine("Failed to decode Mailmark2D codetext.");
                        }
                    }
                }
            }
            else if (selectedMailmarkType.Equals("Mailmark", StringComparison.OrdinalIgnoreCase))
            {
                // Populate Mailmark (4‑state) codetext with required fields
                var mailmark = new MailmarkCodetext
                {
                    Format = 4,
                    VersionID = 1,
                    Class = "0",
                    SupplychainID = 384224,
                    ItemID = 16563762,
                    DestinationPostCodePlusDPS = "EF61AH8T "
                };

                // Generate the barcode image and save it as PNG
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    string outputPath = "Mailmark.png";
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Mailmark barcode saved to {outputPath}");
                }
            }
            else
            {
                Console.WriteLine($"Unsupported Mailmark type: {selectedMailmarkType}");
            }
        }
        catch (ArgumentException ex)
        {
            // Handle validation errors
            Console.WriteLine("Validation error: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors
            Console.WriteLine("Unexpected error: " + ex.Message);
        }
    }

    /// <summary>
    /// Validates that the customer data length fits the capacity of the selected Mailmark type.
    /// </summary>
    /// <param name="data">Customer content to validate.</param>
    /// <param name="mailmarkType">Selected Mailmark type (e.g., "Mailmark2D" or "Mailmark").</param>
    /// <exception cref="ArgumentException">Thrown when validation fails.</exception>
    private static void ValidateCustomerData(string data, string mailmarkType)
    {
        if (string.IsNullOrEmpty(data))
        {
            // Empty data is acceptable for both types
            return;
        }

        if (mailmarkType.Equals("Mailmark2D", StringComparison.OrdinalIgnoreCase))
        {
            // Ensure the content does not exceed the defined maximum length
            if (data.Length > Mailmark2DCustomerContentMaxLength)
            {
                throw new ArgumentException(
                    $"Customer content length ({data.Length}) exceeds the maximum allowed ({Mailmark2DCustomerContentMaxLength}) for Mailmark2D.");
            }
        }
        else if (mailmarkType.Equals("Mailmark", StringComparison.OrdinalIgnoreCase))
        {
            // 4‑state Mailmark does not have a CustomerContent field; any non‑empty data is invalid.
            throw new ArgumentException("Customer content is not supported for the selected Mailmark type (4‑state).");
        }
        else
        {
            // Unknown Mailmark type supplied
            throw new ArgumentException($"Unknown Mailmark type: {mailmarkType}");
        }
    }
}