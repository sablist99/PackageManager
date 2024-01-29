namespace PackageManager.Data
{
    public class RamPart
    {
        public int BeginAddress { get; set; } = 0;
        public int EndAddress { get; set; }
        public bool IsEmpty { get; set; } = true;
        public RamPart? PreviousPart { get; set; } = null!;
        public RamPart? NextPart { get; set; } = null!;
        public ProgramTask? ProgramTask { get; set; }
    }
}
