using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlankMonoGameTemplate.Engine.Entities
{
    public interface ICollectible
    {
        bool IsUnique { get; set; }
        bool Collected { get; set; }
        void Collect();
    }
}
