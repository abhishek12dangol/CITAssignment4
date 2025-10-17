using DataServiceLayer.Models;
using System;

namespace DataServiceLayer.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string ShipName { get; set; }
    public string ShipCity { get; set; }
}