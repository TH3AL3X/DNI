using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNI
{
    public class Config : IRocketPluginConfiguration
    {
        public ushort effect;
        public void LoadDefaults()
        {
            effect = 1505;
        }
    }
}
