using System;
using System.Collections.Generic;

public class TreeViewExample
{
   

    Gtk.Entry filterEntry;

    Gtk.TreeModelFilter filter;
    private Gtk.ListStore tokenListStore;
    private Gtk.Window window;

    public TreeViewExample(List<List<String>> tokens)
    {
        // Create a Window
        window = new Gtk.Window("Lista De Tokens");
        window.SetSizeRequest(500, 200);

        // Create an Entry used to filter the tree
        filterEntry = new Gtk.Entry();

        // Fire off an event when the text in the Entry changes
        filterEntry.Changed += OnFilterEntryTextChanged;

        // Create a nice label describing the Entry
        Gtk.Label filterLabel = new Gtk.Label("Token:");

        // Put them both into a little box so they show up side by side
        Gtk.HBox filterBox = new Gtk.HBox();
        filterBox.PackStart(filterLabel, false, false, 5);
        filterBox.PackStart(filterEntry, true, true, 5);

        // Create our TreeView
        Gtk.TreeView tree = new Gtk.TreeView();

        // Create a box to hold the Entry and Tree
        Gtk.VBox box = new Gtk.VBox();

        // Add the widgets to the box
        box.PackStart(filterBox, false, false, 5);
        box.PackStart(tree, true, true, 5);

        window.Add(box);

        // Create a column for the artist name
        Gtk.TreeViewColumn tipoTokenColumn = new Gtk.TreeViewColumn();
        tipoTokenColumn.Title = "TIPO";

        // Create the text cell that will display the artist name
        Gtk.CellRendererText tipoTokenDescriptionCell = new Gtk.CellRendererText();

        // Add the cell to the column
        tipoTokenColumn.PackStart(tipoTokenDescriptionCell, true);

        // Create a column for the song title
        Gtk.TreeViewColumn tokenColumn = new Gtk.TreeViewColumn();
        tokenColumn.Title = "Token";

        // Do the same for the song title column
        Gtk.CellRendererText tokenTitleCell = new Gtk.CellRendererText();
        tokenColumn.PackStart(tokenTitleCell, true);

        // Add the columns to the TreeView
        tree.AppendColumn(tipoTokenColumn);
        tree.AppendColumn(tokenColumn);

        // Tell the Cell Renderers which items in the model to display
        tipoTokenColumn.AddAttribute(tipoTokenDescriptionCell, "text", 0);
        tokenColumn.AddAttribute(tokenTitleCell, "text", 1);

        // Create a model that will hold two strings - Artist Name and Song Title
        this.tokenListStore = new Gtk.ListStore(typeof(string), typeof(string));

        // Add some data to the store
        int c = 1;
        tokens.ForEach(lst =>
        {

            lst.ForEach(x => {
                switch (c)
                {
                    case 1:

                            tokenListStore.AppendValues("Entero", x);
                            break;
                    case 2:

                        tokenListStore.AppendValues("Decimal", x);
                        break;
                    case 3:

                        tokenListStore.AppendValues("Palabra", x);
                        break;
                    case 4:

                        tokenListStore.AppendValues("Moneda", x);
                        break;




                }
            });
            c++;
        });


        // Instead of assigning the ListStore model directly to the TreeStore, we create a TreeModelFilter
        // which sits between the Model (the ListStore) and the View (the TreeView) filtering what the model sees.
        // Some may say that this is a "Controller", even though the name and usage suggests that it is still part of
        // the Model.
        filter = new Gtk.TreeModelFilter(tokenListStore, null);

        // Specify the function that determines which rows to filter out and which ones to display
        filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc(FilterTree);

        // Assign the filter as our tree's model
        tree.Model = filter;

        // Show the window and everything on it

    }
    public void addItem(string tipo, string token)
    {
        this.tokenListStore.AppendValues(tipo, token);
    }
    public void addList(string tipo, List<String> listaTokens)
    {
        //tokenListStore.AppendValues(tipo, token);
        listaTokens.ForEach(x => { tokenListStore.AppendValues(tipo, x); });

    }
    public void windowShow()
    {
        window.ShowAll();
    }
    private void OnFilterEntryTextChanged(object o, System.EventArgs args)
    {
        // Since the filter text changed, tell the filter to re-determine which rows to display
        filter.Refilter();
    }

    private bool FilterTree(Gtk.TreeModel model, Gtk.TreeIter iter)
    {
        string artistName = model.GetValue(iter, 0).ToString();

        if (filterEntry.Text == "")
            return true;

        if (artistName.IndexOf(filterEntry.Text) > -1)
            return true;
        else
            return false;
    }
}
