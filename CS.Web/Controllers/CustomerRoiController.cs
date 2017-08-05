using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CS.Data.Interfaces;
using CS.Data.Repositories;
using CS.Model;
using WebGrease.Css.Extensions;

namespace CS.Web.Controllers
{
    public class CustomerRoiController : Controller
    {
        private readonly ICustomerRoiRepository _customerRoiRepository = new CustomerRoiRepository();
        private readonly ICustomerRepository _customerRepository = new CustomerRepository();

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

        // GET: /CustRoi/Edit/5
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


    }
}
