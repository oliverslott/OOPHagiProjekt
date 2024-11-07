using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public abstract class Buff
    {
        private string description;

        public string Description { get => description; set => description = value; }

        public abstract void Apply(Player player);
    }
}
