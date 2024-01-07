using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sessions
{
    public class FeedbackBusiness : IFeedbackBusiness
    {
        private readonly IFeedbackRepo feedbackRepo;

        public FeedbackBusiness(IFeedbackRepo feedbackRepo)
        {
            this.feedbackRepo = feedbackRepo;
        }

        public FeedbackModel AddFeedback(int UserId, FeedbackModel feedbackModel)
        {
            return feedbackRepo.AddFeedback(UserId, feedbackModel);
        }

        public List<FeedbackModel> GetFeedbacks(int BookId)
        {
            return feedbackRepo.GetFeedbacks(BookId);
        }
    }
}
