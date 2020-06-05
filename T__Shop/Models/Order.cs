using System;
using System.Collections.Generic;

namespace T__Shop
{
    public partial class Order
    {
        public long Id { get; set; }
        public long TattooId { get; set; }
        public string UserId { get; set; }

        public virtual Tattoo Tattoo { get; set; }
    }
}
