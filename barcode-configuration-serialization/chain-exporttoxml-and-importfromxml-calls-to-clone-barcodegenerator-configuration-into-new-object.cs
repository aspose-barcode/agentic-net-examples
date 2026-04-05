using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeCloneExample
{
    class Program
    {
        static void Main()
        {
            // Create the original barcode generator and configure it
            using (var originalGenerator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                originalGenerator.CodeText = "123ABC";
                // Set some visual parameters using unit members
                originalGenerator.Parameters.BackColor = Color.Yellow;
                originalGenerator.Parameters.Barcode.XDimension.Point = 2f;
                originalGenerator.Parameters.Barcode.BarHeight.Point = 40f;

                // Export the configuration to a memory stream as XML
                using (var xmlStream = new MemoryStream())
                {
                    bool exportSuccess = originalGenerator.ExportToXml(xmlStream);
                    if (!exportSuccess)
                    {
                        throw new InvalidOperationException("Failed to export barcode configuration to XML.");
                    }

                    // Reset stream position before importing
                    xmlStream.Seek(0, SeekOrigin.Begin);

                    // Import the configuration from the XML stream into a new generator
                    BarcodeGenerator clonedGenerator = BarcodeGenerator.ImportFromXml(xmlStream);
                    if (clonedGenerator == null)
                    {
                        throw new InvalidOperationException("Failed to import barcode configuration from XML.");
                    }

                    // Save both barcodes to verify that the clone has the same settings
                    originalGenerator.Save("original.png");
                    clonedGenerator.Save("cloned.png");

                    // Dispose the cloned generator
                    clonedGenerator.Dispose();
                }
            }

            Console.WriteLine("Original and cloned barcode images have been saved.");
        }
    }
}