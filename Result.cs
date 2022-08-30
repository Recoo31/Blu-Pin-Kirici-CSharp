namespace Result1
{
    public enum Status
    {
        Good,
        Bad
    }
    class Result
    {
        private string pin;
        private Status status;

        public string Pin { get => pin; set => pin = value; }
        public Status Status { get => status; set => status = value; }
    }
}
