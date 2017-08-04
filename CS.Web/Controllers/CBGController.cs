using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CS.Data.Interfaces;
using CS.Data.Repositories;
using CS.Model;

namespace CS.Web.Controllers
{
    public class CbgController : Controller
    {
        private readonly ICustomerBankGuaranteeRepository _customerBankGuaranteeRepository = new CustomerBankGuaranteeRepository();
        private readonly ICustomerRepository _customerRepository = new CustomerRepository();

        public ActionResult Index()
        {
            if (Session["LoggedUser"] == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            var customerbankguarantees = _customerBankGuaranteeRepository.FindAll().Include(c => c.Cutomer);
            return View(customerbankguarantees.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (Session["LoggedUser"] == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //CustomerBankGuarantee customerbankguarantee = (from a in db.CustomerBankGuarantees
            //                                                where a.TranId == id
            //                                                select a).Include(c=>c.Cutomer).FirstOrDefault();
            var customerbankguarantee =
                _customerBankGuaranteeRepository.FindAll().Include(c=>c.Cutomer).FirstOrDefault(aCustBankGuarantee => aCustBankGuarantee.TranId == id);
            if (customerbankguarantee == null)
            {
                return HttpNotFound();
            }
            return View(customerbankguarantee);
        }

        public ActionResult Create()
        {
            if (Session["LoggedUser"] == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            ViewBag.CustomerId = new SelectList(_customerRepository.FindAll().OrderBy(a=>a.CustomerName), "CustomerId", "CustomerName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,BgAmount,OpeningDate,ExpiryDate,IsActive")] CustomerBankGuarantee customerbankguarantee)
        {
            if (ModelState.IsValid)
            {
                customerbankguarantee.TranId = _customerBankGuaranteeRepository.FindAll().DefaultIfEmpty().Max(a => a == null ? 1 : a.TranId + 1);
                _customerBankGuaranteeRepository.Insert(customerbankguarantee);
                _customerBankGuaranteeRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(_customerRepository.FindAll().OrderBy(a => a.CustomerName), "CustomerId", "CustomerName");
            return View(customerbankguarantee);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["LoggedUser"] == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerBankGuarantee customerbankguarantee = _customerBankGuaranteeRepository.Find(a => a.TranId == id);
            if (customerbankguarantee == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(_customerRepository.FindAll().OrderBy(a => a.CustomerName), "CustomerId", "CustomerName", customerbankguarantee.CustomerId);
            return View(customerbankguarantee);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TranID,CustomerId,BgAmount,OpeningDate,ExpiryDate,IsActive")] CustomerBankGuarantee customerbankguarantee)
        {
            if (ModelState.IsValid)
            {
                _customerBankGuaranteeRepository.Update(customerbankguarantee);
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(_customerRepository.FindAll().OrderBy(a => a.CustomerName), "CustomerId", "CustomerName");
            return View(customerbankguarantee);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["LoggedUser"] == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerBankGuarantee customerbankguarantee = _customerBankGuaranteeRepository.Find(a => a.TranId == id);
            if (customerbankguarantee == null)
            {
                return HttpNotFound();
            }
            return View(customerbankguarantee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _customerBankGuaranteeRepository.Delete(id);
            _customerBankGuaranteeRepository.Save();
            return RedirectToAction("Index");
        }
       
    }
}
