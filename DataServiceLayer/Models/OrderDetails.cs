using System;

namespace DataServiceLayer.Models;

public class OrderDetails
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }

    private double _unitPrice;

    public double UnitPrice
    {
        get => _unitPrice;
        set
        {
            if (value < 0) throw new ArgumentException("Price cannot be negative", nameof(UnitPrice));
            _unitPrice = value;
        }

    }

    private double _unitQuantity;
    public double Quantity
    {
        get => _unitQuantity;
        set
        {
            if (value <= 0) throw new ArgumentException("Quantity cannot be less than zero", nameof(Quantity));
            _unitQuantity = value;
        }
    }

    private double _discount;

    public double Discount
    {
        get => _discount;
        set
        {
            if (value < 0 || value > 1) throw new ArgumentException("Discount must be 0 and 1", nameof(Discount));
            _discount = value;
        }
    }

    public Order Order { get; set; }
    public Product Product { get; set; }
}