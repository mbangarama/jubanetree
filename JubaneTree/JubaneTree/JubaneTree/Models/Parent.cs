namespace JubaneTree.Models
{
    public class Parent
    {
        public int MemberLookUpId { get; set; }
        public int? ParentFather { get; set; }
        public int? ParentMother { get; set; }
        public int? FamilyCode { get; set; }
    }
}