using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestS.Common.Core
{
    public class TableItem
    {
        public TableItem(Point position, Size size, UIElement uiContainer, GameTable gameTable)
        {
            this.Id = Guid.NewGuid();
            this.Position = position;
            this.Size = size;
            this.UiContainer = uiContainer;
            this.GameTable = gameTable;
            this.GameTable.ItemsDraw += new DrawEventHangler(ItemsDrawing);

            this.GameTable.AddItem(this);
        }

        private bool _Changed = false;

        public Guid Id { get; private set; }
        private Point _position = default(Point);

        /// <summary>
        /// Represents the pixel position of the item relative to its parent
        /// </summary>
        public Point Position
        {
            get
            {
                return _position;
            }
            set
            {
                _Changed = true;
                _position = value;
            }
        }
        private Size _size = default(Size);

        /// <summary>
        /// Represents the pixel size of the table item
        /// </summary>
        public Size Size
        {
            get
            {
                return _size;
            }
            set
            {
                _Changed = true;
                _size = value;
            }
        }
        public UIElement UiContainer { get; private set; }
        public GameTable GameTable { get; private set; }

        /// <summary>
        /// This method will fire when the game table items draw event is fired
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ItemsDrawing(object sender, DrawEventArgs e)
        {
            if (this.Position.X < 0 || 
                this.Position.Y < 0 ||
                this.Position.X + this.Size.Width > this.GameTable.Size.Width ||
                this.Position.Y + this.Size.Height> this.GameTable.Size.Height)
            {
                //throw new ItemOutsideTableException(this);
            }
            else
            {
                //Perform the ui operation only if it's position has changed
                //Hopefuly this reduces the number of draws required
                if (_Changed)
                {
                    Canvas.SetLeft(UiContainer, Position.X);
                    Canvas.SetTop(UiContainer, Position.Y);
                    this.UiContainer.RenderSize = this.Size;

                    //set the switch back to false
                    _Changed = false;
                }
            }
        }
    }
}
