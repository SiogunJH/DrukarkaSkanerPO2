using Base;
using System.Reflection;
using System.Reflection.Metadata;

namespace Zadanie4
{
    public class Copier : IPrinter, IScanner
    {
        protected IDevice.State printerState = IDevice.State.off;
        protected IDevice.State scannerState = IDevice.State.off;
        public IDevice.State GetState()
        {
            if (printerState == IDevice.State.off && scannerState == IDevice.State.off) return IDevice.State.off;
            if (printerState == IDevice.State.standby && scannerState == IDevice.State.standby) return IDevice.State.standby;
            return IDevice.State.on;
        }
        public IDevice.State GetPrinterState() => printerState;
        public IDevice.State GetScannerState() => scannerState;

        public void PowerOff()
        {
            printerState = IDevice.State.off;
            scannerState = IDevice.State.off;
            Console.WriteLine("... Device is off !");
        }

        public void PowerOn()
        {
            printerState = IDevice.State.on;
            scannerState = IDevice.State.on;
            Console.WriteLine("Device is on ...");
        }

        public void StandbyOff()
        {
            if (GetState() == IDevice.State.off) return;
            printerState = IDevice.State.on;
            scannerState = IDevice.State.on;
            Console.WriteLine("Device exited standby and is on ...");
        }

        public void StandbyOn()
        {
            if (GetState() == IDevice.State.off) return;
            printerState = IDevice.State.standby;
            scannerState = IDevice.State.standby;
            Console.WriteLine("... Device is on standby !");
        }

        public void SetState(IDevice.State state) 
        { 
            printerState = state;
            scannerState = state;
        }
        public void SetPrinterState(IDevice.State state) => printerState = state;
        
        public void SetScannerState(IDevice.State state) => scannerState = state;
        

        public int Counter { get; private set; } = 0;
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;

        public void Print(in IDocument document)
        {
            if (GetState() == IDevice.State.off) return;
            SetPrinterState(IDevice.State.on);
            SetPrinterState(IDevice.State.standby);

            var outputString = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss ");
            outputString += $"Print: {document.GetFileName()}.";
            switch (document.GetFormatType())
            {
                case IDocument.FormatType.PDF:
                    outputString += "pdf";
                    break;
                case IDocument.FormatType.TXT:
                    outputString += "txt";
                    break;
                case IDocument.FormatType.JPG:
                    outputString += "jpg";
                    break;
            }
            Console.WriteLine(outputString);
            PrintCounter++;
            if (PrintCounter % 2 == 0) SetPrinterState(IDevice.State.standby);
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (GetState() == IDevice.State.off)
            {
                document = null;
                return;
            };
            SetPrinterState(IDevice.State.standby);
            SetScannerState(IDevice.State.on);


            document = new ImageDocument($"ImageScan{ScanCounter:D3}");

            var outputString = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss ");
            outputString += $"Scan: {document.GetFileName()}.";
            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    outputString += "pdf";
                    break;
                case IDocument.FormatType.TXT:
                    outputString += "txt";
                    break;
                case IDocument.FormatType.JPG:
                    outputString += "jpg";
                    break;
            }
            Console.WriteLine(outputString);
            ScanCounter++;
            if (ScanCounter%2==0) SetScannerState(IDevice.State.standby);
        }

        public void Scan(out IDocument document)
        {
            if (GetState() == IDevice.State.off)
            {
                document = null;
                return;
            };
            SetPrinterState(IDevice.State.standby);
            SetScannerState(IDevice.State.on);

            document = new ImageDocument($"ImageScan{ScanCounter:D3}");

            var outputString = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss ");
            outputString += $"Scan: {document.GetFileName()}.jpg";
            Console.WriteLine(outputString);

            ScanCounter++;
            if (ScanCounter % 2 == 0) SetScannerState(IDevice.State.standby);
        }

        public void ScanAndPrint()
        {
            if (GetState() == IDevice.State.off) return;
            
            IDocument document;
            Scan(out document);
            Print(document);
        }
    }
}