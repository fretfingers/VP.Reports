using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using DevExpress.XtraReports.Web.WebDocumentViewer.DataContracts;
using VP.Reports.Models;

namespace VP.Reports.Services
{
    public class CustomDocumentOperationService : DocumentOperationService
    {
        private VinPayEntities _vpContext = new VinPayEntities();
        public override bool CanPerformOperation(DocumentOperationRequest request)
        {
            return true;
        }

        public override DocumentOperationResponse PerformOperation(DocumentOperationRequest request, PrintingSystemBase initialPrintingSystem, PrintingSystemBase printingSystemWithEditingFields)
        {
            var companyPolicy = _vpContext.CompanyPolicies.FirstOrDefault();

            using (var stream = new MemoryStream())
            {
                printingSystemWithEditingFields.ExportToPdf(stream);
                stream.Position = 0;
                var mailAddress = new MailAddress(companyPolicy.Email);
                var recipients = new MailAddressCollection() { mailAddress };
                var attachment = new Attachment(stream, "PayslipAll", System.Net.Mime.MediaTypeNames.Application.Pdf);
                return SendEmail(recipients, "Payroll Report", "Kindly find attached payroll reports", attachment);
            }
        }

        public DocumentOperationResponse SendEmail(MailAddressCollection recipients, string subject, string messageBody, Attachment attachment)
        {
            
            var company = _vpContext.Companies.FirstOrDefault();
            var companyPolicy = _vpContext.Companies.FirstOrDefault();

            string SmtpHost = company.SmtpHost; // "smtpout.secureserver.net";

            int SmtpPort = Convert.ToInt32(company.SmtpPort); // 80;

            if (string.IsNullOrEmpty(SmtpHost) || SmtpPort == -1)
            {
                return new DocumentOperationResponse { Message = "Please configure the SMTP server settings." };
            }

            string SmtpUserName = company.SmtpUsername == "" || company.SmtpUsername == null ? company.Email : company.SmtpUsername; // "a.leke@powersoft-solutions.org";
            string SmtpUserPassword = company.SmtpPassword; // "Akinboro@123";
            string SmtpFrom = company.SmtpUsername == "" || company.SmtpUsername == null ? company.Email : company.SmtpUsername; // "a.leke@powersoft-solutions.org";
            string SmtpFromDisplayName = company == null ? companyPolicy.Company1 : company.Name;// "AppXync Co";

            

            using (var smtpClient = new SmtpClient(SmtpHost, SmtpPort))
            {
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                if (!string.IsNullOrEmpty(SmtpUserName))
                {
                    smtpClient.Credentials = new NetworkCredential(SmtpUserName, SmtpUserPassword);
                }

                using (var message = new MailMessage())
                {
                    message.Subject = subject.Replace("\r", "").Replace("\n", "");
                    message.IsBodyHtml = true;
                    message.Body = messageBody;
                    message.From = new MailAddress(SmtpFrom, SmtpFromDisplayName);

                    foreach (var item in recipients)
                    {
                        message.To.Add(item);
                    }

                    try
                    {
                        if (attachment != null)
                        {
                            message.Attachments.Add(attachment);
                        }
                        smtpClient.Send(message);
                        return new DocumentOperationResponse
                        {
                            Succeeded = true,
                            Message = "Mail was sent successfully"
                        };
                    }
                    catch (SmtpException e)
                    {
                        return new DocumentOperationResponse
                        {
                            Message = "Sending an email message failed."
                        };
                    }
                    finally
                    {
                        message.Attachments.Clear();
                    }
                }
            }
        }

        protected string RemoveNewLineSymbols(string value)
        {
            return value;
        }

        public StatusMessage EmailPaySlip()
        {
            StatusMessage statusMessage = new StatusMessage();
            DocumentOperationResponse documentOperationResponse = new DocumentOperationResponse();
            try
            {
                var companyPolicy = _vpContext.CompanyPolicies.FirstOrDefault();

                if (companyPolicy != null)
                {
                    var employees = _vpContext.Employees.ToList();

                    if (employees != null)
                    {
                        foreach (Employee employee in employees)
                        {
                            if (employee.EmployeeEmailAddress != null && employee.EmployeeEmailAddress != "")
                            {
                                //load payslip report 
                                StaffPaySlip report = new StaffPaySlip();

                                string sCurrentPeriod = Convert.ToString(companyPolicy.CurrentPeriod);

                                report.Parameters["Period"].Value = Convert.ToDateTime(companyPolicy.CurrentPeriod);
                                report.Parameters["EmployeeId"].Value = employee.EmployeeID;

                                //export report and send as mail
                                var stream = new MemoryStream();

                                PdfExportOptions pdfOptions = report.ExportOptions.Pdf;

                                //report.CreateDocument();
                                report.ExportToPdf(stream);
                                //return File(stream.GetBuffer(), "application/pdf");

                                stream.Position = 0;
                                var mailAddress = new MailAddress(employee.EmployeeEmailAddress);
                                var recipients = new MailAddressCollection() { mailAddress };
                                var attachment = new Attachment(stream,"Payslip", System.Net.Mime.MediaTypeNames.Application.Pdf);
                                documentOperationResponse = SendEmail(recipients, "Payslip for " + sCurrentPeriod, "Find attached your payslip for " + sCurrentPeriod, attachment);

                                if (documentOperationResponse.Succeeded)
                                {
                                    statusMessage.Status = "Success";
                                    statusMessage.Message = documentOperationResponse.Message;
                                }
                                else
                                {
                                    statusMessage.Status = "Failed";
                                    statusMessage.Message = documentOperationResponse.Message;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                statusMessage.Status = "Failed";
                statusMessage.Message = ex.Message;
            }

            return statusMessage;
        }
    }
}