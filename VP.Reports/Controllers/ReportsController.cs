using DevExpress.XtraReports.Web.WebDocumentViewer.DataContracts;
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VP.Reports.Models;
using VP.Reports.Services;

namespace VP.Reports.Controllers {
    public class ReportsController : Controller {

       private CustomDocumentOperationService _customDocumentOperationService = new CustomDocumentOperationService();

        
        public ReportsController(CustomDocumentOperationService customDocumentOperationService)
        {
            _customDocumentOperationService = customDocumentOperationService;
        }

        public ReportsController()
        {
           
        }
        public ActionResult Index() {
            return View();
        }

        public ActionResult Designer() {
            Models.ReportDesignerModel model = new Models.ReportDesignerModel();
            return View(model);
        }

        public ActionResult Payslip(DateTime? Period) {
            var report = new PaySlip();
            //report.Parameters["Company"].Value = "AppXync";
            //report.Parameters["Branch"].Value = "Lagos";
            return View(report);
        }

        public ActionResult PayAnalysis()
        {
            var report = new PayAnalysis();
            //report.Parameters["Company"].Value = "AppXync";
            //report.Parameters["Branch"].Value = "Lagos";
            return View(report);
        }

        public ActionResult BankSchedule()
        {
            var report = new BankSchedule();
            //report.Parameters["Company"].Value = "AppXync";
            //report.Parameters["Branch"].Value = "Lagos";
            return View(report);
           
        }

        public ActionResult Payee()
        {
            var report = new Payee();
            //report.Parameters["Company"].Value = "AppXync";
            //report.Parameters["Branch"].Value = "Lagos";
            return View(report);
        }

        public ActionResult Pension()
        {
            var report = new Pension();
            //report.Parameters["Company"].Value = "AppXync";
            //report.Parameters["Branch"].Value = "Lagos";
            return View(report);
        }

        public ActionResult PayJournal()
        {
            var report = new PayrollJournal();
            //report.Parameters["Company"].Value = "AppXync";
            //report.Parameters["Branch"].Value = "Lagos";
            return View(report);
        }

        [HttpPost]
        public async Task<ActionResult> EmailPaySlip(StatusMessage sm)
        {   
            
            StatusMessage statusMessage = new StatusMessage();

            statusMessage = _customDocumentOperationService.EmailPaySlip();

            //try
            //{
            //    var companyPolicy = _vpContext.CompanyPolicies.FirstOrDefault();

            //    if (companyPolicy != null)
            //    {
            //        var employees = _vpContext.Employees.ToList();

            //        if (employees != null)
            //        {
            //            foreach (Employee employee in employees)
            //            {
            //                if (employee.EmployeeEmailAddress != null && employee.EmployeeEmailAddress != "")
            //                {
            //                    //load payslip report 
            //                    StaffPaySlip report = new StaffPaySlip();

            //                    string sCurrentPeriod = Convert.ToString(companyPolicy.CurrentPeriod);

            //                    report.Parameters["Period"].Value = Convert.ToDateTime(companyPolicy.CurrentPeriod);
            //                    report.Parameters["EmployeeId"].Value = employee.EmployeeID;

            //                    //export report and send as mail
            //                    var stream = new MemoryStream();
            //                    report.ExportToPdf(stream);
            //                    //return File(stream.GetBuffer(), "application/pdf");

            //                    stream.Position = 0;
            //                    var mailAddress = new MailAddress(employee.EmployeeEmailAddress);
            //                    var recipients = new MailAddressCollection() { mailAddress };
            //                    var attachment = new Attachment(stream, System.Net.Mime.MediaTypeNames.Application.Pdf);
            //                    documentOperationResponse = _customDocumentOperationService.SendEmail(recipients, "Payslip for " + sCurrentPeriod, "Find attached your payslip for " + sCurrentPeriod, attachment);

            //                    if(documentOperationResponse.Succeeded)
            //                    {
            //                        statusMessage.Status = "Success";
            //                        statusMessage.Message = documentOperationResponse.Message;
            //                    }
            //                    else{
            //                        statusMessage.Status = "Failed";
            //                        statusMessage.Message = documentOperationResponse.Message;
            //                    }

            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    statusMessage.Status = "Failed";
            //    statusMessage.Message = ex.Message;
            //}

            return Json(statusMessage);
        }
    }
}