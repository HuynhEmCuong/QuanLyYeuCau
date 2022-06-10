using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.ViewModels.Reports
{
    public class ReportUserViewModel
    {
        public ReportUserViewModel()
        {
            
        }

        public ReportUserViewModel(int userId, string userName, double totalSuccess =0, double totalProceesing =0, double totalLate=0,double total=0)
        {
            UserId = userId;
            UserName = userName;
            TotalSuccess = totalSuccess;
            TotalProceesing = totalProceesing;
            TotalLate = totalLate;
            Total = total;
        }

        public int   UserId { get; set; }

        public string   UserName  { get; set; }

        public double TotalSuccess { get; set; }

        public double TotalProceesing { get; set; }

        public double TotalLate { get; set; }
        public double  Total { get; set; }
    }
}
