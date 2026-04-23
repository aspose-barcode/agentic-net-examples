using System;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;

namespace Example
{
    class Program
    {
        static void Main()
        {
            // Set the maximal number of additional threads to 2 for controlled multithreading
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 2;

            // Output the current setting to verify
            Console.WriteLine($"ProcessorSettings.MaxAdditionalAllowedThreads = {BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads}");
        }
    }
}