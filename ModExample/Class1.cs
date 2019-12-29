using GameDefinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModExample
{
    public class Test : Initializable
    {
        public int priority { get => 100; }

        public void Initialize()
        {
            Debug.LogWithTime(LogLevel.Debug, "My own mod got loaded :D");
        }
    }
}
