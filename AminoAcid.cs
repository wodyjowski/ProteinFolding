namespace ProteinFolding
{
    public class AminoAcid
    {
        public int X { get; set; }
        public int Y { get; set; }
        public AcidType Type { get; set; }
        public AminoAcid Previous { get; set; }

    }

    public enum AcidType
    {
        H,
        P
    }
}
