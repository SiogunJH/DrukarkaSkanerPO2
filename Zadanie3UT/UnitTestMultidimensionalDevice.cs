using Microsoft.VisualStudio.TestTools.UnitTesting;
using Base;
using Zadanie3;
using System;
using System.IO;

namespace Zadanie2UT
{

    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }


    [TestClass]
    public class UnitTestMultidimensionalDevice
    {
        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOff()
        {
            var multiDevice = new MultidimensionalDevice();
            multiDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, multiDevice.GetState());
        }

        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOn()
        {
            var multiDevice = new MultidimensionalDevice();
            multiDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, multiDevice.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączonym urządzeniu wielofunkcyjnym w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Print_DeviceOn()
        {
            var multiDevice = new MultidimensionalDevice();
            multiDevice.PowerOn();
            multiDevice.Printer.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multiDevice.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonym urządzeniu wielofunkcyjnym w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Print_DeviceOff()
        {
            var multiDevice = new MultidimensionalDevice();
            multiDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multiDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonym urządzeniu wielofunkcyjnym w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Scan_DeviceOff()
        {
            var multiDevice = new MultidimensionalDevice();
            multiDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multiDevice.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonym urządzeniu wielofunkcyjnym w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Scan_DeviceOn()
        {
            var multiDevice = new MultidimensionalDevice();
            multiDevice.PowerOn();
            multiDevice.Scanner.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multiDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultidimensionalDevice_Scan_FormatTypeDocument()
        {
            var multiDevice = new MultidimensionalDevice();
            multiDevice.PowerOn();
            multiDevice.Scanner.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multiDevice.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                multiDevice.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                multiDevice.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonym urządzeniu wielofunkcyjnym w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_ScanAndPrint_DeviceOn()
        {
            var multiDevice = new MultidimensionalDevice();
            multiDevice.PowerOn();
            multiDevice.Printer.PowerOn();
            multiDevice.Scanner.PowerOn();


            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multiDevice.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonym urządzeniu wielofunkcyjnym w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_ScanAndPrint_DeviceOff()
        {
            var multiDevice = new MultidimensionalDevice();
            multiDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multiDevice.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Fax` i włączonym urządzeniu wielofunkcyjnym w napisie pojawia się słowo `Fax`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Fax_DeviceOn()
        {
            var multiDevice = new MultidimensionalDevice();
            multiDevice.PowerOn();
            multiDevice.FaxMachine.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multiDevice.Fax(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Fax` i wyłączonym urządzeniu wielofunkcyjnym w napisie NIE pojawia się słowo `Fax`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Fax_DeviceOff()
        {
            var multiDevice = new MultidimensionalDevice();
            multiDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multiDevice.Fax(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_PrintCounter()
        {
            var multiDevice = new MultidimensionalDevice();
            multiDevice.PowerOn();
            multiDevice.Printer.PowerOn();
            multiDevice.Scanner.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            multiDevice.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            multiDevice.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            multiDevice.Print(in doc3);

            multiDevice.PowerOff();
            multiDevice.Print(in doc3);
            multiDevice.Scan(out doc1);
            multiDevice.PowerOn();

            multiDevice.ScanAndPrint();
            multiDevice.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, multiDevice.PrintCounter);
        }

        [TestMethod]
        public void MultidimensionalDevice_ScanCounter()
        {
            var multiDevice = new MultidimensionalDevice();
            multiDevice.PowerOn();
            multiDevice.Printer.PowerOn();
            multiDevice.Scanner.PowerOn();

            IDocument doc1;
            multiDevice.Scan(out doc1);
            IDocument doc2;
            multiDevice.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multiDevice.Print(in doc3);

            multiDevice.PowerOff();
            multiDevice.Print(in doc3);
            multiDevice.Scan(out doc1);
            multiDevice.PowerOn();

            multiDevice.ScanAndPrint();
            multiDevice.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, multiDevice.ScanCounter);
        }

        [TestMethod]
        public void MultidimensionalDevice_PowerOnCounter()
        {
            var multiDevice = new MultidimensionalDevice();
            multiDevice.PowerOn();
            multiDevice.PowerOn();
            multiDevice.PowerOn();

            IDocument doc1;
            multiDevice.Scan(out doc1);
            IDocument doc2;
            multiDevice.Scan(out doc2);

            multiDevice.PowerOff();
            multiDevice.PowerOff();
            multiDevice.PowerOff();
            multiDevice.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multiDevice.Print(in doc3);

            multiDevice.PowerOff();
            multiDevice.Print(in doc3);
            multiDevice.Scan(out doc1);
            multiDevice.PowerOn();

            multiDevice.ScanAndPrint();
            multiDevice.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, multiDevice.Counter);
        }

    }
}
