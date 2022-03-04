namespace ManagerLayer.Interface
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFeedbackManager
    {
        Task<FeedbackModel> AddFeedback(FeedbackModel feedback);
        IEnumerable<FeedbackModel> GetFeedback();
    }
}
