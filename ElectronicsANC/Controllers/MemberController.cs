using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class MemberController : Controller
    {
        private MemberRepository _memberRepository = new MemberRepository();

        // GET: Member
        public ActionResult Index(string sortOrder, string firstName, string lastName, string city, string country)
        {
            List<MemberModel> members = _memberRepository.GetAllMembers();

            ViewBag.FirstNameSortParam = string.IsNullOrEmpty(sortOrder) ? "fn_desc" : "";
            ViewBag.LastNameSortParam = sortOrder == "last name" ? "ln_desc" : "last name";
            ViewBag.AddressSortParam = sortOrder == "address" ? "address_desc" : "address";
            ViewBag.CitySortParam = sortOrder == "city" ? "city_desc" : "city";
            ViewBag.CountrySortParam = sortOrder == "country" ? "country_desc" : "country";
            ViewBag.PostalCodeSortParam = sortOrder == "postal code" ? "pc_desc" : "postal code";

            if(!string.IsNullOrEmpty(firstName))
                members = SearchBy(0, firstName, ref members);

            if (!string.IsNullOrEmpty(lastName))
                members = SearchBy(1, lastName, ref members);

            if (!string.IsNullOrEmpty(city))
                members = SearchBy(2, city, ref members);

            if (!string.IsNullOrEmpty(country))
                members = SearchBy(3, country, ref members);

            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(country))
                members = ResetSearch(ref members);

            members = SortMembers(members, sortOrder);

            return View("Index", members);
        }

        // GET: Member/Details/5
        public ActionResult Details(Guid id)
        {
            MemberModel memberModel = _memberRepository.GetMemberById(id);

            return View("DetailsMember", memberModel);
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            return View("CreateMember");
        }

        // POST: Member/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                MemberModel memberModel = new MemberModel();
                UpdateModel(memberModel);

                _memberRepository.InsertMember(memberModel);

                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                        return RedirectToAction("Index");
                }

                return Redirect("~/");
            }
            catch
            {
                return View("CreateMember");
            }
        }

        // GET: Member/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            MemberModel memberModel = _memberRepository.GetMemberById(id);

            return View("EditMember", memberModel);
        }

        // POST: Member/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                MemberModel memberModel = new MemberModel();
                UpdateModel(memberModel);

                _memberRepository.UpdateMember(memberModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditMember");
            }
        }

        // GET: Member/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            MemberModel memberModel = _memberRepository.GetMemberById(id);

            return View("DeleteMember", memberModel);
        }

        // POST: Member/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                _memberRepository.DeleteMember(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteMember");
            }
        }

        private List<MemberModel> SearchBy(int type, string filter, ref List<MemberModel> memberModels)
        {
            switch (type)
            {
                case 0:
                    memberModels = _memberRepository.GetMembersByFirstName(filter);
                    break;
                case 1:
                    memberModels = _memberRepository.GetMembersByLastName(filter);
                    break;
                case 2:
                    memberModels = _memberRepository.GetMembersByCity(filter);
                    break;
                case 3:
                    memberModels = _memberRepository.GetMembersByCountry(filter);
                    break;
                default:
                    memberModels = _memberRepository.GetAllMembers();
                    break;
            }

            return memberModels;
        }

        private List<MemberModel> ResetSearch(ref List<MemberModel> memberModels)
        {
            return memberModels = _memberRepository.GetAllMembers();
        }

        private List<MemberModel> SortMembers(List<MemberModel> temp, string sortOrder)
        {
            switch (sortOrder)
            {
                case "fn_desc":
                    return _memberRepository.OrderByDescendingParameter(temp, "FirstName");
                case "ln_desc":
                    return _memberRepository.OrderByDescendingParameter(temp, "LastName");
                case "address_desc":
                    return _memberRepository.OrderByDescendingParameter(temp, "Address");
                case "city_desc":
                    return _memberRepository.OrderByDescendingParameter(temp, "City");
                case "country_desc":
                    return _memberRepository.OrderByDescendingParameter(temp, "Country");
                case "pc_desc":
                    return _memberRepository.OrderByDescendingParameter(temp, "PostalCode");
                case "last name":
                    return _memberRepository.OrderByAscendingParameter(temp, "LastName");
                case "address":
                    return _memberRepository.OrderByAscendingParameter(temp, "Address");
                case "city":
                    return _memberRepository.OrderByAscendingParameter(temp, "City");
                case "country":
                    return _memberRepository.OrderByAscendingParameter(temp, "Country");
                case "postal code":
                    return _memberRepository.OrderByAscendingParameter(temp, "PostalCode");
                default:
                    return _memberRepository.OrderByAscendingParameter(temp, "FirstName");
            }
        }
    }
}
