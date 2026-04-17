using System;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a BarCodeReader without setting an image
        using (var reader = new BarCodeReader())
        {
            try
            {
                // Attempt to export reader settings to XML; this should fail because no image is set
                bool success = reader.ExportToXml("readerExport.xml");
                Console.WriteLine("Export succeeded: " + success);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
            }
        }
    }
}