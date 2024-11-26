using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using demo21111.Models;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace demo21111;

public partial class AddNew : Window
{
    public Prod pr = new Prod();
    public string filename;
    public string path;
    public string destpath;
    public Bitmap bitm;
    public AddNew()
    {
        InitializeComponent();
    }
    public AddNew(Prod prod)
    {
        InitializeComponent();
        textId.Text = "Id товара: " + pr.IdProd.ToString();
        nameT.Text = prod.Name;
        descrT.Text = prod.Descr;
        maxdT.Text = prod.Maxdisc.ToString();
        currdT.Text = prod.Currdisc.ToString();
        catT.Text = prod.Categ;
        manT.Text = prod.Manuf;
        supT.Text = prod.Suppl;
        switch (prod.Edizm)
        {
            case "уп.":
                edizmCB.SelectedIndex = 0;
                break;
            case "шт.":
                edizmCB.SelectedIndex = 1;
                break;
        }
        imgOut.Source = prod.bitmap;
        artT.Text = prod.Articul;
        costT.Text = prod.Cost.ToString();
        quanT.Text = prod.Quanskl.ToString();
        pr = prod;  
    }

    private void vyhod_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Catalog catalog = new Catalog();
        catalog.Show();
        this.Close();
    }

    private async void img_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        OpenFileDialog op = new OpenFileDialog();
        var res = await op.ShowAsync(this);
        if (res == null) return;
        path = string.Join("", res);
        if (res != null)
        {

           bitm  = new Bitmap(path);
        
        
        }
        imgOut.Source = bitm;
        filename = Path.GetFileName(path);
        destpath = Path.Combine(path, AppDomain.CurrentDomain.BaseDirectory+ "\\" + "Assets");


    }

    private void Ok_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        bool check = true;
       

        if (filename == null)
        {
            filename = "picture.png";
            pr.Image = filename;
        }
        else
        {
            pr.Image = filename;
        }
        if (nameT.Text != null)
        {
            pr.Name = nameT.Text;

        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Прворьте заполненность поля наименования");
            msg.ShowAsync();
        }
        if (manT.Text != null)
        {
            pr.Manuf = manT.Text;

        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Прворьте заполненность поля производителя");
            msg.ShowAsync();
        }
        if (supT.Text != null)
        {
            pr.Suppl = supT.Text;

        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Прворьте заполненность поля поставщика");
            msg.ShowAsync();
        }
        if (catT.Text != null)
        {
            pr.Categ = catT.Text;

        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Прворьте заполненность поля категории");
            msg.ShowAsync();
        }
        int cost;
        if (costT.Text != null && Int32.TryParse(costT.Text, out cost))
        {
            pr.Cost = cost;

        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Прворьте поле текущей скидки");
            msg.ShowAsync();
        }
        float currD;
        if (currdT.Text != null && float.TryParse(currdT.Text, out currD))
        {
            pr.Currdisc = currD;

        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Прворьте поле текущей скидки");
            msg.ShowAsync();
        }
        float maxD;
        if (maxdT.Text != null && float.TryParse(maxdT.Text, out maxD))
        {
            pr.Maxdisc = maxD;

        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Прворьте поле максимальной скидки");
            msg.ShowAsync();
        }
        int quan;
        if (quanT.Text != null && Int32.TryParse(quanT.Text, out quan))
        {
            pr.Quanskl = quan;

        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Прворьте поле количества на складе");
            msg.ShowAsync();
        }
      
            string ed = "";
            switch (edizmCB.SelectedIndex)
            {
                case 0:
                     ed = "уп.";
                    break;
                case 1:
                     ed = "шт.";
                    break;
                case -1:
                    check = false;
                    var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Выберите единицы измерения");
                    msg.ShowAsync();
                    break;

            }
            pr.Edizm = ed;
        

        if (descrT.Text != null)
        {
            pr.Descr = descrT.Text;
        
        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Прворьте заполненность поля описание");
            msg.ShowAsync();
        }
        if (artT.Text != null)
        {
            pr.Articul = artT.Text;

        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Прворьте заполненность поля артикула");
            msg.ShowAsync();
        }
        

        if (check)
        {
            if (pr.IdProd != null)
            {
                Helper.user11129.Prods.Where(x => x.IdProd == pr.IdProd).FirstOrDefault().Image = pr.Image;
                Helper.user11129.Prods.Where(x => x.IdProd == pr.IdProd).FirstOrDefault().Name = pr.Name;
                Helper.user11129.Prods.Where(x => x.IdProd == pr.IdProd).FirstOrDefault().Descr = pr.Descr;
                Helper.user11129.Prods.Where(x => x.IdProd == pr.IdProd).FirstOrDefault().Manuf = pr.Manuf;
                Helper.user11129.Prods.Where(x => x.IdProd == pr.IdProd).FirstOrDefault().Suppl = pr.Suppl;
                Helper.user11129.Prods.Where(x => x.IdProd == pr.IdProd).FirstOrDefault().Maxdisc = pr.Maxdisc;
                Helper.user11129.Prods.Where(x => x.IdProd == pr.IdProd).FirstOrDefault().Currdisc = pr.Currdisc;
                Helper.user11129.Prods.Where(x => x.IdProd == pr.IdProd).FirstOrDefault().Articul = pr.Articul;
                Helper.user11129.Prods.Where(x => x.IdProd == pr.IdProd).FirstOrDefault().Categ = pr.Categ;
                Helper.user11129.Prods.Where(x => x.IdProd == pr.IdProd).FirstOrDefault().Cost = pr.Cost;
                Helper.user11129.Prods.Where(x => x.IdProd == pr.IdProd).FirstOrDefault().Edizm = pr.Edizm;
                Helper.user11129.Prods.Where(x => x.IdProd == pr.IdProd).FirstOrDefault().Quanskl = pr.Quanskl;
            }
            else
            {
                pr.IdProd = Helper.user11129.Prods.OrderBy(x => x.IdProd).LastOrDefault().IdProd + 1;
                Helper.user11129.Prods.Add(pr);
            }
            
            Helper.user11129.SaveChanges();
            Catalog cat = new Catalog();
            cat.Show();
            this.Close();
        }
    }
}