using System;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Report ThreadPool status before barcode generation
        ThreadPool.GetAvailableThreads(out int workerBefore, out int ioBefore);
        Console.WriteLine($"ThreadPool before processing: Worker threads available = {workerBefore}, IO threads available = {ioBefore}");

        // Generate a simple Code128 barcode and save it to a file
        const string outputFile = "barcode.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Optional: configure image size or other parameters here
            generator.Save(outputFile);
        }

        // Report ThreadPool status after barcode generation
        ThreadPool.GetAvailableThreads(out int workerAfter, out int ioAfter);
        Console.WriteLine($"ThreadPool after processing: Worker threads available = {workerAfter}, IO threads available = {ioAfter}");
    }
}