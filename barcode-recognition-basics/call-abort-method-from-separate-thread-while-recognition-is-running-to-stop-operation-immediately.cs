using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    // Method that runs in a separate thread and performs barcode recognition
    private static void ThreadRecognize(object readerObj)
    {
        var reader = (BarCodeReader)readerObj;
        try
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
            }
        }
        catch (RecognitionAbortedException)
        {
            // This exception is thrown when Abort() is called from another thread
            Console.WriteLine("Recognition was aborted.");
        }
    }

    static void Main()
    {
        const string imagePath = "test.png";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the reader with desired decode types
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code39, DecodeType.Code128))
        {
            // Start recognition in a separate thread
            Thread recognizeThread = new Thread(ThreadRecognize);
            recognizeThread.Start(reader);

            // Wait briefly to let the recognition start, then abort it
            Task.Delay(100).Wait();
            reader.Abort();

            // Ensure the worker thread finishes before exiting
            recognizeThread.Join();
        }
    }
}