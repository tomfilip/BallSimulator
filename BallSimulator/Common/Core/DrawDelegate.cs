using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestS.Common.Core
{
    public delegate void DrawEventHangler(object sender, DrawEventArgs e);

    /// <summary>
    /// The draw event arguments. Contains the time delta between the draw events.
    /// </summary>
    public class DrawEventArgs : EventArgs
    {
        public DrawEventArgs(float delta)
        {
            this.Delta = delta;
        }

        public float Delta{ get; private set; }
    }
}
