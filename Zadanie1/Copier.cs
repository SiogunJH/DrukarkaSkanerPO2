using ver1;

namespace Zadanie1
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public new int Counter { get; private set; } = 0;
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;

        public new void PowerOn()
        {
            state = IDevice.State.on;
            Counter++;
        }
        public new void PowerOff()
        {
            state = IDevice.State.off;
        }

        public void Print(in IDocument document)
        {
            throw new NotImplementedException();
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            throw new NotImplementedException();
        }
        public void Scan(out IDocument document)
        {
            throw new NotImplementedException();
        }

        public void ScanAndPrint()
        {
            throw new NotImplementedException();
        }
    }
}