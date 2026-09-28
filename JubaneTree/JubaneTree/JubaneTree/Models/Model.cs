using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JubaneTree.Models
{
    public class Model
    {
        public Model()
        {
            this.List = new List<Model>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public IList<Model> List { get; set; }
        public bool IsChild
        {
            get
            {
                return this.List.Count == 0;
            }
        }
    }
}
