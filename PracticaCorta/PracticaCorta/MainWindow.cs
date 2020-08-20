using System;
using Gtk;
using PracticaCorta.Identify;
using System.Collections.Generic;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void btnAction(object sender, EventArgs e)
    {


        Identify identificar = new Identify(txtStatement.Text);
        identificar.applySplit();


        List<List<String>> listas = new List<List<string>>();

        listas.Add(identificar.GetListEnteros());
        listas.Add(identificar.GetListDecimaless());
        listas.Add(identificar.GetListPalabras());
        listas.Add(identificar.GetListMoneda());

        TreeViewExample verTokens  = new TreeViewExample(listas);
        //verTokens.addItem("test", "Correcto");
        verTokens.windowShow();

        //verTokens.addItem("Numerico", "test");

        //verTokens.addList("Enteros", identificar.GetListEnteros());
        //verTokens.addList("Decimales", identificar.GetListDecimaless());
        //verTokens.addList("Palabras", identificar.GetListPalabras());
        //verTokens.addList("Moneda", identificar.GetListMoneda());

    }

}
