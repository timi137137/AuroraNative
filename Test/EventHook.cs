using AuroraNative;
using AuroraNative.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class EventHook : Event
    {
        public override void LifeCycle(LifeCycleArgs e)
        {
            Console.WriteLine(e.MetaEventType);
            Console.WriteLine(e.PostType);
            Console.WriteLine(e.SubType);
        }
    }
}
