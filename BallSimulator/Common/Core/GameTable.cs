using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BallSimulator.Common.Core
{
    public class GameTable
    {
        public GameTable(Point position, Size size, UIElement uiContainer)
        {
            this.Size = size;
            this.Position = position;
            this.UiContainer = uiContainer;
        }

        public void Start()
        {
            _running = true;
            Stopwatch sw = new Stopwatch();

            int interval = 15;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (object sender, EventArgs e) => 
            {
                sw.Stop();
                var ellapsedTime = sw.ElapsedMilliseconds;
                sw.Restart();
                float delta = (float)interval / (float)1000;
                Draw(delta);
                if (ellapsedTime > 0)
                { FPS = 1000 / ellapsedTime; }
                else
                { FPS = 1000; }
            };
            timer.Interval = new TimeSpan(0, 0, 0, 0, interval);

            sw.Start();
            timer.Start();
        }

        public void Stop()
        {
            _running = false;
        }

        private bool _running = false;
        private bool _changed = false;

        private Size _size = default(Size);
        public Size Size 
        { 
            get 
            {
                return _size;
            }
            set
            {
                _size = value;
                _changed = true;
            }
        }
        private Point _position = default(Point);
        public Point Position { 
            get 
            {
                return _position;
            }
            set
            {
                _position = value;
                _changed = true;
            }
        }
        public UIElement UiContainer { get; private set; }
        public float FPS { get; private set; }


        private Dictionary<Guid, TableItem> _Items = new Dictionary<Guid, TableItem>();
        //Events

        /// <summary>
        /// This event will be fired before the table drawing begins
        /// </summary>
        public event DrawEventHangler BeforeTableDraw;
        /// <summary>
        /// This event will be fired after the table drawing is completed
        /// </summary>
        public event DrawEventHangler AfterTableDraw;
        /// <summary>
        /// This event will be fired when the table items draw
        /// </summary>
        public event DrawEventHangler ItemsDraw;


        /// <summary>
        /// The draw function. Do all the ui drawings here.
        /// </summary>
        /// <param name="delta">The time delta between the draw events</param>
        protected virtual void Draw(float delta)
        {
            DrawEventArgs args = new DrawEventArgs(delta);
            if (BeforeTableDraw != null)
            { BeforeTableDraw(this, args); }
            
            //Redraw the table container
            if (_changed)
            {
                Canvas.SetLeft(UiContainer, Position.X);
                Canvas.SetTop(UiContainer, Position.Y);
                this.UiContainer.RenderSize = this.Size;

                //Set the switch back
                _changed = false;
            }
            if (AfterTableDraw != null)
            { AfterTableDraw(this, args); }

            if (ItemsDraw != null)
            //Redraw child elements
            { ItemsDraw(this, args); }
        }

        internal virtual void AddItem(TableItem item)
        {
            _Items.Add(item.Id ,item);
        }
        public virtual void RemoveItem(TableItem item)
        {
            _Items.Remove(item.Id);
        }
    }
}
