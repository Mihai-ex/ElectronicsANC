using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class ReviewController : Controller
    {
        private ReviewRepository _reviewRepository = new ReviewRepository();

        // GET: Review
        public ActionResult Index()
        {
            List<ReviewModel> reviews = _reviewRepository.GetAllReviews();

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
            return View("CreateReview");
        }

        // POST: Review/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ReviewModel review = new ReviewModel();
                UpdateModel(review);

                _reviewRepository.InsertReview(review);

                return RedirectToAction("Index");
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
                ReviewModel review = new ReviewModel();
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
    }
}
