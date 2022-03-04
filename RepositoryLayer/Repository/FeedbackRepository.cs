namespace RepositoryLayer.Repository
{
    using Models;
    using MongoDB.Driver;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IMongoCollection<FeedbackModel> Feedback;

        public FeedbackRepository(IDatabaseSetting DB)
        {
            var client = new MongoClient(DB.ConnectionString);
            var Db = client.GetDatabase(DB.DatabaseName);
            Feedback = Db.GetCollection<FeedbackModel>("Feedback");
        }

        public async Task<FeedbackModel> AddFeedback(FeedbackModel feedback)
        {
            try
            {
                var check = await this.Feedback.Find(x => x.FeedbackID == feedback.FeedbackID).SingleOrDefaultAsync();
                if (check == null)
                {
                    await this.Feedback.InsertOneAsync(feedback);
                    return feedback;
                }
                return null;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<FeedbackModel> GetFeedback()
        {
            try
            {
                return Feedback.Find(FilterDefinition<FeedbackModel>.Empty).ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
