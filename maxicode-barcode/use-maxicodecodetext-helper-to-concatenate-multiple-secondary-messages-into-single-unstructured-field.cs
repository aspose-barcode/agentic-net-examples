// Title: MaxiCode barcode generation with concatenated secondary messages
// Description: Demonstrates how to create a MaxiCode barcode using Aspose.BarCode, concatenating several secondary messages into a single unstructured field for Mode 2.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of MaxiCodeCodetextMode2, MaxiCodeStandardSecondMessage, and ComplexBarcodeGenerator to produce a MaxiCode image. Developers working with logistics, shipping, or inventory systems often need to embed multiple data elements in a MaxiCode; this pattern illustrates how to combine secondary messages into one field before encoding.
// Prompt: Use the MaxiCodeCodetext helper to concatenate multiple secondary messages into a single unstructured field.
// Tags: maxicode, barcode-generation, secondary-message, concatenation, aspose.barcode, complexbarcode

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

namespace MaxiCodeExample
{
    /// <summary>
    /// Generates a MaxiCode barcode (Mode 2) with a concatenated secondary message.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Builds the MaxiCode data, concatenates secondary messages,
        /// and saves the resulting barcode image to disk.
        /// </summary>
        static void Main()
        {
            // Prepare the primary data required for MaxiCode Mode 2
            var maxiCodeData = new MaxiCodeCodetextMode2
            {
                PostalCode = "524032140",   // 9‑digit US postal code
                CountryCode = 56,           // Country code (e.g., USA = 56)
                ServiceCategory = 999       // Service category identifier
            };

            // Define multiple secondary messages that need to be combined
            string[] secondaryMessages = new[]
            {
                "First part of the message",
                "Second part of the message",
                "Additional info"
            };

            // Concatenate the secondary messages into a single unstructured string
            string concatenatedMessage = string.Join(" ", secondaryMessages);

            // Create a standard (unstructured) second message and assign the concatenated text
            var secondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = concatenatedMessage
            };
            maxiCodeData.SecondMessage = secondMessage;

            // Generate the MaxiCode barcode using ComplexBarcodeGenerator.
            // ComplexBarcodeGenerator implements IDisposable, so it is wrapped in a using block.
            using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
            {
                // Produce the barcode image in memory
                generator.GenerateBarCodeImage();

                // Save the generated image to a file (PNG format by default)
                generator.Save("maxicode_output.png");
            }
        }
    }
}