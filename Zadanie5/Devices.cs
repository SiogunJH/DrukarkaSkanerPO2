using System;

namespace Base
{
    public interface IDevice
    {
        enum State {on, off, standby};

        public void PowerOn(); // uruchamia urządzenie, zmienia stan na `on`
        public void PowerOff(); // wyłącza urządzenie, zmienia stan na `off
        public void StandbyOn();
        public void StandbyOff();
        public State GetState(); // zwraca aktualny stan urządzenia
        abstract protected void SetState(State state);
    }

    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);
    }

    public interface IScanner : IDevice
    {
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }
}
