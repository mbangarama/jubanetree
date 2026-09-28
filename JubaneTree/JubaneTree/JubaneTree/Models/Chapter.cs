using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JubaneTree.Models
{
    public class Chapter
    {
        public int ChapterID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
    public class Topic
    {
        public int TopicID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SubTopic> SubTopics { get; set; }

        public int ChapterID { get; set; }
        public virtual Chapter Chapter { get; set; }
    }
    public class SubTopic
    {
        public int SubTopicID { get; set; }
        public string Name { get; set; }

        public int TopicID { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
