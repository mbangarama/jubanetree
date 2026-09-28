using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JubaneTree.Models
{
    public class Member
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Please select a title.")]
        public string Title { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Job { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }
        public int FamilyCode { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfBirth { get; set; }
        public bool HasChildren { get; set; }
        //public bool HasChildren {
        //    get { return Children.Count() != 0; }
        //}
        public bool? IsAlive { get; set; }
        public Members nodes { get; set; }

        public string text
        {
            get
            {
                return string.Join(
                    " ",
                    new[] { Name, Surname }
                        .Where(x => !string.IsNullOrWhiteSpace(x)));
            }
        }

        public Member()
        {
            nodes = new Members();
        }
    }

    public class Members : List<Member>
    {
    }
}
