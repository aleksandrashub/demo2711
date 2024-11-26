using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using demo21111.Models;
using MsBox.Avalonia;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace demo21111;

public partial class Catalog : Window
{

    public List<Prod> prods = Helper.user11129.Prods.ToList();
    public Catalog()
    {
        InitializeComponent();
        if (Helper.curruser != null)
        {
            fioTxt.Text = Helper.curruser.Surname + " " + Helper.curruser.Name + " " + Helper.curruser.Lastname; ;


        }
        if (Helper.role == 1)
        {
            addBtn.IsVisible = true;
            listUser.IsVisible = false;
            listAdm.IsVisible = true;
        }
        else
        {
            listUser.IsVisible = true;
            listAdm.IsVisible = false;
            addBtn.IsVisible = false;
        }
        updateList();
      
    }
    private void updateList()
    {
        prods = Helper.user11129.Prods.ToList();

        var currprods = prods;

        switch (sort.SelectedIndex)
        {
            case 0:
                currprods = prods.OrderByDescending(x => x.ItogCost).ToList();
                
                break; 

            case 1:

                currprods = prods.OrderBy(x => x.ItogCost).ToList();
                break;
            case 2:
            default:
                break;
        
        
        }

        switch (filter.SelectedIndex)
        {
               
            case 1:

                currprods = currprods.Where(x => x.Currdisc >= 0 && x.Currdisc <= 9.99).ToList();
                break;
             case 2:
                
                currprods = currprods.Where(x => x.Currdisc >= 10 && x.Currdisc <= 14.99).ToList();
                break;
               
            case 3:
                currprods = currprods.Where(x => x.Currdisc >= 15).ToList();
                break;
            case 0:
            default:
                break;
        
        
        }

        string search = searchText.Text ?? "";
        int count = search.Split(' ').Length;
        search = search.ToLower();
        string[] values = new string[count];
        values = search.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries) ;
        if (!string.IsNullOrEmpty(search))
        { 
        currprods = prods.Where(x => x.Name.ToLower().Contains(search)).ToList();
        
        }
        var list = currprods;
        if (Helper.role == 1)
        {
            listAdm.ItemsSource = list;
        }
        else
        { 
        listUser.ItemsSource = list;
        }
       
        if (Helper.zakaz != null)
        {
            if (Helper.zakaz.ZakazPrs.Count() > 0)
            {
                korzBtn.IsVisible = true;
            }
            else
            {
                korzBtn.IsVisible = false;
            }
        }
        else
        {
            korzBtn.IsVisible = false;
        }
    }

    private void sort_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        updateList();
    }

  
    private void filter_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        updateList();
    }

    private void poisk_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        updateList();
    }

    private void MenuItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var prod = new Prod();
        if (Helper.role == 1)
        {
             prod = listAdm.SelectedItem as Prod;
        }
        else
        {
             prod = listUser.SelectedItem as Prod;
        }
        if (Helper.zakaz == null)
        {
            Helper.zakaz = new Zakaz();
            if (Helper.user11129.Zakazs.Count() > 0)
            {
                Helper.zakaz.IdZakaz = Helper.user11129.Zakazs.OrderBy(x => x.IdZakaz).LastOrDefault().IdZakaz + 1;
            }
            else
            {
                Helper.zakaz.IdZakaz = 1;
            }
            Helper.user11129.Zakazs.Add(Helper.zakaz);
            Helper.user11129.SaveChanges();
        }
        ZakazPr zakazPr = new ZakazPr();
        zakazPr.IdZakazNavigation = Helper.user11129.Zakazs.Where(x => x.IdZakaz == Helper.zakaz.IdZakaz).FirstOrDefault();
        zakazPr.IdZakaz = Helper.zakaz.IdZakaz;
        zakazPr.IdProdNavigation = Helper.user11129.Prods.Where(x => x.IdProd == prod.IdProd).FirstOrDefault();
        zakazPr.IdProd = prod.IdProd;
        zakazPr.Amount = 1;
        Helper.zakaz.ZakazPrs.Add(zakazPr);
        Helper.user11129.ZakazPrs.Add(zakazPr);
        Helper.user11129.SaveChanges();
        updateList();
    }

    private void vyhod_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Helper.curruser = null;
        Helper.role = -1;
        MainWindow main = new MainWindow();
        main.Show();
        this.Close();

    }

    private void Add_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
       AddNew add = new AddNew();
        add.Show();
        this.Close();

    }

    private void Korz_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Korz korz = new Korz();
        korz.Show();
        this.Close();
    }

   

    private void Delete_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        int ind = Convert.ToInt32(((sender as Button)!).Tag!);
        if (Helper.user11129.ZakazPrs.Where(x => x.IdProd == ind).Count() > 0)
        {
            var ms = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данный товар содержится в заказах, удаление запрещено");
            ms.ShowAsync();
        }
        else
        {
            Prod prod = Helper.user11129.Prods.Where(x => x.IdProd == ind).First();
            Helper.user11129.Prods.Remove(prod);
            Helper.user11129.SaveChanges();
            updateList();
        }
    }

    private void Edit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        int ind = Convert.ToInt32(((sender as Button)!).Tag!);
        if (Helper.role == 1 )
        {
            Prod prod = Helper.user11129.Prods.Where(x => x.IdProd == ind).First();    
            if (Helper.user11129.ZakazPrs.Select(x => x.IdProd).Contains(ind))
            {
                var ms = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Товар содержится в заказах, редактирование запрещено");
                ms.ShowAsync();
            }
            else
            {
                AddNew addNew = new AddNew(prod!);
                addNew.Show();
                this.Close();

            }
        }
    }
}