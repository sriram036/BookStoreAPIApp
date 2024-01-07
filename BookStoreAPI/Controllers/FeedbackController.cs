using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using System.Collections.Generic;

namespace BookStoreAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBusiness feedbackBusiness;

        public FeedbackController(IFeedbackBusiness feedbackBusiness)
        {
            this.feedbackBusiness = feedbackBusiness;
        }

        
        [HttpPost]
        [Route("AddFeedback")]
        public IActionResult AddFeedback(FeedbackModel feedbackModel)
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            FeedbackModel feedback = feedbackBusiness.AddFeedback(UserId, feedbackModel);

            if(feedbackModel != null)
            {
                return Ok(new ResponseModel<FeedbackModel> { IsSuccess = true, Message = "Feedback Added", Data = feedback });
            }

            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Feedback Not Added", Data = "Rating Constraint Not Matched" });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetFeedbacks")]
        public List<FeedbackModel> GetFeedbacks(int BookId)
        {
            return feedbackBusiness.GetFeedbacks(BookId);
        }
    }
}
