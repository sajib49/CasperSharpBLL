using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CrystalDecisions.Shared;
using CS.Data.Interfaces;
using CS.Data.Repositories;
using CS.Model;
using WebGrease.Css.Extensions;
using CrystalDecisions.CrystalReports.Engine;
using CS.Model.ViewModels;

namespace CS.Web.Controllers
{
    public class CustomerRoiController : Controller
    {
        private readonly ICustomerRoiRepository _customerRoiRepository = new CustomerRoiRepository();
        private readonly ICustomerRepository _customerRepository = new CustomerRepository();
        private readonly IMarketGroupRepository _marketGroupRepository = new MarketGroupRepository();

        public ActionResult Index()
        {
            var customerrois = _customerRoiRepository.FindAll().Include(c => c.Cutomer);
            return View(customerrois.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRoi customerroi = _customerRoiRepository.Find(a => a.TranId == id);
            if (customerroi == null)
            {
                return HttpNotFound();
            }
            return View(customerroi);
        }

        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(_customerRepository.FindAll().OrderBy(a => a.CustomerName), "CustomerId", "CustomerName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,LastUpdateDate,ExpMonth,CommisionInc,KpiInc,CollectionInc,VehicleSubsidiary,OthersInc,MgrSalary,SASalary,RASalary,DriverSalary,OthersExp,VehicleExp,OfficeRent,Maintenance,StockInc,CreditToMKT,PromRepInc,Bg")] CustomerRoi customerroi)
        {
            if (ModelState.IsValid)
            {
                 _customerRoiRepository.Delete(a =>
                    a.ExpMonth.Month == DateTime.Today.Month &&
                    a.ExpMonth.Year == DateTime.Today.Year &&
                    a.CustomerId == customerroi.CustomerId);
                
                customerroi.ProfitRoi = (customerroi.CommisionInc + customerroi.KpiInc + customerroi.CollectionInc + customerroi.VehicleSubsidiary + customerroi.OthersInc) - (customerroi.MgrSalary + customerroi.SaSalary + customerroi.RaSalary + customerroi.DriverSalary + customerroi.OthersExp + customerroi.VehicleExp + customerroi.OfficeRent + customerroi.Maintenance);
                customerroi.IsCurrent = 1;
                customerroi.TranId = _customerRoiRepository.FindAll().Max(a => a == null ? 1 : a.TranId + 1);
                _customerRoiRepository.Insert(customerroi);
                _customerRoiRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(_customerRepository.FindAll().OrderBy(a => a.CustomerName), "CustomerId", "CustomerName", customerroi.CustomerId);
            return View(customerroi);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRoi customerroi = _customerRoiRepository.Find(a => a.TranId == id);
            if (customerroi == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(_customerRepository.FindAll().OrderBy(a => a.CustomerName), "CustomerId", "CustomerName", customerroi.CustomerId);
            return View(customerroi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TranId,CustomerId,LastUpdateDate,ExpMonth,CommisionInc,KpiInc,CollectionInc,VehicleSubsidiary,OthersInc,MgrSalary,SASalary,RASalary,DriverSalary,OthersExp,VehicleExp,OfficeRent,Maintenance,StockInc,CreditToMKT,PromRepInc,Bg,IsCurrent")] CustomerRoi customerroi)
        {
            if (ModelState.IsValid)
            {
                var allCustomerRoi = _customerRoiRepository.FindAll(a => a.CustomerId == customerroi.CustomerId);
                allCustomerRoi.ForEach(a =>
                {
                    a.IsCurrent = 0;
                    _customerRoiRepository.Update(a);
                });


                 _customerRoiRepository.Delete(a =>
                    a.ExpMonth.Month == DateTime.Today.Month && 
                    a.ExpMonth.Year == DateTime.Today.Year
                    && a.CustomerId == customerroi.CustomerId);

                customerroi.ProfitRoi = (customerroi.CommisionInc + customerroi.KpiInc + customerroi.CollectionInc + customerroi.VehicleSubsidiary + customerroi.OthersInc) -
                                        (customerroi.MgrSalary + customerroi.SaSalary + customerroi.RaSalary + customerroi.DriverSalary + customerroi.OthersExp + customerroi.VehicleExp + customerroi.OfficeRent + customerroi.Maintenance);
                customerroi.IsCurrent = 1;
                customerroi.TranId = _customerRoiRepository.FindAll().DefaultIfEmpty().Max(a => a == null ? 1 : a.TranId + 1);
                _customerRoiRepository.Insert(customerroi);
                _customerRoiRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(_customerRepository.FindAll().OrderBy(a => a.CustomerName), "CustomerId", "CustomerName", customerroi.CustomerId);
            return View(customerroi);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRoi customerroi = _customerRoiRepository.Find(a => a.TranId == id);
            if (customerroi == null)
            {
                return HttpNotFound();
            }
            return View(customerroi);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerRoi customerroi = _customerRoiRepository.Find(a => a.TranId == id);
            _customerRoiRepository.Delete(customerroi);
            _customerRoiRepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult CustomerRoiReportFilter()
        {
            ViewBag.AreaList = new SelectList(_marketGroupRepository.FindAll(a=>
                                 a.MarketGroupType ==1).OrderBy(a => a.MarketGroupDesc), 
                                 "MarketGroupId", "MarketGroupDesc");
            ViewBag.TerritoryList = new SelectList(_marketGroupRepository.FindAll(a =>
                                a.MarketGroupType == 2).OrderBy(a => a.MarketGroupDesc),
                                "MarketGroupId", "MarketGroupDesc");
            ViewBag.CustomerList = new SelectList(_customerRepository.FindAll().
                                OrderBy(a => a.CustomerName), "CustomerId",
                                "CustomerName");


            return View();
        }

        public ActionResult CustomerRoiReport(int nCustomerId,int nAreaId,int nTerritoryId,string sFromDate,string sTodate)
        {
            List<CustomerRoiDetails> oCustomerRoiDetails = new List<CustomerRoiDetails>()
            {
                new CustomerRoiDetails
                {
                    AreaName = "Dhaka",
                    OthersInc = 2424,
                    VehicleExp = 4235,
                    StockInc = 435,
                    CreditToMkt = 534
                },
                   new CustomerRoiDetails
                {
                    AreaName = "Rajshahi",
                    OthersInc = 324,
                    VehicleExp = 534,
                    StockInc = 435345,
                    CreditToMkt = 534
                }

            };

            ReportDocument rptCustomerRoi = new ReportDocument();
            //rptCustomerRoi.Load(Path.Combine(Server.MapPath("~/Reports"), "TestRepot.rpt")); //For Same Project
            //rptCustomerRoi.SetDataSource(_customerRoiRepository.GetCustomerRoiDetailse());
            string path = Path.GetFullPath(Path.Combine(Server.MapPath("~"), @"..\CS.Report/sajib.rpt"));  //For Diffrent Project
            rptCustomerRoi.Load(path);
            rptCustomerRoi.SetDataSource(oCustomerRoiDetails);
            rptCustomerRoi.SetParameterValue("Country","Bangladesh");
            //rptCustomerRoi.Load(Path.Combine(Server.MapPath("~/Reports"), "TestRepot.rpt"));
           
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rptCustomerRoi.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            //return File(stream, "application/pdf","file_name"); // For Direct download
            return new FileStreamResult(stream, "application/pdf"); //For Direct View
        }


    }
}
