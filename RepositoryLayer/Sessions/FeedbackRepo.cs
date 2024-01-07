using Microsoft.Extensions.Configuration;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Sessions
{
    public class FeedbackRepo : IFeedbackRepo
    {
        private readonly IConfiguration configuration;

        public FeedbackRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public FeedbackModel AddFeedback(int UserId, FeedbackModel feedbackModel)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreConnection"]))
            {
                if (feedbackModel.Rating > 0 && feedbackModel.Rating < 6)
                {
                    SqlCommand cmd = new SqlCommand("spAddFeedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@BookId", feedbackModel.BookId);
                    cmd.Parameters.AddWithValue("@Rating", feedbackModel.Rating);
                    cmd.Parameters.AddWithValue("@Comment", feedbackModel.Comment);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return feedbackModel;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<FeedbackModel> GetFeedbacks(int BookId)
        {
            List<FeedbackModel> feedbacks = new List<FeedbackModel>();

            using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetFeedbacks", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", BookId);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    FeedbackModel feedback = new FeedbackModel();
                    feedback.UserId = Convert.ToInt32(Reader["UserId"]);
                    feedback.BookId = Convert.ToInt32(Reader["BookId"]);
                    feedback.Rating = Convert.ToInt32(Reader["Rating"]);
                    feedback.Comment = Reader["Comment"].ToString();
                    feedbacks.Add(feedback);
                }

                return feedbacks;
            }
        }
    }
}
