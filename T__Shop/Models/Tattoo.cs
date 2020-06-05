using System;
using System.Collections.Generic;

namespace T__Shop
{
    public partial class Tattoo
    {
        public Tattoo()
        {
            Order = new HashSet<Order>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
