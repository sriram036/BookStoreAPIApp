using ModelLayer.Models;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface IFeedbackRepo
    {
        FeedbackModel AddFeedback(int UserId, FeedbackModel feedbackModel);
        List<FeedbackModel> GetFeedbacks(int BookId);
    }
}