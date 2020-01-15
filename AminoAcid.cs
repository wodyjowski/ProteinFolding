using System;

namespace ProteinFolding
{
    public class AminoAcid
    {
        public AminoAcid(char type)
        {
            Type = (AcidType)Enum.Parse(typeof(AcidType), type.ToString());
        }
        public int X { get; set; }
        public int Y { get; set; }
        public AcidType Type { get; set; }
        public AminoAcid Previous { get; set; }
        public int PrevDirection { get; set; } = -1;

        public void Up()
        {
            PrevDirection = 0;
            --X;
        }
        
        public void Down()
        {
            PrevDirection = 1;
            ++X;
        }

        public void Left()
        {
            PrevDirection = 2;
            --Y;
        }

        public void Right()
        {
            PrevDirection = 3;
            ++Y;
        }

        public override bool Equals(object? obj)
        {
            AminoAcid testAcid = obj as AminoAcid;
            if (testAcid == null)
                return false;

            if (testAcid.X == X && testAcid.Y == Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    public enum AcidType
    {
        H,
        P
    }
}
