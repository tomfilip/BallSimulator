using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using BallSimulator.Common.Core;

namespace BallSimulator.Common
{
    public class Ball: TableItem
    {
        public int XVector { get; set; }
        public int YVector { get; set; }
        public float Elasticity = 0.7f;
        public float GravityAcceleration = 20f;
        public float Weight = 20;

        public Ball(Point position, Size size, UIElement uiElement, BallTable gameTable)
            : base(position, size, uiElement, gameTable)
        {

        }

        protected override void ItemsDrawing(object sender, DrawEventArgs e)
        {
            var Table = (BallTable)GameTable;

            var newX = this.Position.X + (XVector * e.Delta);
            if (newX < 0)
            {
                newX = 0;
                XVector = (int)(XVector * -Elasticity);
                YVector = (int)(YVector * Table.FloorTension);
            }
            else if (newX > Table.Size.Width - this.Size.Width)
            {
                newX = Table.Size.Width - this.Size.Width;
                XVector = (int)(XVector * -Elasticity);
                YVector = (int)(YVector * Table.FloorTension);
            }

            var newY = this.Position.Y + (YVector * e.Delta);
            if (newY <= 0)
            {
                newY = 0;
                YVector = (int)(YVector * -Elasticity);
                XVector = (int)(XVector * Table.FloorTension);
            }
            else if (newY >= Table.Size.Height - this.Size.Height - 2)
            {
                newY = Table.Size.Height - this.Size.Height;
                YVector = (int)(YVector * -Elasticity);
                XVector = (int)(XVector * Table.FloorTension);
            }
            else
            {
                YVector = YVector + (int)(Weight / (GravityAcceleration * e.Delta));
            }

            this.Position = new Point(newX, newY);

            base.ItemsDrawing(sender, e);
        }
    }
}
