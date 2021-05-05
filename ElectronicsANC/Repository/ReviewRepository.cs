using ElectronicsANC.Models;
using ElectronicsANC.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Repository
{
    public class ReviewRepository
    {
        public ElectronicsDbDataContext dbContext;

        public ReviewRepository()
        {
            dbContext = new ElectronicsDbDataContext();
        }

        public ReviewRepository(ElectronicsDbDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ReviewModel> GetAllReviews()
        {
            List<ReviewModel> reviewList = new List<ReviewModel>();

            foreach (Review dbReview in dbContext.Reviews)
                AddDbObjectToModelCollection(reviewList, dbReview);

            return reviewList;
        }

        public ReviewModel GetReviewById(Guid id)
        {
            var review = dbContext.Reviews.FirstOrDefault(x => x.IdReview == id);

            return MapDbObjectToModel(review);
        }

        public List<ReviewModel> OrderByDescendingParameter(List<ReviewModel> models, string parameter)
        {
            if (parameter == "Details")
                return models.OrderByDescending(x => x.ReviewDetails).ToList();

            if (parameter == "Date")
                return models.OrderByDescending(x => x.ReviewDate).ToList();

            return models;
        }

        public List<ReviewModel> OrderByAscendingParameter(List<ReviewModel> models, string parameter)
        {
            if (parameter == "Details")
                return models.OrderBy(x => x.ReviewDetails).ToList();

            if (parameter == "Date")
                return models.OrderBy(x => x.ReviewDate).ToList();

            return models;
        }

        public void InsertReview(ReviewModel review)
        {
            review.IdReview = Guid.NewGuid();

            dbContext.Reviews.InsertOnSubmit(MapModelToDbObject(review));
            dbContext.SubmitChanges();
        }

        public void UpdateReview(ReviewModel review)
        {
            Review dbReview = dbContext.Reviews.FirstOrDefault(x => x.IdReview == review.IdReview);

            if(dbReview != null)
            {
                dbReview.IdReview = review.IdReview;
                dbReview.IdMember = review.IdMember;
                dbReview.IdProduct = review.IdProduct;
                dbReview.ReviewDate = review.ReviewDate;
                dbReview.ReviewDetails = review.ReviewDetails;
                dbContext.SubmitChanges();
            }
        }

        public void DeleteReview(Guid id)
        {
            Review dbReview = dbContext.Reviews.FirstOrDefault(x => x.IdReview == id);

            if(dbReview != null)
            {
                dbContext.Reviews.DeleteOnSubmit(dbReview);
                dbContext.SubmitChanges();
            }
        }

        private void AddDbObjectToModelCollection(List<ReviewModel> reviewList, Review dbReview)
        {
            reviewList.Add(MapDbObjectToModel(dbReview));
        }

        private ReviewModel MapDbObjectToModel(Review dbReview)
        {
            ReviewModel review = new ReviewModel();

            if(dbReview != null)
            {
                review.IdReview = dbReview.IdReview;
                review.IdMember = dbReview.IdMember;
                review.IdProduct = dbReview.IdProduct;
                review.ReviewDate = dbReview.ReviewDate;
                review.ReviewDetails = dbReview.ReviewDetails;

                return review;
            }

            return null;
        }

        private Review MapModelToDbObject(ReviewModel review)
        {
            Review dbReview = new Review();

            if(review != null)
            {
                dbReview.IdReview = review.IdReview;
                dbReview.IdMember = review.IdMember;
                dbReview.IdProduct = review.IdProduct;
                dbReview.ReviewDate = review.ReviewDate;
                dbReview.ReviewDetails = review.ReviewDetails;

                return dbReview;
            }

            return null;
        }
    }
}