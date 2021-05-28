using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class ReviewController : Controller
    {
        private ReviewRepository _reviewRepository = new ReviewRepository();
        private MemberRepository _memberRepository = new MemberRepository();
        private ProductRepository _productRepository = new ProductRepository();
        
        // GET: Review
        public ActionResult Index(string sortOrder)
        {
            List<ReviewModel> reviews = _reviewRepository.GetAllReviews();

            ViewBag.ReviewSortParam = string.IsNullOrEmpty(sortOrder) ? "review_desc" : "";
            ViewBag.DateSortParam = sortOrder == "reviewdate" ? "date_desc" : "date";

            reviews = SortReviews(reviews, sortOrder);

            foreach(var review in reviews)
            {
                var member = _memberRepository.GetMemberById(review.IdMember);
                var product = _productRepository.GetProdctById(review.IdProduct);
                review.MemberName = member.Name;
                review.ProductName = product.ProductName;
            }

            return View("Index", reviews);
        }

        // GET: Review/Details/5
        public ActionResult Details(Guid id)
        {
            ReviewModel review = _reviewRepository.GetReviewById(id);

            return View("DetailsReview", review);
        }

        // GET: Review/Create
        public ActionResult Create()
        {
            SetViewbagMembers();
            SetViewbagProducts();

            return View("CreateReview");
        }

        // POST: Review/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ReviewModel review = new ReviewModel();

                //verifica daca viewbag-ul este gol cand vine request din Details de la ceva produs
                //in caz ca e gol trebuie facut viewbag-ul din nou
                if (Request.Form["Member"] == null)                
                    SetViewbagMembers();

                //trebuie NEAPARAT facut update la model aici ca sa se salveze idProdus adus din controller extern
                UpdateModel(review);
                //mai sus trebuie facut update model pentru ca, pentru ceva motiv, cand se executa linia de jos se sare direct la catch!
                review.IdMember = Guid.Parse(Request.Form["Member"]);

                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                        if(review.IdProduct == Guid.Empty)
                            review.IdProduct = Guid.Parse(Request.Form["ProductsToReview"]);
                }

                UpdateModel(review);

                _reviewRepository.InsertReview(review);

                if (User.Identity.IsAuthenticated)
                    if (User.IsInRole("Admin"))
                        return RedirectToAction("Index");

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("CreateReview");
            }
        }

        // GET: Review/Edit/5
        public ActionResult Edit(Guid id)
        {
            ReviewModel review = _reviewRepository.GetReviewById(id);

            return View("EditReview", review);
        }

        // POST: Review/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                ReviewModel review = _reviewRepository.GetReviewById(id);
                UpdateModel(review);

                _reviewRepository.UpdateReview(review);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditReview");
            }
        }

        // GET: Review/Delete/5
        public ActionResult Delete(Guid id)
        {
            ReviewModel review = _reviewRepository.GetReviewById(id);

            return View("DeleteReview", review);
        }

        // POST: Review/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                _reviewRepository.DeleteReview(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteReview");
            }
        }

        private void SetViewbagProducts()
        {
            var items = _productRepository.GetAllProducts();

            if (items != null)
            {
                ViewBag.products = items;
            }
        }

        private void SetViewbagMembers()
        {
            var members = _memberRepository.GetAllMembers();

            if (members != null)
            {
                ViewBag.members = members;
            }
        }

        private List<ReviewModel> SortReviews(List<ReviewModel> temp, string sortOrder)
        {
            switch (sortOrder)
            {
                case "review_desc":
                    return _reviewRepository.OrderByDescendingParameter(temp, "Details");
                case "date_desc":
                    return _reviewRepository.OrderByDescendingParameter(temp, "Date");
                case "date":
                    return _reviewRepository.OrderByAscendingParameter(temp, "Date");
                default:
                    return _reviewRepository.OrderByAscendingParameter(temp, "Details");
            }
        }
    }
}
