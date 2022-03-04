namespace ManagerLayer.Manager
{
    using ManagerLayer.Interface;
    using Models;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class FeedbackManager : IFeedbackManager
    {
        private readonly IFeedbackRepository repo;
        public FeedbackManager(IFeedbackRepository repo)
        {
            this.repo = repo;
        }
        public async Task<FeedbackModel> AddFeedback(FeedbackModel feedback)
        {
            try
            {
                return await this.repo.AddFeedback(feedback);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<FeedbackModel> GetFeedback()
        {
            try
            {
                return this.repo.GetFeedback();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
