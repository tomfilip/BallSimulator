using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BallSimulator.Common.Core;

namespace BallSimulator.Common
{
    public class BallTable: GameTable
    {
        public BallTable(Point position, Size size, UIElement uiContainer)
            :base(position, size, uiContainer)
        {
            this.FloorTension = 0.92f;
        }

        public float FloorTension { get; set; }
    }
}
