using Avalonia.Controls;
using demo21111.Models;
using System.Collections.Generic;
using System.Linq;

namespace demo21111
{
    public partial class MainWindow : Window
    {

        public bool access = false;
        public List<Client> clients = Helper.user11129.Clients.ToList();
        public MainWindow()
        {
            InitializeComponent();
            OpenFileDialog op = new OpenFileDialog();
        }

        private void guest_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Helper.role = 2;
            Helper.curruser = null;
            Catalog cat = new Catalog();
            cat.Show();
            this.Close();

        }

        private void vhod_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string loginStr = loginTB.Text;
            string passwdStr = passwdTB.Text;

            foreach (Client cl in clients)
            {
                if (cl.Passwd == passwdStr && cl.Login == loginStr)
                {
                    Helper.curruser = cl;
                    Helper.role = (int)cl.IdRole;
                    access = true;
                    break;
                }
            
            }

            if (access)
            {
                Catalog cat = new Catalog();
                cat.Show();
                this.Close();

            }

        }
    }
}