using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Blogs
    {
        public virtual int Id { get; set; }
        public virtual string Heading { get; set; }
        public virtual string ImagesUrl { get; set; }
        public virtual string OtherImagesUrl { get; set; }
        public virtual string Description { get; set; }
        public virtual string UserId { get; set; }
        public virtual string UserName { get; set; }
    }
}
