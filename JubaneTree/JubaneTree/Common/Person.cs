using System.Collections.Generic;
namespace Common
{
    public class Person
    {
        public int MemberID { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Job { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }
        public bool HasChildren { get; set; }
        public int FamilyCodeID { get; set; }
        public bool IsAlive { get; set; }
        public List<Person> Children { get; set; }
    }
}