using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using demo21111.Models;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
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
        fiouser.Text = Helper.curruser.Surname + " " + Helper.curruser.Name + " " + Helper.curruser.Lastname;
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
        int amount = Convert.ToInt32(Helper.user11129.ZakazPrs.Where(x => x.IdProd == ind).First().Amount.Value)-1;
        if (amount == 0)
        {
            ZakazPr pr = Helper.user11129.ZakazPrs.FirstOrDefault(x => x.IdProd == ind);
            Helper.zakaz.ZakazPrs.Remove(pr);
            Helper.user11129.ZakazPrs.Remove(pr);
           
        }
        else
        {
            Helper.user11129.ZakazPrs.FirstOrDefault(x => x.IdProd == ind).Amount = amount;
            Helper.zakaz.ZakazPrs.FirstOrDefault(x => x.IdProd == ind).Amount = amount;
        }
        Helper.user11129.SaveChanges();
        updateList();
    }

    private void Plus_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        int ind = Convert.ToInt32(((sender as Button)!).Tag!);
        int amount = Convert.ToInt32(Helper.user11129.ZakazPrs.Where(x => x.IdProd == ind).First().Amount.Value) + 1;
        if (amount > Helper.user11129.Prods.FirstOrDefault(x => x.IdProd == ind).Quanskl)
        {
            var ms = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Нельзя увеличить количество данного товара, так как вы выбрали максимальное");
            ms.ShowAsync();
        }
        else
        {
            Helper.user11129.ZakazPrs.FirstOrDefault(x => x.IdProd == ind).Amount = amount;
            Helper.zakaz.ZakazPrs.FirstOrDefault(x => x.IdProd == ind).Amount = amount;
            Helper.user11129.SaveChanges();

        }
        updateList();
    }

    private void CreateOrder_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        bool check = true;
        if (punkts.SelectedIndex == -1)
        {
            check = false;
        }
        else
        {
            Helper.zakaz.IdPunktNavigation = Helper.user11129.Punkts.FirstOrDefault(x => x.Punkt1 == punkts.SelectedItem);
            Helper.zakaz.IdPunkt = Helper.user11129.Punkts.FirstOrDefault(x => x.Punkt1 == punkts.SelectedItem).IdPunkt;
            Helper.user11129.Zakazs.FirstOrDefault(x => x.IdZakaz == Helper.zakaz.IdZakaz).IdPunktNavigation = Helper.zakaz.IdPunktNavigation;
            Helper.user11129.Zakazs.FirstOrDefault(x => x.IdZakaz == Helper.zakaz.IdZakaz).IdPunkt = Helper.zakaz.IdPunkt;
            Helper.user11129.SaveChanges();
        }

        Random rand = new Random();
        int code = rand.Next(100,999);
        Helper.user11129.Zakazs.FirstOrDefault(x => x.IdZakaz == Helper.zakaz.IdZakaz).Code = code;
        Helper.user11129.Zakazs.FirstOrDefault(x => x.IdZakaz == Helper.zakaz.IdZakaz).Datecr = DateOnly.FromDateTime(DateTime.Now.Date);
        string datecr = "Дата создания заказа: " + DateOnly.FromDateTime(DateTime.Now.Date);
        dateZak.Text = datecr;
        int srok;
        bool srokCheck = true;
        float costAll = 0;
        float costDisc = 0;
        foreach (Prod pr in Helper.user11129.Prods)
        {
            foreach (ZakazPr zakPr in Helper.zakaz.ZakazPrs)
            {
                if (pr.IdProd == zakPr.IdProd)
                {
                    costAll += (float)pr.Cost;
                    costDisc += (float)pr.Cost;
                    if (pr.Quanskl > 3 && pr.Quanskl >= zakPr.Amount)
                    {

                    }
                    else
                    { 
                    srokCheck = false;
                    }
                }
            }
        }
        allcost.Text = "Стоимость заказа без скидки: " + costAll.ToString();
        disccost.Text = "Стоимость заказа со скидкой: " + costDisc.ToString();
        if (srokCheck)
        {
            Helper.user11129.Zakazs.FirstOrDefault(x => x.IdZakaz == Helper.zakaz.IdZakaz).Datedel = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(3));
            string datedel = "Дата доставки заказа: " + DateTime.Now.Date.AddDays(3).ToString();
            dateDel.Text = datedel;
        }
        else
        {
            Helper.user11129.Zakazs.FirstOrDefault(x => x.IdZakaz == Helper.zakaz.IdZakaz).Datedel = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(6));
            string datedel = "Дата доставки заказа: " + DateTime.Now.Date.AddDays(6).ToString();
            dateDel.Text = datedel;
        }

        if (check)
        { 
        
        
        }
    }
}