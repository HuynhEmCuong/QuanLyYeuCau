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

        public ReportUserViewModel(int userId, string userName, double totalSuccess =0, double totalProceesing =0, double totalDisable=0,double total=0,
            double totalProceesingLate =0)
        {
            UserId = userId;
            UserName = userName;
            TotalSuccess = totalSuccess;
            TotalDisable = totalDisable;

            TotalProceesing = totalProceesing;
            TotalProceesingLate = totalProceesingLate;
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
        public double  Total { get; set; }

        public double TotalDisable { get; set; }
    }
}
