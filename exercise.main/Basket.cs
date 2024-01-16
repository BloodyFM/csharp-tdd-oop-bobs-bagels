﻿namespace exercise.main;

public class Basket
{
    private List<IProduct> _products;
    private int _capacity;
    private Menu _menu;

    public Basket()
    {
        _products = new List<IProduct>();
        _capacity = 3;
        _menu = new Menu();
    }

    public int Capacity { get { return _capacity; } set { _capacity = value; } }

    public bool add(int id, string sku)
    {
        var productTuple = _menu.getItem(sku);
        var idExists = _products.FirstOrDefault(x => x.Id() == id);
        if (idExists == null && _products.Count < _capacity)
        {
            if (productTuple.Item2 == "Bagel")
            {
                _products.Add(new Bagel(id, sku));
                return true;
            }
            else if (productTuple.Item2 == "Coffee")
            {
                _products.Add(new Product(id, sku));
                return true;
            }
        }
        return false;
    }

    public bool addFilling(int id, string sku)
    {
        var productTuple = _menu.getItem(sku.Trim());
        Bagel? bagel = _products.OfType<Bagel>().FirstOrDefault(x => x.Id() == id);
        if (productTuple.Item2 == "Filling" && bagel != null)
        {
            bagel.Filling = new Product(id, sku);
            return true;
        }
        return false;
    }

    public bool remove(int id)
    {
        var toDelete = _products.FirstOrDefault(x => x.Id().Equals(id));
        if (toDelete != null)
            return _products.Remove(toDelete);
        return false;
    }

    public double getItemPrice(string sku)
    {
        return _menu.getItem(sku.Trim()).Item1;
    }

    public double getBasketPrice()
    {
        double price = 0.00;
        var bagels = _products.OfType<Bagel>().ToList();
        var coffees = _products.OfType<Product>().ToList();
        foreach (var item in coffees)
        {
            price += _menu.getItem(item.Sku()).Item1;
        }
        foreach (var item in bagels)
        {
            price += _menu.getItem(item.Sku()).Item1;
            if (item.Filling != null)
            {
                price += _menu.getItem(item.Filling.Sku()).Item1;
            }
        }
        return price;
    }
}
