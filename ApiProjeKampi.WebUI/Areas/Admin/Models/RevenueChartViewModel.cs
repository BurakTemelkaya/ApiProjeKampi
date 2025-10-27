namespace ApiProjeKampi.WebUI.Areas.Admin.Models;

public class RevenueChartViewModel
{
    public List<string> Labels { get; set; } = [];
    public List<int> Income { get; set; } = [];
    public List<int> Expense { get; set; } = [];

    // Alt kutucuk verileri
    public int TotalReservations { get; set; }
    public int ApprovedReservations { get; set; }
    public int CanceledReservations { get; set; }
}