using ModelLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IFeedbackBusiness
    {
        FeedbackModel AddFeedback(int UserId, FeedbackModel feedbackModel);
        List<FeedbackModel> GetFeedbacks(int BookId);
    }
}