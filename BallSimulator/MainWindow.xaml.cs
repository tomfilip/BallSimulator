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
using BallSimulator.Common;
using BallSimulator.Common.Core;

namespace BallSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BallTable Table { get; set; }
        private Ball Item { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Table = new BallTable(new Point(0, 0), new Size(800, 620), Container);
            Ball item = new Ball(new Point(50, 50), new Size(35, 35), rect, Table);
            Item = item;
            Table.ItemsDraw += (object sender, DrawEventArgs args) =>
            {
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
                    if (Item.Position.X > 0) { Item.XVector -= koef; }
                }
                if (e.Key == Key.Right)
                {
                    if (Item.XVector < (Table.Size.Width - 35)) { Item.XVector += koef; }
                }
                if (e.Key == Key.Up)
                {
                    if (Item.Position.X > 0) { Item.YVector -= koef; }
                }
                if (e.Key == Key.Down)
                {
                    if (Item.YVector < (Table.Size.Height - 35)) { Item.YVector += koef; }
                }
            }
        }
    }
}
