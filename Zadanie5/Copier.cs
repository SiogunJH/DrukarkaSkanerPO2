using Base;
using System.Reflection;
using System.Reflection.Metadata;

namespace Zadanie5
{
    public class Copier
    {
        // MODULES
        public Printer Printer { get; set; }
        public Scanner Scanner { get; set; }

        // GET STATE
        public IDevice.State GetPrinterState() => Printer.GetState();
        public IDevice.State GetScannerState() => Scanner.GetState();
        public IDevice.State GetState()
        {
            if (GetPrinterState() == IDevice.State.off && GetScannerState() == IDevice.State.off) return IDevice.State.off;
            if (GetPrinterState() == IDevice.State.standby && GetScannerState() == IDevice.State.standby) return IDevice.State.standby;
            return IDevice.State.on;
        }

        // STATE MANIPULATION
        public void PowerOff()
        {
            SetPrinterState(IDevice.State.off);
            SetScannerState(IDevice.State.off);
            Console.WriteLine("... Device is off !");
        }
        public void PowerOn()
        {
            SetPrinterState(IDevice.State.on);
            SetScannerState(IDevice.State.on);
            Console.WriteLine("Device is on ...");
        }
        public void StandbyOff()
        {
            if (GetState() == IDevice.State.off) return;
            SetPrinterState(IDevice.State.on);
            SetScannerState(IDevice.State.on);
            Console.WriteLine("Device exited standby and is on ...");
        }
        public void StandbyOn()
        {
            if (GetState() == IDevice.State.off) return;
            SetPrinterState(IDevice.State.standby);
            SetScannerState(IDevice.State.standby);
            Console.WriteLine("... Device is on standby !");
        }

        //SET STATE
        public void SetPrinterState(IDevice.State state) => Printer.SetState(state);
        public void SetScannerState(IDevice.State state) => Scanner.SetState(state);
        public void SetState(IDevice.State state)
        {
            SetScannerState(state);
            SetPrinterState(state);
        }

        //COUNTERS
        public int Counter { get; private set; } = 0;
        public int PrintCounter { get => Printer.Counter; }
        public int ScanCounter { get => Scanner.Counter; }

        //FUNCTIONS
        public void Print(in IDocument document) => Printer.Print(in document);
        public void Scan(out IDocument document, IDocument.FormatType formatType) => Scanner.Scan(out document, formatType);
        public void ScanAndPrint()
        {
            if (GetState() == IDevice.State.off) return;
            
            IDocument document;
            Scan(out document, IDocument.FormatType.JPG);
            Print(document);
        }

        //CONSTRUCTORS
        public Copier()
        {
            Printer = new();
            Scanner = new();
        }
    }
}