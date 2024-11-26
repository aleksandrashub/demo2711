using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using demo21111.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace demo21111;

public partial class Korz : Window
{
    public List<string> punktL = Helper.user11129.Punkts.Select(x => x.Punkt1).ToList();
    public List<ZakazPr> zakazpr = Helper.user11129.ZakazPrs.Where(x => x.IdZakaz == Helper.zakaz.IdZakaz).Include(x => x.IdZakazNavigation).Include(x => x.IdProdNavigation).ToList();
    public Korz()
    {
        InitializeComponent();
        updateList();
        punkts.ItemsSource = punktL;
    }

    private void updateList()
    {
        var listP = zakazpr;
        list.ItemsSource = listP.ToList();
        
    }

    private void vyhod_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

        Catalog cat = new Catalog();
        cat.Show();
        this.Close();
    }

    private void Minus_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        int ind = Convert.ToInt32(((sender as Button)!).Tag!);
        int amount = Convert.ToInt32(Helper.user11129.ZakazPrs.Where(x => x.IdProd == ind).First().Amount.Value);
      //  if (amount)

    }

    private void Plus_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        int ind = Convert.ToInt32(((sender as Button)!).Tag!);



    }
}