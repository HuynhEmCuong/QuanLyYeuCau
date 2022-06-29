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

        #region Success
        public double TotalSuccess { get; set; }

        public double TotalSuccessLate { get; set; }
        #endregion

        #region Proceesing

        public double TotalProceesing { get; set; }

        public double TotalProceesingLate { get; set; }

        #endregion
        public double TotalLate { get; set; }
        public double  Total { get; set; }
    }
}
