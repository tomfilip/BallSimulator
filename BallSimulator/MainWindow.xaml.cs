using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestS.Common.Core;

namespace TestS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private GameTable Table { get; set; }
        private TableItem Item { get; set; }
        private int X { get; set; }
        private int Y { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Table = new GameTable(new Point(0, 0), new Size(800, 620), Container);
            TableItem item = new TableItem(new Point(50, 50), new Size(35, 35), rect, Table);
            Item = item;
            Table.ItemsDraw += (object sender, DrawEventArgs args) =>
            {
                float damagaKoef = 0.7f;
                float gravityAcceleration = 20f;
                float weight = 20;
                float floorTension = 0.95f;

                var newX = item.Position.X + (X * args.Delta);
                if (newX < 0)
                { 
                    newX = 0;
                    X = (int)(X * -damagaKoef);
                    Y = (int)(Y * floorTension);
                }
                else if (newX > Table.Size.Width - item.Size.Width)
                { 
                    newX = Table.Size.Width - item.Size.Width;
                    X = (int)(X * -damagaKoef);
                    Y = (int)(Y * floorTension);
                }

                var newY = item.Position.Y + (Y * args.Delta);
                if (newY <= 0)
                { 
                    newY = 0;
                    Y = (int)(Y * -damagaKoef);
                    X = (int)(X * floorTension);
                }
                else if (newY >= Table.Size.Height - item.Size.Height - 2)
                { 
                    newY = Table.Size.Height - item.Size.Height;
                    Y = (int)(Y * -damagaKoef);
                    X = (int)(X * floorTension);
                }
                else
                {
                    Y = Y + (int)(weight / (gravityAcceleration * args.Delta));
                }
                
                item.Position = new Point(newX, newY);
                
                lblFPS.Content = Math.Round(Table.FPS, 2);
            };

            Table.Start();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            int koef = 400;
            //float ratio = 1.5;

            if (Item != null){
                /*if (Item.Position.X == 0)
                { X = 0; }
                else if (Item.Position.X >= Table.Size.Width - Item.Size.Width)
                { X = 0; }

                if (Item.Position.Y == 0)
                { Y = 0; }
                else if (Item.Position.Y >= Table.Size.Height - Item.Size.Height)
                { Y = 0; }*/

                if (e.Key == Key.Left)
                {
                    if (Item.Position.X > 0) { X -= koef; }
                }
                if (e.Key == Key.Right)
                {
                    if (X < (Table.Size.Width - 35)) { X += koef; }
                }
                if (e.Key == Key.Up)
                {
                    if (Item.Position.X > 0) { Y -= koef; }
                }
                if (e.Key == Key.Down)
                {
                    if (Y < (Table.Size.Height - 35)) { Y += koef; }
                }
            }
        }
    }
}
