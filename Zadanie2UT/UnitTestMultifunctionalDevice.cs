using Microsoft.VisualStudio.TestTools.UnitTesting;
using Base;
using Zadanie2;
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
    public class UnitTestMultifunctionalDevice
    {
        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOff()
        {
            var multiDevice = new MultifunctionalDevice();
            multiDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, multiDevice.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOn()
        {
            var multiDevice = new MultifunctionalDevice();
            multiDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, multiDevice.GetState());
        }


        // weryfikacja, czy po wywo³aniu metody `Print` i w³¹czonym urz¹dzeniu wielofunkcyjnym w napisie pojawia siê s³owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOn()
        {
            var multiDevice = new MultifunctionalDevice();
            multiDevice.PowerOn();

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

        // weryfikacja, czy po wywo³aniu metody `Print` i wy³¹czonym urz¹dzeniu wielofunkcyjnym w napisie NIE pojawia siê s³owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOff()
        {
            var multiDevice = new MultifunctionalDevice();
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

        // weryfikacja, czy po wywo³aniu metody `Scan` i wy³¹czonym urz¹dzeniu wielofunkcyjnym w napisie NIE pojawia siê s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOff()
        {
            var multiDevice = new MultifunctionalDevice();
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

        // weryfikacja, czy po wywo³aniu metody `Scan` i wy³¹czonym urz¹dzeniu wielofunkcyjnym w napisie pojawia siê s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOn()
        {
            var multiDevice = new MultifunctionalDevice();
            multiDevice.PowerOn();

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

        // weryfikacja, czy wywo³anie metody `Scan` z parametrem okreœlaj¹cym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultifunctionalDevice_Scan_FormatTypeDocument()
        {
            var multiDevice = new MultifunctionalDevice();
            multiDevice.PowerOn();

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


        // weryfikacja, czy po wywo³aniu metody `ScanAndPrint` i wy³¹czonym urz¹dzeniu wielofunkcyjnym w napisie pojawiaj¹ siê s³owa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOn()
        {
            var multiDevice = new MultifunctionalDevice();
            multiDevice.PowerOn();

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

        // weryfikacja, czy po wywo³aniu metody `ScanAndPrint` i wy³¹czonym urz¹dzeniu wielofunkcyjnym w napisie NIE pojawia siê s³owo `Print`
        // ani s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOff()
        {
            var multiDevice = new MultifunctionalDevice();
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

        // weryfikacja, czy po wywo³aniu metody `Fax` i w³¹czonym urz¹dzeniu wielofunkcyjnym w napisie pojawia siê s³owo `Fax`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Fax_DeviceOn()
        {
            var multiDevice = new MultifunctionalDevice();
            multiDevice.PowerOn();

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

        // weryfikacja, czy po wywo³aniu metody `Fax` i wy³¹czonym urz¹dzeniu wielofunkcyjnym w napisie NIE pojawia siê s³owo `Fax`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Fax_DeviceOff()
        {
            var multiDevice = new MultifunctionalDevice();
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
        public void MultifunctionalDevice_PrintCounter()
        {
            var multiDevice = new MultifunctionalDevice();
            multiDevice.PowerOn();

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

            // 5 wydruków, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(5, multiDevice.PrintCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanCounter()
        {
            var multiDevice = new MultifunctionalDevice();
            multiDevice.PowerOn();

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

            // 4 skany, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(4, multiDevice.ScanCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_PowerOnCounter()
        {
            var multiDevice = new MultifunctionalDevice();
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

            // 3 w³¹czenia
            Assert.AreEqual(3, multiDevice.Counter);
        }

    }
}
